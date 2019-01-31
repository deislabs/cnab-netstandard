using Newtonsoft.Json;

namespace Cnab.Bundle
{
    public class Maintainer
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}