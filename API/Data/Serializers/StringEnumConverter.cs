using Newtonsoft.Json;

namespace Fulfill3D.API.API.Data.Serializers
{
    public class StringEnumConverter<TEnum> : JsonConverter where TEnum : struct, Enum
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString()?.ToLower()); // Write value as lowercase string
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return default(TEnum); // Handle null value by returning the default enum value
            }

            var enumText = reader.Value.ToString();
            if (Enum.TryParse<TEnum>(enumText, ignoreCase: true, out var result))
            {
                return result;
            }

            throw new JsonSerializationException($"Unable to parse '{enumText}' to enum {typeof(TEnum).Name}");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TEnum);
        }
    }


}