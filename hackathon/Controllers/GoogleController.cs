using Google.Cloud.Language.V1;
using Google.Cloud.Speech.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hackathon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleController : ControllerBase
    {
        private readonly ILogger<GoogleController> _logger;

        public GoogleController(ILogger<GoogleController> logger)
        {
            _logger = logger;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "./keyy.json");
        }

        [HttpPost]
        public ActionResult<string> GetTranscription(string file)
        {
            try
            {
                string transcription = "";
                var bytes = Convert.FromBase64String(file);
                var speechClient = SpeechClient.Create();
                var config = new RecognitionConfig
                {
                    Encoding = RecognitionConfig.Types.AudioEncoding.Flac,
                    SampleRateHertz = 16000,
                    LanguageCode = LanguageCodes.English.UnitedStates
                };
                var audio = RecognitionAudio.FromBytes(bytes);
                var response = speechClient.Recognize(config, audio);

                foreach (var result in response.Results)
                {
                    foreach (var alternative in result.Alternatives)
                    {
                        transcription += alternative.Transcript;
                        Console.WriteLine(alternative.Transcript);
                    }
                }

                return transcription;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return Problem();
            }

        }

        [HttpPost]
        public ActionResult<List<string>> GetCategory(string text)
        {
            try
            {
                List<string> d = new List<string>();
                var classi = LanguageServiceClient.Create();
                var categories = classi.ClassifyText(new Document(Document.FromPlainText(text)));
                foreach (var category in categories.Categories)
                {
                    if (category.Confidence > 0.5)
                    {
                        d.Add(category.Name);
                    }
                    
                }

                return d;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return Problem();
            }
        }
    }
}
