using Newtonsoft.Json;

namespace Fulfill3D.API.API.Data.Enums
{
    public enum BlogPostStatus
    {
        [JsonProperty("published")] published,
        [JsonProperty("draft")] draft
    }
}