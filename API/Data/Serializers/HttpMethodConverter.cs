using Newtonsoft.Json;
using HttpMethod = Fulfill3D.API.API.Data.Enums.HttpMethod;

namespace Fulfill3D.API.API.Data.Serializers
{
    public class HttpMethodConverter : JsonConverter<List<HttpMethod>>
    {
        public override List<HttpMethod> ReadJson(JsonReader reader, Type objectType, List<HttpMethod> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                var methods = serializer.Deserialize<List<string>>(reader);
                var result = new List<HttpMethod>();
                foreach (var method in methods)
                {
                    if (Enum.TryParse(method, true, out HttpMethod parsedMethod))
                    {
                        result.Add(parsedMethod);
                    }
                }
                return result;
            }
            else
            {
                var method = serializer.Deserialize<string>(reader);
                if (Enum.TryParse(method, true, out HttpMethod parsedMethod))
                {
                    return new List<HttpMethod> { parsedMethod };
                }
                return new List<HttpMethod>();
            }
        }

        public override void WriteJson(JsonWriter writer, List<HttpMethod> value, JsonSerializer serializer)
        {
            var methodStrings = new List<string>();
            foreach (var method in value)
            {
                methodStrings.Add(method.ToString());
            }
            serializer.Serialize(writer, methodStrings);
        }
    }
}