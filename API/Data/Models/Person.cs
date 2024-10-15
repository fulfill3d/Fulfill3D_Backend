using Newtonsoft.Json;

namespace Fulfill3D.API.API.Data.Models
{
    public class Person
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("partitionKey")]
        public string PartitionKey { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("socialMedia")]
        public List<SocialMedia> SocialMedia { get; set; }
    }
}