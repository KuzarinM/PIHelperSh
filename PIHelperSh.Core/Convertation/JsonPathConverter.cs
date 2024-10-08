﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;

namespace PIHelperSh.Core.Convertation
{
    /// <summary>
    /// Данный конвертер позволяет расширить стандартные возможности анотации JsonProperty так, чтобы можно было указывать вложенные поля напрмер(data.value)
    /// </summary>
    public class JsonPathConverter: JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            object targetObj = Activator.CreateInstance(objectType);

            foreach (PropertyInfo prop in objectType.GetProperties().Where(p => p.CanRead && p.CanWrite))
            {
                var att = prop.GetCustomAttribute<JsonPropertyAttribute>();

                var jsonPath = (att != null ? att.PropertyName : prop.Name);

                if (jsonPath != null)
                {
                    JToken? token = jo.SelectToken(jsonPath);

                    if (token == null)
                        token = jo.SelectToken($"{char.ToLowerInvariant(jsonPath[0])}{jsonPath.Substring(1)}");

                    if (token != null && token.Type != JTokenType.Null)
                    {
                        object value = token.ToObject(prop.PropertyType, serializer);
                        prop.SetValue(targetObj, value, null);
                    }
                }
            }

            return targetObj;
        }

        public override bool CanConvert(Type objectType)
        {
            // CanConvert is not called when [JsonConverter] attribute is used
            return false;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
