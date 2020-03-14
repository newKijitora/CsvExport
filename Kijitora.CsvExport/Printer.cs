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
        public static void CsvExport<T>(this IEnumerable<T> objs, string outputPath, CsvFormat format, CsvConfig config)
        {
            if (objs is null || outputPath is null || format is null || config is null)
            {
                throw new ArgumentNullException();
            }

            bool fieldIsExist = false;
            PropertyInfo[] propInfos = null;

            if (objs.Any())
            {
                fieldIsExist = true;

                try
                {
                    propInfos = objs.First().ExtractProperties(format.Fields).ToArray();
                }
                catch
                {
                    throw new ArgumentException();
                }

                if (propInfos.Length == 0)
                {
                    throw new ArgumentException("フォーマットが不正です。");
                }
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
                    var header = new StringBuilder(format.Headers[i]);

                    if (!config.DoubleQuateRequired)
                    {
                         config.ParseColumn(ref header);
                    }

                    stringBuilder.Append(quate);
                    stringBuilder.Append(config.DoubleQuateEscaped ? header.Replace("\"", "\"\"") : header);
                    stringBuilder.Append(quate);
                    stringBuilder.Append(config.Delimiter);
                }

                stringBuilder.Remove(stringBuilder.Length - config.Delimiter.Length, config.Delimiter.Length);
                stringBuilder.Append(newLineCode);
            }

            if (propInfos != null)
            {
                // フィールドの出力
                foreach (var obj in objs)
                {
                    for (int i = 0; i < propInfos.Length; i++)
                    {
                        var field = new StringBuilder(propInfos[i].GetValue(obj).ToString());

                        if (!config.DoubleQuateRequired)
                        {
                            config.ParseColumn(ref field);
                        }

                        stringBuilder.Append(quate);
                        stringBuilder.Append(config.DoubleQuateEscaped ? field.Replace("\"", "\"\"") : field);
                        stringBuilder.Append(quate);
                        stringBuilder.Append(config.Delimiter);
                    }

                    stringBuilder.Remove(stringBuilder.Length - config.Delimiter.Length, config.Delimiter.Length);
                    stringBuilder.Append(newLineCode);
                }
            }

            // 最終行の改行を削除する必要がある場合
            if ((fieldIsExist || config.HeaderRequired) && !config.NewLineAtLastLine)
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
