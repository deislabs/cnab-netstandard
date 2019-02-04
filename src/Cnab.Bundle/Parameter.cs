using System;
using System.Collections.Generic;

using Cnab.JsonConverters;
using Newtonsoft.Json;

namespace Cnab.Bundle
{
    [JsonConverter(typeof(ParameterConverter))]
    public interface IParameter
    {
        string DataType { get; set; }
        bool Required { get; set; }
        ParameterMetadata Metadata { get; set; }
        Location Destination { get; set; }
        bool IsValid(object value);
    }

    public abstract class Parameter<T> : IParameter
    {
        [JsonProperty("type")]
        public string DataType { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("defaultValue")]
        public T DefaultValue { get; set; }

        [JsonProperty("allowedValues")]
        public List<T> AllowedValues { get; set; }

        [JsonProperty("metadata")]
        public ParameterMetadata Metadata { get; set; }

        [JsonProperty("destination")]
        public Location Destination { get; set; }

        public abstract bool IsValid(object value);
    }

    public class ParameterMetadata
    {
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class StringParameter : Parameter<string>
    {
        [JsonProperty("minLength")]
        public int MinimumLength { get; set; }

        [JsonProperty("maxLength")]
        public int MaximumLength { get; set; }

        public override bool IsValid(object value)
        {
            if (!(value is string))
                return false;

            var stringVal = value as string;
            if (!this.AllowedValues.Contains(stringVal))
                return false;

            if ((stringVal.Length < this.MinimumLength) || (stringVal.Length > this.MaximumLength))
                return false;

            return true;
        }
    }

    public class BoolParameter : Parameter<bool>
    {
        public override bool IsValid(object value)
        {
            if (!(value is bool))
                return false;
            
            return true;
        }
    }

    public class IntParameter : Parameter<int>
    {
        [JsonProperty("minValue")]
        public int MinimumValue { get; set; }

        [JsonProperty("maxValue")]
        public int MaximumValue { get; set; }

        public override bool IsValid(object value)
        {   
            // TODO - this is rather naive
            // it should probably handle values such as "80" or 80.0
            
            if (!(value is int))
                return false;

            var intVal = Convert.ToInt32(value);
            if (!this.AllowedValues.Contains(intVal))
                return false;

            if ((intVal < this.MinimumValue) || (intVal > this.MaximumValue))
                return false;

            return true;
        }
    }
}