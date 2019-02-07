using System.Collections.Generic;

using Newtonsoft.Json;

namespace Cnab.Bundle
{
    public class Image : BaseImage
    {
        [JsonProperty("description", Required = Required.AllowNull)]
        public string Description { get; set; }

        [JsonProperty("refs", Required = Required.Always)]
        public List<LocationReference> LocationReferences { get; set; }
    }

    public class LocationReference
    {
        [JsonProperty("path", Required = Required.Always)]
        public string Path { get; set; }

        [JsonProperty("field", Required = Required.Always)]
        public string Field { get; set; }

        [JsonProperty("mediaType", Required = Required.AllowNull)]
        public string MediaType { get; set; }
    }
}