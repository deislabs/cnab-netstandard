using Newtonsoft.Json;

namespace Cnab.Bundle
{
    public class Location
    {
        [JsonProperty("path", Required = Required.Always)]
        public string Path { get; set; }

        [JsonProperty("env", Required = Required.Always)]
        public string EnvironmentVariable { get; set; }
    }
}