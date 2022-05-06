using Eminate.Model;
using Microsoft.AspNetCore.Mvc;

namespace Eminate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetTitleCrew")]
        public IEnumerable<TitleCrew> GetTitleCrew()
        {
            return Reader<TitleCrew>.ReadFile("C:\\Users\\amykh\\Desktop\\TitleCrew.tsv");
        }

        [HttpGet("GetNameBasics")]
        public IEnumerable<NameBasics> GetNameBasics()
        {
            return Reader<NameBasics>.ReadFile("C:\\Users\\amykh\\Desktop\\NameBasics.tsv");
        }

        [HttpGet("GetTitleAkas")]
        public IEnumerable<TitleVariations> GetTitleAkas()
        {
            return Reader<TitleVariations>.ReadFile("C:\\Users\\amykh\\Desktop\\TitleAkas.tsv");
        }

        [HttpGet("GetCombinedFields")]
        public IActionResult GetCombinedFields()
        {
            //var titleAkas = Reader<TitleVariations>.ReadFile("C:\\Users\\amykh\\Desktop\\TitleAkas.tsv");
            var nameBasics = Reader<NameBasics>.ReadFile("C:\\Users\\amykh\\Desktop\\NameBasics.tsv");
            var titleCrew = Reader<TitleCrew>.ReadFile("C:\\Users\\amykh\\Desktop\\TitleCrew.tsv");
            var combinedData = titleCrew
                .Take(10)
                .GroupJoin(
                    Reader<TitleVariations>.ReadFile("C:\\Users\\amykh\\Desktop\\TitleAkas.tsv"),
                    tcrew => tcrew.TitleKey,
                    taka => taka.TitleId,
                    (tcrew, taka) =>  new { tcrew, taka })
                //.GroupJoin(
                //    nameBasics,
                //    combined => combined.tcrew.Directors,
                //    basic => basic.NameKey,
                //    (combined,basic) => new {combined.taka, combined.tcrew, basic})
                .Take(10)
                .ToList();
            return Ok(combinedData);

        }

    }
}