using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace Eminate.Model
{
    public class TitleCrew
    {

        [Name("tconst")]
        public string TitleKey { get; set; } //tconst

        [Name("directors")]
        public string Directors { get; set; }

        [Name("writers")]
        public string Writers { get; set; }

    }
}
