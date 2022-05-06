using CsvHelper;
using System.Globalization;
namespace Eminate
{
    public class Reader<T>
    {
        public static IEnumerable<T> ReadFile (string path)
        {
            IEnumerable<T> records;
            var myConfig = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = "\t",
                BadDataFound = null
            };

            using(var reader = new StreamReader(path))

            using(var csv = new CsvReader(reader, myConfig))
            {
                records = csv.GetRecords<T>().ToList();

            }
            return records;

        }
    }
}