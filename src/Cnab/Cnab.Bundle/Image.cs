using System.Collections.Generic;

using Newtonsoft.Json;

namespace Cnab.Bundle
{
    public class Image : BaseImage
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("refs")]
        public List<LocationReference> LocationReferences { get; set; }
    }

    public class LocationReference
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("mediaType")]
        public string MediaType { get; set; }
    }
}