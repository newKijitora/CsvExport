using System.Text;

namespace Kijitora.ImportExport
{
    public class CsvConfig
    {
        // 文字エンコード: 初期値は「UTF-8」
        public Encoding Encoding { get; set; } = Encoding.GetEncoding(65001);

        // 改行コード指定
        public string NewLine { get; set; }

        // ヘッダーが必要かどうか: 初期値は「不要」
        public bool HeaderRequired { get; set; } = false;

        // 区切り文字: 初期値は「カンマ区切り」
        public string Delimiter { get; set; } = ",";

        // ダブルクォートで囲むかどうか: 初期値は「囲む」
        public bool DoubleQuateRequired { get; set; } = true;
    }
}
