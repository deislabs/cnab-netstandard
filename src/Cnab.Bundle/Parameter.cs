using System;
using System.Collections.Generic;

using Cnab.JsonConverters;
using Newtonsoft.Json;

namespace Cnab.Bundle
{
    [JsonConverter(typeof(ParameterConverter))]
    public interface IParameterDefinition
    {
        string DataType { get; set; }
        bool Required { get; set; }
        ParameterMetadata Metadata { get; set; }
        Location Destination { get; set; }

        ValidationResult IsValid(string value);
        ValidationResult IsValid(int value);
        ValidationResult IsValid(bool value);
    }

    public abstract class Parameter<T> : IParameterDefinition
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

        public abstract ValidationResult IsValid(string value);
        public abstract ValidationResult IsValid(int value);
        public abstract ValidationResult IsValid(bool value);
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

        public override ValidationResult IsValid(string value)
        {
            if ((this.AllowedValues.Count != 0) && (!this.AllowedValues.Contains(value)))
            {
                return new ValidationResult()
                {
                    Succeeded = false,
                    ValidationError = $"value {value} is not in the set of allowed values for this parameter"
                };
            }

            if ((this.MinimumLength != 0) && (value.Length < this.MinimumLength))
            {
                return new ValidationResult()
                {
                    Succeeded = false,
                    ValidationError = $"value {value} is too short: minimum length is ${this.MinimumLength}"
                };
            }

            if ((this.MaximumLength != 0) && (value.Length > this.MaximumLength))
            {
                return new ValidationResult()
                {
                    Succeeded = false,
                    ValidationError = $"value {value} is too long: maximum length is ${this.MaximumLength}"
                };
            }

            return new ValidationResult()
            {
                Succeeded = true
            };
        }

        public override ValidationResult IsValid(bool value)
        {
            return new ValidationResult()
            {
                Succeeded = false,
                ValidationError = $"value ${value} is not string"
            };
        }

        public override ValidationResult IsValid(int value)
        {
            return new ValidationResult()
            {
                Succeeded = false,
                ValidationError = $"value {value} is not string"
            };
        }
    }

    public class BoolParameter : Parameter<bool>
    {
        public override ValidationResult IsValid(bool value)
        {
            return new ValidationResult()
            {
                Succeeded = true,
            };
        }

        public override ValidationResult IsValid(string value)
        {
            return new ValidationResult()
            {
                Succeeded = false,
                ValidationError = $"value {value} is not a boolean"
            };
        }

        public override ValidationResult IsValid(int value)
        {
            return new ValidationResult()
            {
                Succeeded = false,
                ValidationError = $"value {value} is not a boolean"
            };
        }
    }

    public class IntParameter : Parameter<int>
    {
        [JsonProperty("minValue")]
        public int MinimumValue { get; set; }

        [JsonProperty("maxValue")]
        public int MaximumValue { get; set; }

        public override ValidationResult IsValid(int value)
        {
            if ((this.AllowedValues.Count != 0) && (!this.AllowedValues.Contains(value)))
            {
                return new ValidationResult()
                {
                    Succeeded = false,
                    ValidationError = $"value {value} is not in the set of allowed values for this parameter"
                };
            }

            if ((this.MinimumValue != 0) && (value < this.MinimumValue))
            {
                return new ValidationResult()
                {
                    Succeeded = false,
                    ValidationError = $"value {value} is too low: minimum value is ${this.MinimumValue}"
                };
            }

            if ((this.MaximumValue != 0) && (value > this.MaximumValue))
            {
                return new ValidationResult()
                {
                    Succeeded = false,
                    ValidationError = $"value {value} is too high: maximum value is ${this.MaximumValue}"
                };
            }

            return new ValidationResult()
            {
                Succeeded = true
            };
        }

        public override ValidationResult IsValid(bool value)
        {
            return new ValidationResult()
            {
                Succeeded = false,
                ValidationError = $"value ${value} is not integer"
            };
        }

        public override ValidationResult IsValid(string value)
        {
            return new ValidationResult()
            {
                Succeeded = false,
                ValidationError = $"value {value} is not integer"
            };
        }
    }
}