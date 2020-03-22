using System;

namespace Kijitora.CsvExport
{
    public class CsvColumn
    {
        public CsvField Field { get; set; }
        public CsvHeader Header { get; set; }
        public int Index { get; set; }

        public CsvColumn(CsvHeader header, CsvField field, int index)
        {
            if (header is null || field is null) throw new ArgumentNullException();

            Header = header;
            Field = field;
            Index = index;
        }
    }
}
