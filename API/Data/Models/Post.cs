using Fulfill3D.API.API.Data.Enums;
using Fulfill3D.API.API.Data.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fulfill3D.API.API.Data.Models
{
    public class Post
    {
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("partitionKey")] public string PartitionKey { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("slug")] public string Slug { get; set; }
        [JsonProperty("author")] public string Author { get; set; }
        [JsonProperty("tags")] public List<string> Tags { get; set; }
        [JsonProperty("excerpt")] public string Excerpt { get; set; }
        [JsonProperty("image")] public string Image { get; set; }
        [JsonProperty("status")] [JsonConverter(typeof(StringEnumConverter))] public BlogPostStatus Status { get; set; }

        [JsonProperty("contentBlocks")]
        [JsonConverter(typeof(ContentBlockConverter))]
        public List<ContentBlock> ContentBlocks { get; set; } = new();
    }

    public abstract class ContentBlock
    {
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("data")] public dynamic Data { get; set; }
    }

    public class HeadingBlock : ContentBlock
    {
        public string Text => Data?.text ?? string.Empty;
        public int Level => Data?.level ?? 1;
    }

    public class ParagraphBlock : ContentBlock
    {
        public string Text => Data?.text ?? string.Empty;
    }

    public class ImageBlock : ContentBlock
    {
        public string Src => Data?.src ?? "default-image.png";
        public string Alt => Data?.alt ?? "Image description";
    }

    public class CodeBlock : ContentBlock
    {
        public string Language => Data?.language ?? "TypeScript";
        public string Code => Data?.code ?? "// No code provided";
    }

    public class HyperlinkBlock : ContentBlock
    {
        public string Href => Data?.href ?? "#";
        public string Text => Data?.text ?? "Click here";
    }

    public class QuoteBlock : ContentBlock
    {
        public string Quote => Data?.quote ?? "No quote available";
        public string Author => Data?.author ?? string.Empty;
    }

    public class ListBlock : ContentBlock
    {
        [JsonProperty("items")]
        public List<string> Items { get; set; } = new List<string>();

        [JsonProperty("ordered")]
        public bool Ordered { get; set; }
    }

}
