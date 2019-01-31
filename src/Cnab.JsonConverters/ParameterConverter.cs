using System;

using Cnab.Bundle;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cnab.JsonConverters
{
    public class ParameterConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Parameter).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var item = JObject.Load(reader);
            switch (item["type"].ToString())
            {
                case "int":
                    var intParam = new IntParameter();
                    return populateParameter(item.CreateReader(), serializer, intParam);                
                case "string":
                    var stringParam = new StringParameter();
                    return populateParameter(item.CreateReader(), serializer, stringParam);

                case "bool":
                    var boolParam = new BoolParameter();
                    return populateParameter(item.CreateReader(), serializer, boolParam);

                default:
                    return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // TODO: implement serializer
            throw new NotImplementedException();
        }

        private Parameter populateParameter(JsonReader reader, JsonSerializer serializer, Parameter destination)
        {
            serializer.Populate(reader, destination);
            return destination;
        }
    }
}