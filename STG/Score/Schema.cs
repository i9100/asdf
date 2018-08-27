using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace STG.Score
{
    public class Schema
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("score", Order = 1)]
        public int Score { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }
}
