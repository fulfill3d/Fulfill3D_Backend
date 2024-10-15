using Fulfill3D.API.API.Data.Enums;
using Fulfill3D.API.API.Data.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fulfill3D.API.API.Data.Serializers
{
    public class ProjectConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Project).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            var project = new Project
            {
                Id = jsonObject["id"].Value<string>(),
                PartitionKey = jsonObject["partitionKey"].Value<string>(),
                Name = jsonObject["name"].Value<string>(),
                Description = jsonObject["description"].Value<string>(),
                Demo = jsonObject["demo"]?.Value<string>(),
                Src = jsonObject["src"]?.Value<string>(),
                IsWikiReady = jsonObject["isWikiReady"].Value<bool>(),
                Wiki = jsonObject["wiki"]?.ToObject<ProjectWiki>(),
                ImageUrl = jsonObject["imageUrl"].Value<string>(),
                Status = Enum.Parse<ProjectStatus>(jsonObject["status"].Value<string>(), true),
                Tags = jsonObject["tags"].ToObject<List<string>>()
            };

            return project;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var project = (Project)value;
            writer.WriteStartObject();

            writer.WritePropertyName("id");
            writer.WriteValue(project.Id);

            writer.WritePropertyName("partitionKey");
            writer.WriteValue(project.PartitionKey);

            writer.WritePropertyName("name");
            writer.WriteValue(project.Name);

            writer.WritePropertyName("description");
            writer.WriteValue(project.Description);

            writer.WritePropertyName("demo");
            writer.WriteValue(project.Demo);

            writer.WritePropertyName("src");
            writer.WriteValue(project.Src);

            writer.WritePropertyName("isWikiReady");
            writer.WriteValue(project.IsWikiReady);

            writer.WritePropertyName("wiki");
            serializer.Serialize(writer, project.Wiki);

            writer.WritePropertyName("imageUrl");
            writer.WriteValue(project.ImageUrl);

            writer.WritePropertyName("status");
            writer.WriteValue(project.Status.ToString().ToLower());

            writer.WritePropertyName("tags");
            serializer.Serialize(writer, project.Tags);

            writer.WriteEndObject();
        }
    }

}