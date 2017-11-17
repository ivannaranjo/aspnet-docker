using Newtonsoft.Json;

namespace Test_1_1.Controllers
{
    public class LoggingData
    {
        [JsonProperty("log_name")]
        public string LogName { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }
    }
}
