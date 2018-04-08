using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using FrontEndService.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FrontEndService.Controllers
{
    [Route("")]
    public class ValuesController : Controller
    {
        private readonly EndpointsConfig _endpointsConfig;

        public ValuesController(IOptions<EndpointsConfig> endpointsConfig)
        {
            _endpointsConfig = endpointsConfig.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                HttpResponseMessage result;

                using (var client = new HttpClient()) // don't use HTTP client like this in production! #demoware!
                {
                    var url = string.Concat(_endpointsConfig.Backend, "/api/values");
                    var request = new HttpRequestMessage(HttpMethod.Get, url);

                    result = await client.SendAsync(request);
                }

                if (result.IsSuccessStatusCode)
                {
                    var payload = await result.Content.ReadAsStringAsync();
                    var items = JsonConvert.DeserializeObject<List<string>>(payload);

                    // imagine some extra work with the data

                    return Ok(items);
                }
            }
            catch(Exception ex)
            {
                // handle errors
            }

            return BadRequest();           
        }        
    }
}
