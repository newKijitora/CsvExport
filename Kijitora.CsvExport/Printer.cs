using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using KijitoraClassLibrary.Reflection;

namespace Kijitora.CsvExport
{
    public static class Printer
    {
        // CSVファイルを出力します。
        public static void CsvExport<TSource>(this IEnumerable<TSource> source, string outputPath, CsvFormat format, CsvConfig config)
        {
            if (source is null || outputPath is null || format is null || config is null)
            {
                throw new ArgumentNullException();
            }

            if (!File.Exists(outputPath)) throw new FileNotFoundException();

            bool fieldIsExists = false;
            PropertyInfo[] propInfos = null;

            if (source.Any())
            {
                try
                {
                    propInfos = source.First().ExtractProperties(
                        format.Fields.Select(field => field.Name).ToArray()).ToArray();
                }
                catch
                {
                    throw new ArgumentException();
                }

                if (propInfos.Length == 0)
                {
                    throw new ArgumentException("フォーマットが不正です。");
                }

                fieldIsExists = true;
            }

            // 書き出し用の文字列
            StringBuilder stringBuilder = new StringBuilder();

            // CSVは値をダブルクォートで囲むのが原則
            string quate = config.DoubleQuateRequired ? "\"" : "";

            // 改行コードの設定
            string newLineCode = config.GetNewLineCodeToString();

            // ヘッダーの出力
            if (config.HeaderRequired)
            {
                for (int i = 0; i < format.Headers.Length; i++)
                {
                    StringBuilder header = new StringBuilder(format.Headers[i].Name);

                    if (!config.DoubleQuateRequired)
                    {
                        config.ParseColumn(ref header);
                    }

                    stringBuilder
                        .Append(quate)
                        .Append(config.DoubleQuateEscapeRequired ? header.Replace("\"", "\"\"") : header)
                        .Append(quate)
                        .Append(config.Delimiter);
                }

                stringBuilder
                    .Remove(stringBuilder.Length - config.Delimiter.Length, config.Delimiter.Length)
                    .Append(newLineCode);
            }

            if (propInfos != null)
            {
                // フィールドの出力
                foreach (TSource obj in source)
                {
                    for (int i = 0; i < propInfos.Length; i++)
                    {
                        StringBuilder field = new StringBuilder(propInfos[i].GetValue(obj).ToString());

                        if (!config.DoubleQuateRequired)
                        {
                            config.ParseColumn(ref field);
                        }

                        stringBuilder
                            .Append(quate)
                            .Append(config.DoubleQuateEscapeRequired ? field.Replace("\"", "\"\"") : field)
                            .Append(quate)
                            .Append(config.Delimiter);
                    }

                    stringBuilder
                        .Remove(stringBuilder.Length - config.Delimiter.Length, config.Delimiter.Length)
                        .Append(newLineCode);
                }
            }

            // 最終行の改行を削除する必要がある場合
            if ((fieldIsExists || config.HeaderRequired) && !config.NewLineAtLastLine)
            {
                stringBuilder.Remove(stringBuilder.Length - newLineCode.Length, newLineCode.Length);
            }

            // ファイルへ出力
            using (FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.ReadWrite))
            using (StreamWriter writer = new StreamWriter(stream, config.Encoding))
            {
                writer.Write(stringBuilder.ToString());
            }
        }
    }
}
