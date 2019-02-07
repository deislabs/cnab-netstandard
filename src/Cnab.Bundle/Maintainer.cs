using Newtonsoft.Json;

namespace Cnab.Bundle
{
    public class Maintainer
    {
        [JsonProperty("name", Required = Required.AllowNull)]
        public string Name { get; set; }

        [JsonProperty("email", Required = Required.AllowNull)]
        public string Email { get; set; }

        [JsonProperty("url", Required = Required.AllowNull)]
        public string Url { get; set; }
    }
}