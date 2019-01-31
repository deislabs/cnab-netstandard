using System.Collections.Generic;
using Cnab.JsonConverters;
using Newtonsoft.Json;

namespace Cnab.Bundle
{
    [JsonConverter(typeof(ParameterConverter))]
    public abstract class Parameter
    {
        [JsonProperty("type")]
        public string DataType { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("metadata")]
        public ParameterMetadata Metadata { get; set; }

        [JsonProperty("destination")]
        public Location Destination { get; set; }
    }

    public class ParameterMetadata
    {
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class StringParameter : Parameter
    {
        [JsonProperty("defaultValue")]
        public string DefaultValue { get; set; }

        [JsonProperty("allowedValues")]
        public List<string> AllowedValues { get; set; }

        [JsonProperty("minLength")]
        public int MinimumLength { get; set; }

        [JsonProperty("maxLength")]
        public int MaximumLength { get; set; }
    }

    public class BoolParameter : Parameter
    {
        [JsonProperty("defaultValue")]
        public bool DefaultValue { get; set; }

        [JsonProperty("allowedValues")]
        public List<bool> AllowedValues { get; set; }
    }

    public class IntParameter : Parameter
    {
        [JsonProperty("defaultValue")]
        public int DefaultValue { get; set; }

        [JsonProperty("allowedValues")]
        public List<int> AllowedValues { get; set; }

        [JsonProperty("minValue")]
        public int MinimumValue { get; set; }

        [JsonProperty("maxValue")]
        public int MaximumValue { get; set; }
    }
}