using System;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace STG.Stage
{
    public class Schema
    {
        [JsonProperty("position")]
        [JsonConverter(typeof(ArrayToVectorConverter))]
        public Vector2 Position { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("delay")]
        public int Delay { get; set; }
    }

    public class ArrayToVectorConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Vector2);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JToken.Load(reader);
            if (obj.Type == JTokenType.Array)
            {
                var arr = (JArray)obj;
                if (arr.Count == 2 && arr.All(jtoken => jtoken.Type == JTokenType.Integer))
                    return new Vector2(arr[0].Value<int>(), arr[1].Value<int>());
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
