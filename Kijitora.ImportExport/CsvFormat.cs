using System;
using System.Collections.Generic;
using System.Linq;

namespace Kijitora.ImportExport
{
    public class CsvFormat
    {
        internal string[] Fields { get; }
        internal string[] Headers { get; }

        public CsvFormat(IEnumerable<CsvColumn> columns)
        {
            if (columns is null)
            {
                throw new ArgumentNullException();
            }

            var sortedColumns = columns.OrderBy(column => column.Index).ToArray();
            var columnCount = sortedColumns.Length;

            Fields = new string[columnCount];
            Headers = new string[columnCount];

            for (var i = 0; i < columnCount; i++)
            {
                Fields[i] = sortedColumns[i].Field;
                Headers[i] = sortedColumns[i].Header;
            }
        }
    }
}
