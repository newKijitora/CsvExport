# CsvExport

## �g����

���C�u�������Q�Ƃ���v���O��������A���L�̂悤�ɂ��g�����������B  
���L�́A�R���\�[���A�v���P�[�V��������g�p�����ł��B

```C#
using System.Collections.Generic;
using Kijitora.CsvExport;

namespace SampleNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            // �ۑ���̃t�@�C���p�X
            string path = @"C:\...save-path";

            // �T���v���̃f�[�^�𐶐�����
            List<Character> characters = new List<Character>
            {
                new Character("�A�[�T�[", "�j", "�E��", 17),
                new Character("���[�K��", "�j", "��m", 25),
                new Character("�G���X", "��", "����", 14),
                new Character("�}�M�[", "��", "���@�g��", 21)
            };

            // �t�H�[�}�b�g�𐶐�����
            CsvFormat format = new CsvFormat(new List<CsvColumn>
            {
                new CsvColumn(new CsvHeader("���O"), new CsvField(nameof(Character.Name)), 1),
                new CsvColumn(new CsvHeader("����"), new CsvField(nameof(Character.Sex)), 2),
                new CsvColumn(new CsvHeader("�E��"), new CsvField(nameof(Character.Job)), 3),
                new CsvColumn(new CsvHeader("���x��"), new CsvField(nameof(Character.Level)), 4)
            });

            // �ݒ�𐶐�����
            CsvConfig config = new CsvConfig();

            // ���낢��ݒ肷��
            config.HeaderRequired = true;
            config.Delimiter = ",";
            config.NewLineAtLastLine = false;
            config.DoubleQuateRequired = false;
            config.NewLineCode = NewLineCode.CRLF;
            config.DoubleQuateEscapeRequired = false;

            // �W���̐ݒ�Ƀ��Z�b�g����
            config.ResetToStandard();

            // �G�N�X�|�[�g
            characters.CsvExport(path, format, config);
        }
    }

    // �T���v���N���X
    public class Character
    {
        // �L�����N�^�[�̖��O
        public string Name { get; private set; }

        // �L�����N�^�[�̐���
        public string Sex { get; private set; }

        // �L�����N�^�[�̐E��
        public string Job { get; private set; }

        // �L�����N�^�[�̃��x��
        public int Level { get; private set; }

        // �R���X�g���N�^
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
