﻿namespace Kijitora.CsvExport
{
    public class CsvField
    {
        public CsvField(string fieldName)
        {
            Name = fieldName;
        }

        public string Name { get; }
    }
}