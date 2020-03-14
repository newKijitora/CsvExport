# ImportExport

# 使い方

ライブラリを利用するアプリケーションから、下記のように操作してください。
例は、コンソールアプリケーションです。

using Kijitora.CsvExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingCsvExport
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
                new Character("アーサー", "男", "勇者", 17),
                new Character("モーガン", "男", "戦士", 25),
                new Character("エリス", "女", "賢者", 14),
                new Character("マギー", "女", "魔法使い", 21)
            };

            // フォーマットを生成する
            CsvFormat format = new CsvFormat(new List<CsvColumn>
            {
                new CsvColumn(new CsvHeader("名前"), new CsvField(nameof(Character.Name)), 1),
                new CsvColumn(new CsvHeader("性別"), new CsvField(nameof(Character.Sex)), 2),
                new CsvColumn(new CsvHeader("職業"), new CsvField(nameof(Character.Job)), 3),
                new CsvColumn(new CsvHeader("レベル"), new CsvField(nameof(Character.Level)), 4)
            });

            // 設定を生成する
            CsvConfig config = new CsvConfig();

            // いろいろ設定する
            config.HeaderRequired = true;
            config.Delimiter = ",";
            config.NewLineAtLastLine = false;
            config.DoubleQuateRequired = false;
            config.NewLineCode = NewLineCode.CRLF;
            config.DoubleQuateEscaped = false;

            // 標準の設定にリセットする
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
