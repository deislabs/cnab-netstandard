using Newtonsoft.Json;

namespace Cnab.Bundle
{
    public class Action
    {
        [JsonProperty("modifies", Required = Required.AllowNull)]
        public bool Modifies { get; set; }
    }
}