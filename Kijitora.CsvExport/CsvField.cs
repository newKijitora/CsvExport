namespace Kijitora.CsvExport
{
    public class CsvField
    {
        public string Name { get; set; }

        public CsvField(string fieldName)
        {
            Name = fieldName;
        }
    }
}
