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
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("version", Required = Required.Always)]
        public string Version { get; set; }

        [JsonProperty("description", Required = Required.AllowNull)]
        public string Description { get; set; }

        [JsonProperty("keywords", Required = Required.AllowNull)]
        public List<string> Keywords { get; set; }

        [JsonProperty("maintainers", Required = Required.AllowNull)]
        public List<Maintainer> Maintainers { get; set; }

        [JsonProperty("invocationImages", Required = Required.Always)]
        public List<BaseImage> InvocationImages { get; set; }

        [JsonProperty("images", Required = Required.Always)]
        public Dictionary<string, Image> Images { get; set; }

        [JsonProperty("parameters", Required = Required.AllowNull)]
        public Dictionary<string, IParameterDefinition> Parameters { get; set; }

        [JsonProperty("credentials", Required = Required.AllowNull)]
        public Dictionary<string, Location> Credentials { get; set; }

        [JsonProperty("actions", Required = Required.AllowNull)]
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