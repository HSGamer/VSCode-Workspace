using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestExcel.Helper;

namespace TestExcel.Controllers
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

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("excel")]
        public ActionResult GetExcel()
        {
            IEnumerable<WeatherForecast> list = Get();

            // Create a stream to work with file
            var stream = new MemoryStream();

            // Create excel file
            using (var package = new ExcelPackage(stream))
            {
                // Naming the sheet
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                // Insert data
                var dataRange = workSheet.Cells[1, 1, 1, 5].CreateNewRows(5).FillDataToCells(list, (weather, cells) => {
                    cells[0].Value = weather.Date;
                    cells[0].Style.Numberformat.Format = "dd/MM/yyyy HH:mm";
                    cells[1].Value = weather.Summary;
                    cells[2].Value = weather.TemperatureC;
                    cells[3].Value = weather.TemperatureF;
                    cells[4].Formula = $"={cells[2].Address}+{cells[3].Address}";
                });
                var label = dataRange.SelectSubRange(1, 1, 5, 1);
                var value = dataRange.SelectSubRange(1, 3, 5, 3);
                var chart = workSheet.GeneratePieChart("test_chart", label, value);
                chart.Title.Text = "Test Chart";
                chart.SetSize(100);
                chart.SetPosition(300, 300);

                package.Save();
            }

            // Format response file
            stream.Position = 0;
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            // Return excel file
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
