namespace DatedSchedules.Predictions.Models
{
    using System;
    using System.Text.RegularExpressions;

    public class CsvFile
    {
        public CsvFile(string path)
        {
            Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(path);

            var fileNameRegex = new Regex(@"DS_LOAD_(\d{4})_(\d{2})", RegexOptions.Compiled);

            var match = fileNameRegex.Match(Name);

            if (match.Success && match.Groups.Count == 3)
            {
                Year = Convert.ToUInt32(match.Groups[1].Value);
                Month = Convert.ToUInt32(match.Groups[2].Value);
            }
            else
            {
                throw new ArgumentException("Filename could not be parsed", nameof(path));
            }
        }

        public string Path { get; }

        public string Name { get; }

        public uint Year { get; }

        public uint Month { get; }

        public static CsvFile Create(string path) => new CsvFile(path);
    }
}