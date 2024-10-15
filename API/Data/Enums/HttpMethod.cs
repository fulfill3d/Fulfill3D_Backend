using Newtonsoft.Json;

namespace Fulfill3D.API.API.Data.Enums
{
    public enum HttpMethod
    {
        [JsonProperty("GET")] GET,
        [JsonProperty("POST")] POST,
        [JsonProperty("PUT")] PUT,
        [JsonProperty("DELETE")] DELETE,
        [JsonProperty("PATCH")] PATCH
    }
}