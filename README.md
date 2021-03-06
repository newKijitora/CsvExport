# CsvExport

## 使い方

プログラムからこのライブラリを参照して、下記のように使用します。  
以下は、コンソールアプリケーションから使用する例です。

```C#
using System.Collections.Generic;
using Kijitora.CsvExport;

namespace SampleNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            // 保存先のファイルパス
            string path = @"C:\...save-path";

            // サンプルのデータを生成する
            List<Character> characters = new List<Character>
            {
                // うちの犬と猫たちの名前です ^ ^
                new Character("むう", "男", "勇者", 17),
                new Character("マオ", "女", "戦士", 25),
                new Character("ニコ", "女", "僧侶", 14),
                new Character("ルナ", "女", "魔法使い", 21)
            };

            // フォーマットを生成する
            CsvFormat format = new CsvFormat(new List<CsvColumn>
            {
                // CsvFieldに渡された型は、ToString()メソッドを使って出力されます。
                // 引数は、new CsvColumn(ヘッダー、フィールド、カラムの順序) です。
                new CsvColumn(new CsvHeader("名前"), new CsvField(nameof(Character.Name)), 1),
                new CsvColumn(new CsvHeader("性別"), new CsvField(nameof(Character.Sex)), 2),
                new CsvColumn(new CsvHeader("職業"), new CsvField(nameof(Character.Job)), 3),
                new CsvColumn(new CsvHeader("レベル"), new CsvField(nameof(Character.Level)), 4)
            });

            // 設定を生成する（デフォルトはCSV標準に準拠した設定となります）
            CsvConfig config = new CsvConfig();

            // いろいろ設定する
            config.HeaderRequired = true;             // ヘッダーを出力するかどうか
            config.Delimiter = ",";                   // 区切り文字
            config.NewLineAtLastLine = false;         // 最後に空行を出力するかどうか
            config.DoubleQuateRequired = false;       // フィールドをダブルクォーテーションで囲うかどうか
            config.NewLineCode = NewLineCode.CRLF;    // 改行コード
            config.DoubleQuateEscapeRequired = false; // ダブルクォーテーションをエスケープするかどうか

            // CSV標準の設定にリセットする
            config.ResetToStandard();

            // エクスポート
            characters.CsvExport(path, format, config);
        }
    }

    // サンプルクラス
    public class Character
    {
        // キャラクターの名前
        public string Name { get; private set; }

        // キャラクターの性別
        public string Sex { get; private set; }

        // キャラクターの職業
        public string Job { get; private set; }

        // キャラクターのレベル
        public int Level { get; private set; }

        // コンストラクタ
        public Character(string name, string sex, string job, int level)
        {
            Name = name;
            Sex = sex;
            Job = job;
            Level = level;
        }
    }
}
```
