using System;

namespace Kijitora.CsvExport
{
    public class CsvColumn
    {
        public string Field { get; } = "";
        public string Header { get; } = "";
        public int Index { get; } = 0;

        public CsvColumn(CsvHeader header, CsvField field, int index)
        {
            if (header is null || field is null)
            {
                throw new ArgumentException();
            }

            Header = header.Name;
            Field = field.Name;
            Index = index;
        }
    }
}
