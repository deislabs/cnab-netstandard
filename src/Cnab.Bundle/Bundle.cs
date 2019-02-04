using System;
using System.IO;
using System.Collections.Generic;

using Cnab.JsonConverters;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Cnab.Bundle
{
    public class Bundle
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("keywords")]
        public List<string> Keywords { get; set; }

        [JsonProperty("maintainers")]
        public List<Maintainer> Maintainers { get; set; }

        [JsonProperty("invocationImages")]
        public List<BaseImage> InvocationImages { get; set; }

        [JsonProperty("images")]
        public Dictionary<string, Image> Images { get; set; }

        [JsonProperty("parameters")]
        public Dictionary<string, IParameter> Parameters { get; set; }

        [JsonProperty("credentials")]
        public Dictionary<string, Location> Credentials { get; set; }

        [JsonProperty("actions")]
        public Dictionary<string, Action> Actions { get; set; }

        public static async Task<Bundle> LoadUnsignedAsync(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                var json = await reader.ReadToEndAsync();
                
                return JsonConvert.DeserializeObject<Bundle>(json);
            }
        }
    }
}