namespace Kijitora.CsvExport
{
    public class CsvHeader
    {
        public string Name { get; set; }

        public CsvHeader(string headerName)
        {
            Name = headerName;
        }
    }
}
