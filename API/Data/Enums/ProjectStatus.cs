using Newtonsoft.Json;

namespace Fulfill3D.API.API.Data.Enums
{
    public enum ProjectStatus
    {
        [JsonProperty("active")] active,
        [JsonProperty("draft")] draft
    }
}