using Newtonsoft.Json;

namespace Cnab
{
    public class Action
    {
        [JsonProperty("modifies")]
        public bool Modifies { get; set; }
    }
}