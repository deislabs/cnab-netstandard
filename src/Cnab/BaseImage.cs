using Newtonsoft.Json;

namespace Cnab
{
    public class BaseImage
    {
        [JsonProperty("imageType")]
        public string ImageType { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("digest")]
        public string Digest { get; set; }

        [JsonProperty("size")]
        public ulong Size { get; set; }

        [JsonProperty("platform")]
        public ImagePlatform Platform { get; set; }

        [JsonProperty("mediaType")]
        public string MediaType { get; set; }
    }

    public class ImagePlatform
    {
        [JsonProperty("architecture")]
        public string Architecture { get; set; }

        [JsonProperty("os")]
        public string Os { get; set; }
    }
}