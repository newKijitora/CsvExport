using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using KijitoraClassLibrary.Reflection;

namespace Kijitora.ImportExport
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

            if (!objs.Any())
            {
                throw new ArgumentException();
            }

            if (!File.Exists(outputPath))
            {
                throw new FileNotFoundException();
            }

            // CSVは値をダブルクォートで囲むのが原則
            string quate = config.DoubleQuateRequired ? "\"" : "";

            PropertyInfo[] propInfos = objs.First().ExtractProperties(format.Fields).ToArray();
            int propLength = propInfos.Length;

            using (FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.ReadWrite))
            using (StreamWriter writer = new StreamWriter(stream, config.Encoding))
            {
                // ヘッダーの出力
                if (config.HeaderRequired)
                {
                    writer.WriteLine(string.Join(config.Delimiter, format.Headers.Select(header => quate + header + quate).ToArray()));
                }

                // フィールドの出力
                foreach (var obj in objs)
                {
                    string[] strArray = new string[propLength];

                    for (int i = 0; i < propLength; i++)
                    {
                        strArray[i] = quate + propInfos[i].GetValue(obj).ToString() + quate;
                    }

                    writer.WriteLine(string.Join(config.Delimiter, strArray));
                }
            }
        }
    }
}
