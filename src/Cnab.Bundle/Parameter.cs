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

        bool IsValid(string value);
        bool IsValid(int value);
        bool IsValid(bool value);
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

        public abstract bool IsValid(string value);
        public abstract bool IsValid(int value);
        public abstract bool IsValid(bool value);
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

        public override bool IsValid(string value)
        {
            if (!this.AllowedValues.Contains(value))
                return false;

            if ((value.Length < this.MinimumLength) || (value.Length > this.MaximumLength))
                return false;

            return true;
        }

        public override bool IsValid(bool value)
        {
            return false;
        }

        public override bool IsValid(int value)
        {
            return false;
        }
    }

    public class BoolParameter : Parameter<bool>
    {
        public override bool IsValid(bool value)
        {
            return true;
        }

        public override bool IsValid(string value)
        {
            return false;
        }

        public override bool IsValid(int value)
        {
            return false;
        }

    }

    public class IntParameter : Parameter<int>
    {
        [JsonProperty("minValue")]
        public int MinimumValue { get; set; }

        [JsonProperty("maxValue")]
        public int MaximumValue { get; set; }

        public override bool IsValid(int value)
        {
            if (!this.AllowedValues.Contains(value))
                return false;

            if ((value < this.MinimumValue) || (value > this.MaximumValue))
                return false;

            return true;
        }

        public override bool IsValid(string value)
        {
            return false;
        }

        public override bool IsValid(bool value)
        {
            return false;
        }
    }
}