using Fulfill3D.API.API.Data.Enums;
using Fulfill3D.API.API.Data.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fulfill3D.API.API.Data.Models
{
    public class Project
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("partitionKey")] 
        public string PartitionKey { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("demo")]
        public string Demo { get; set; }

        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("isWikiReady")]
        public bool IsWikiReady { get; set; }

        [JsonProperty("wiki")]
        public ProjectWiki Wiki { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectStatus Status { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        public static Project FromJson(string json) => JsonConvert.DeserializeObject<Project>(json);
    }

    public class SeeAlso
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class DevOps
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter<DevOpsType>))]
        public DevOpsType Type { get; set; }

        [JsonProperty("yml")]
        public string Yml { get; set; }
    }

    public class Diagram
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Architecture
    {
        [JsonProperty("description")]
        public List<string> Description { get; set; }

        [JsonProperty("diagram")]
        public Diagram Diagram { get; set; }
    }

    public class Database
    {
        [JsonProperty("description")]
        public List<string> Description { get; set; }

        [JsonProperty("diagram")]
        public Diagram Diagram { get; set; }
    }

    public class IdP
    {
        [JsonProperty("description")]
        public List<string> Description { get; set; }
    }

    public class Security
    {
        [JsonProperty("description")]
        public List<string> Description { get; set; }

        [JsonProperty("diagram")]
        public Diagram Diagram { get; set; }
    }

    public class Function
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("trigger")]
        public string Trigger { get; set; }

        [JsonProperty("method")]
        [JsonConverter(typeof(HttpMethodConverter))]
        public List<Fulfill3D.API.API.Data.Enums.HttpMethod> Method { get; set; }

        [JsonProperty("request")]
        public string Request { get; set; }

        [JsonProperty("response")]
        public string Response { get; set; }
    }

    public class Microservice
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("scalability")]
        public string Scalability { get; set; }

        [JsonProperty("devOps")]
        public List<DevOps> DevOps { get; set; }

        [JsonProperty("functions")]
        public List<Function> Functions { get; set; }
    }

    public class ProjectWiki
    {
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("features")]
        public List<string> Features { get; set; }

        [JsonProperty("technologyStack")]
        public List<string> TechnologyStack { get; set; }

        [JsonProperty("architecture")]
        public Architecture Architecture { get; set; }

        [JsonProperty("useCases")]
        public List<string> UseCases { get; set; }

        [JsonProperty("microservices")]
        public List<Microservice> Microservices { get; set; }

        [JsonProperty("devOps")]
        public List<DevOps> DevOps { get; set; }

        [JsonProperty("database")]
        public Database Database { get; set; }

        [JsonProperty("idp")]
        public IdP IdP { get; set; }

        [JsonProperty("security")]
        public Security Security { get; set; }

        [JsonProperty("frontendSrc")]
        public string FrontendSrc { get; set; }

        [JsonProperty("backendSrc")]
        public string BackendSrc { get; set; }

        [JsonProperty("seeAlso")]
        public List<SeeAlso> SeeAlso { get; set; }

        [JsonProperty("furtherReading")]
        public List<SeeAlso> FurtherReading { get; set; }
    }

}

