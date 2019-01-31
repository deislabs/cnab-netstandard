using Newtonsoft.Json;

namespace Cnab.Bundle
{
    public class Action
    {
        [JsonProperty("modifies")]
        public bool Modifies { get; set; }
    }
}