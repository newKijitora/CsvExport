using System.Text;

namespace Kijitora.CsvExport
{
    public class CsvConfig
    {
        // 文字エンコード: 初期値は「UTF-8」
        public Encoding Encoding { get; set; } = Encoding.GetEncoding(65001);

        // 改行コード指定: 初期値は「CRLF」
        public NewLineCode NewLineCode { get; set; } = NewLineCode.CRLF;

        // 最終行に改行が必要かどうか: 初期値は「必要」
        public bool NewLineAtLastLine { get; set; }

        // ヘッダーが必要かどうか: 初期値は「不要」
        public bool HeaderRequired { get; set; }

        // 区切り文字: 初期値は「カンマ区切り」
        public string Delimiter { get; set; } = ",";

        // ダブルクォートで囲むかどうか: 初期値は「囲む」
        public bool DoubleQuateRequired { get; set; }

        // ダブルクォートをエスケープするかどうか: 初期値は「する」
        public bool DoubleQuateEscaped { get; set; }

        // コンストラクタ
        public CsvConfig()
        {
            ResetToStandard();
        }

        // 改行コードを文字列に変換する
        internal string GetNewLineCodeToString()
        {
            string newLineCodeString = null;

            switch (NewLineCode)
            {
                case NewLineCode.CRLF:
                    newLineCodeString = "\r\n";
                    break;
                case NewLineCode.LF:
                    newLineCodeString = "\n";
                    break;
            }

            return newLineCodeString;
        }

        // 設定を標準にリセットする
        public void ResetToStandard()
        {
            // 改行コード指定: 初期値は「CRLF」
            NewLineCode = NewLineCode.CRLF;

            // 最終行に改行が必要かどうか: 初期値は「必要」
            NewLineAtLastLine = true;

            // ヘッダーが必要かどうか: 初期値は「不要」
            HeaderRequired = false;

            // ダブルクォートで囲むかどうか: 初期値は「囲む」
            DoubleQuateRequired = true;

            // ダブルクォートをエスケープするかどうか: 初期値は「する」
            DoubleQuateEscaped = true;
        }

        // 設定に従って列をパースする
        internal StringBuilder ParseColumn(ref StringBuilder builder)
        {
            string[] controlCharacters = new[] { "\r\n", "\n", Delimiter };

            if (DoubleQuateEscaped)
            {
                builder.Replace("\"", "");
            }

            foreach (var controlCharacter in controlCharacters)
            {
                builder.Replace(controlCharacter, "");
            }

            return builder;
        }
    }
}
