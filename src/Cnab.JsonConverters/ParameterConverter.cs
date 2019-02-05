using System;

using Cnab.Bundle;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cnab.JsonConverters
{
    public class ParameterConverter : JsonConverter
    {
        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IParameterDefinition).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var item = JObject.Load(reader);
            switch (item["type"].ToString())
            {
                case "int":
                    return populateParameter(item.CreateReader(), serializer, new IntParameter());
                    
                case "string":
                    return populateParameter(item.CreateReader(), serializer, new StringParameter());

                case "bool":
                    return populateParameter(item.CreateReader(), serializer, new BoolParameter());

                default:
                    return null;
            }
        }

        // because CanWrite returns false, it means this method should never be executed
        // it is declared here to satisfy the implementation of the JsonConverter abstract class
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private IParameterDefinition populateParameter(JsonReader reader, JsonSerializer serializer, IParameterDefinition destination)
        {
            serializer.Populate(reader, destination);
            
            return destination;
        }
    }
}