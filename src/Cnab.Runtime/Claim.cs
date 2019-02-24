using System;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using NUlid;
using Cnab;

namespace Cnab.Runtime
{
    public class Claim
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("revision")]
        public string Revision { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("modified")]
        public DateTime Modified { get; set; }

        [JsonProperty("bundle")]
        public Bundle Bundle { get; set; }

        [JsonProperty("result")]
        public Result Result { get; set; }

        [JsonProperty("parameters")]
        public Dictionary<string, IParameterDefinition> Parameters { get; set; }

        public Claim() { }
        public Claim(string name)
        {
            if (!IsValidClaimName(name))
            {
                throw new ArgumentException($"invalid name: {name}. Names must be [a-zA-Z0-9-_]+");
            }

            var now = DateTime.Now;

            Name = name;
            Revision = Ulid.NewUlid().ToString();
            Created = now;
            Modified = now;
            Result = new Result()
            {
                Action = Action.Unknown,
                Status = Status.Unknown
            };
            Parameters = new Dictionary<string, IParameterDefinition>();
        }
        public void Update(string action, string status)
        {
            this.Result.Action = action;
            this.Result.Status = status;
            this.Modified = DateTime.Now;
            this.Revision = Ulid.NewUlid().ToString();
        }

        public static bool IsValidClaimName(string name)
        {
            var pattern = "^[a-zA-Z0-9_-]+$";
            var rgx = new Regex(pattern);

            return rgx.IsMatch(name);
        }
    }

    public class Result
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}