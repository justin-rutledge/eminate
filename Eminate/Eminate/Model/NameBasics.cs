using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace Api.Model
{
    public class NameBasics
    {

        [Name("nconst")]
        public string NameKey { get; set; }

        [Name("primaryName")]
        public string PrimaryName { get; set; }

        [Name("birthYear")]
        public string BirthYear { get; set; }

        [Name("deathYear")]
        public string DeathYear { get; set; }

        [Name("primaryProfession")]
        public string PrimaryProfession { get; set; }

        [Name("knownForTitles")]
        public string KnownForTitles { get; set; }

    }
}
