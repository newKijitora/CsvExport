namespace Kijitora.ImportExport
{
    public class CsvColumn
    {
        public string Field { get; } = "";
        public string Header { get; } = "";
        public int Index { get; } = 0;

        public CsvColumn(CsvHeader header, CsvField field, int index)
        {
            Header = header.Name;
            Field = field.Name;
            Index = index;
        }
    }
}
