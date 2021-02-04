using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
    public class LocationController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<LocationController> _logger;

        public LocationController(ILogger<LocationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync(string query)
        {
            //string contentRootPath = Environment.CurrentDirectory;
            ////List<object> cities = JsonConvert.DeserializeObject<List<object>>(System.IO.File.ReadAllText(contentRootPath + "/Data/autocomplete.json"));
            //return Ok(System.IO.File.ReadAllText(contentRootPath + "/Data/autocomplete.json"));
            ////var data = File.ReadAllText("json1.json");
            ////var auctions = JsonConvert.DeserializeObject<IEnumerable<object>>([ { foo = "bar", baz = "Blech" } ]);
            ////return Json(auctions);



            object ObjResponse = new object();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri("http://dataservice.accuweather.com/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetDepartments using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("/locations/v1/cities/autocomplete?apikey=ft517SgeQZZB5lc8GgokFATFz2jZvm3U&q=" + query);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    ObjResponse = Res.Content.ReadAsStringAsync().Result;
                }
            }
            return Ok(ObjResponse);
        }
    }
}
