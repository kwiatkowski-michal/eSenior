using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using hackathon.Models;

namespace hackathon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WizytyController : ControllerBase
    {
        private readonly ILogger<WizytyController> _logger;
        private readonly HttpClient _client;


        public WizytyController(ILogger<WizytyController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
        }


        [HttpGet("/{province:decimal}/{benefit:alpha}/{locality:alpha}")]
        public async Task<ActionResult<List<WizytaModel>>> GetSpecialistVisits(int province, string benefit, string locality)
        {
            try
            {
                string link;
                if (province < 10)
                {
                    link =
                        $"https://api.nfz.gov.pl/app-itl-api/queues?province=0{province}&benefit={benefit}&locality={locality}";
                }
                else
                {
                    link =
                        $"https://api.nfz.gov.pl/app-itl-api/queues?province={province}&benefit={benefit}&locality={locality}";
                }


                List<WizytaModel> d = new List<WizytaModel>();
                var response = await _client.GetFromJsonAsync<JsonNode>(link);
                var jsonData = response!["data"]!;
                foreach (var ququq in jsonData.AsArray())
                {
                    var attr = ququq["Attributes"];
                    var wi = new WizytaModel()
                    {
                        address = attr["address"]?.ToString(),
                        benefit = attr["benefit"].ToString(),
                        locality = attr["locality"].ToString(),
                        phone = attr["phone"].ToString(),
                        place = attr["place"].ToString(),
                        provider = attr["provider"].ToString()
                    };
                    d.Add(wi);
                }

                return d;
            }

            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                return Problem();
            }
        }
    }
}
