using Eminate.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

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
            var timer = Stopwatch.StartNew();

            var titleAkasPath = "Data/titleAkas.tsv";
            var nameBasicsPath = "Data/nameBasics.tsv";
            var titleCrewPath = "Data/titleCrew.tsv";

            var titleAkas = Reader<TitleVariations>.ReadFile(titleAkasPath);
            timer.Stop();
            var titleAkasImportTime = timer.Elapsed;
            _logger.LogInformation("Completed loading {titleAkasCount} rows of titleAKas data after {timer} seconds",
                titleAkas.Count(), titleAkasImportTime.TotalSeconds);

            timer.Start();
            var nameBasics = Reader<NameBasics>.ReadFile(nameBasicsPath);
            timer.Stop();
            var nameBasicsImportTime = timer.Elapsed - titleAkasImportTime;
            _logger.LogInformation("Completed loading {nameBasicsCount} rows of nameBasics data after {timer} seconds",
                nameBasics.Count(), nameBasicsImportTime.TotalSeconds);

            timer.Start();
            var titleCrew = Reader<TitleCrew>.ReadFile(titleCrewPath);
            timer.Stop();
            var titleCrewImportTime = timer.Elapsed - (titleAkasImportTime + nameBasicsImportTime);
            _logger.LogInformation("Completed loading {titleCrewCount} rows of titleCrew data after {timer} seconds",
                titleCrew.Count(), titleCrewImportTime.TotalSeconds);

            var totalImportTime = timer.Elapsed;
            _logger.LogInformation("Completed all imports after {elapsed} seconds",
                totalImportTime.TotalSeconds);

            timer.Start();
            var combinedData = titleCrew
                //.Take(10)
                .GroupJoin(
                    titleAkas,
                    tcrew => tcrew.TitleKey,
                    taka => taka.TitleId,
                    (tcrew, taka) =>  new { tcrew, taka })
                .GroupJoin(
                    nameBasics,
                    combined => combined.tcrew.Directors,
                    basic => basic.NameKey,
                    (combined,basic) => new {combined.taka, combined.tcrew, basic})
                .ToList();

            timer.Stop();

            var transformationTime = timer.Elapsed - totalImportTime;
            _logger.LogInformation("Completed transformations after {elapsed} seconds",
                transformationTime.TotalSeconds);

            _logger.LogInformation("Completed all imports and data transformations after {elapsed} seconds",
                timer.Elapsed.TotalSeconds);

            return Ok(combinedData.Take(10));

        }

    }
}