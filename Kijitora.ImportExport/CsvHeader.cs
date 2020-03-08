namespace Kijitora.ImportExport
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
