using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace Api.Model
{
    public class TitleVariations
    {

        [Name("titleId")]
        public string TitleId { get; set; }

        [Name("ordering")]
        public string Ordering { get; set; }

        [Name("title")]
        public string Title { get; set; }

        [Name("region")]
        public string Region { get; set; }

        [Name("language")]
        public string Language { get; set; }

        [Name("types")]
        public string Types { get; set; }

        [Name("attributes")]
        public string Attributes { get; set; }

        [Name("isOriginalTitle")]
        public string IsOriginalTitle { get; set; }


    }
}
