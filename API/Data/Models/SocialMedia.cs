using Fulfill3D.API.API.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fulfill3D.API.API.Data.Models
{
    public class SocialMedia
    {
        [JsonProperty("platform")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SocialMediaPlatform Platform { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}