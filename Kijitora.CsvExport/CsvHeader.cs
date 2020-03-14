namespace Kijitora.CsvExport
{
    public class CsvHeader
    {
        public CsvHeader(string headerName)
        {
            Name = headerName;
        }

        public string Name { get; }
    }
}
