using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AccuWeather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private string BaseAddress = "http://dataservice.accuweather.com/";
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
        [HttpGet("GetCurrnetCondition")]
        public async Task<ActionResult> GetCurrnetConditionAsync(int cityKey)
        {
            object ObjResponse = new object();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetDepartments using HttpClient
                HttpResponseMessage Res = await client.GetAsync("/currentconditions/v1/" + cityKey + "?apikey=ft517SgeQZZB5lc8GgokFATFz2jZvm3U");
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    ObjResponse = Res.Content.ReadAsStringAsync().Result;
                }
            }
            return Ok(ObjResponse);
            //string contentRootPath = Environment.CurrentDirectory;
            //return Ok(System.IO.File.ReadAllText(contentRootPath + "/Data/currentConditions.json"));
        }
        [HttpGet("GetDailyForecasts")]
        public async Task<ActionResult> GetDailyForecastsAsync(int cityKey, bool metric, int numDays=5)
        {
            object ObjResponse = new object();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetDepartments using HttpClient
                HttpResponseMessage Res;
                if (metric == true)
                    Res = await client.GetAsync("/forecasts/v1/daily/" + numDays + "day/" + cityKey + "?apikey=ft517SgeQZZB5lc8GgokFATFz2jZvm3U&metric=true");
                else
                    Res = await client.GetAsync("/forecasts/v1/daily/" + numDays + "day/" + cityKey + "?apikey=ft517SgeQZZB5lc8GgokFATFz2jZvm3U&metric=false");
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    ObjResponse = Res.Content.ReadAsStringAsync().Result;
                }
            }
            return Ok(ObjResponse);
            //string contentRootPath = Environment.CurrentDirectory;
            //return Ok(System.IO.File.ReadAllText(contentRootPath + "/Data/5days.json"));
        }
    }
}
