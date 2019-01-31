using System;
using System.IO;
using System.Collections.Generic;

using Cnab.JsonConverters;
using Newtonsoft.Json;

namespace Cnab.Bundle
{
    public class Bundle
    {
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
        public Dictionary<string, Parameter> Parameters { get; set; }

        [JsonProperty("credentials")]
        public Dictionary<string, Location> Credentials { get; set; }

        [JsonProperty("actions")]
        public Dictionary<string, Action> Actions { get; set; }

        public static Bundle LoadUnsigned(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<Bundle>(json);
            }
        }
    }
}