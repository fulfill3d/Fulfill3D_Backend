using Newtonsoft.Json;

namespace Fulfill3D.API.API.Data.Models
{
    public class Company
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("mission")]
        public string Mission { get; set; }

        [JsonProperty("descriptions")]
        public List<string> Descriptions { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("logoUrl")]
        public string LogoUrl { get; set; }

        [JsonProperty("socialMedia")]
        public List<SocialMedia> SocialMedia { get; set; }
    }
}