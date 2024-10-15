using Newtonsoft.Json;

namespace Fulfill3D.API.API.Data.Enums
{
    public enum DevOpsType
    {
        [JsonProperty("CI")] CI,
        [JsonProperty("CD")] CD,
        [JsonProperty("CI/CD")] CICD
    }
}