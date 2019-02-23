using Newtonsoft.Json;

namespace Cnab
{
    public class Location
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("env")]
        public string EnvironmentVariable { get; set; }
    }
}