using System;
using System.Collections.Generic;
using System.Linq;

namespace Kijitora.CsvExport
{
    public class CsvFormat
    {
        internal CsvField[] Fields { get; }
        internal CsvHeader[] Headers { get; }

        public CsvFormat(IEnumerable<CsvColumn> columns)
        {
            if (columns is null) throw new ArgumentNullException();
            if (!columns.Any()) throw new ArgumentException();

            CsvColumn[] sortedColumns = columns.OrderBy(column => column.Index).ToArray();
            int columnCount = sortedColumns.Length;

            Fields = new CsvField[columnCount];
            Headers = new CsvHeader[columnCount];

            for (var i = 0; i < columnCount; i++)
            {
                Fields[i] = sortedColumns[i].Field;
                Headers[i] = sortedColumns[i].Header;
            }
        }
    }
}
