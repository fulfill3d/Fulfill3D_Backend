using Fulfill3D.API.API.Data.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fulfill3D.API.API.Data.Serializers
{
    public class ContentBlockConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(ContentBlock) || objectType == typeof(List<ContentBlock>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jArray = JArray.Load(reader);
            var contentBlocks = new List<ContentBlock>();

            foreach (var jToken in jArray)
            {
                var type = jToken["type"]?.ToString();
                var data = jToken["data"];

                ContentBlock contentBlock = type switch
                {
                    "heading" => data.ToObject<HeadingBlock>(),
                    "paragraph" => data.ToObject<ParagraphBlock>(),
                    "list" => new ListBlock
                    {
                        Items = data["items"].ToObject<List<string>>(),  // Explicitly convert JArray to List<string>
                        Ordered = data["ordered"].ToObject<bool>()
                    },
                    "code" => data.ToObject<CodeBlock>(),
                    "hyperlink" => data.ToObject<HyperlinkBlock>(),
                    "quote" => data.ToObject<QuoteBlock>(),
                    _ => null
                };

                if (contentBlock != null)
                {
                    contentBlock.Type = type;  // Ensure type is set
                    contentBlock.Data = data;  // Preserve the data field
                    contentBlocks.Add(contentBlock);
                }
            }

            return contentBlocks;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var contentBlocks = value as List<ContentBlock>;
            if (contentBlocks == null)
            {
                return;
            }

            writer.WriteStartArray();

            foreach (var block in contentBlocks)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("type");
                writer.WriteValue(block.Type);

                writer.WritePropertyName("data");
                serializer.Serialize(writer, block.Data);  // Serialize the data property

                writer.WriteEndObject();
            }

            writer.WriteEndArray();
        }

        private void WriteSingleContentBlock(JsonWriter writer, ContentBlock contentBlock, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("type");
            writer.WriteValue(contentBlock.Type);

            switch (contentBlock)
            {
                case HeadingBlock headingBlock:
                    writer.WritePropertyName("text");
                    writer.WriteValue(headingBlock.Text);
                    writer.WritePropertyName("level");
                    writer.WriteValue(headingBlock.Level);
                    break;
                case ParagraphBlock paragraphBlock:
                    writer.WritePropertyName("text");
                    writer.WriteValue(paragraphBlock.Text);
                    break;
                case ImageBlock imageBlock:
                    writer.WritePropertyName("src");
                    writer.WriteValue(imageBlock.Src);
                    writer.WritePropertyName("alt");
                    writer.WriteValue(imageBlock.Alt);
                    break;
                case CodeBlock codeBlock:
                    writer.WritePropertyName("language");
                    writer.WriteValue(codeBlock.Language.ToString());
                    writer.WritePropertyName("code");
                    writer.WriteValue(codeBlock.Code);
                    break;
                case HyperlinkBlock hyperlinkBlock:
                    writer.WritePropertyName("href");
                    writer.WriteValue(hyperlinkBlock.Href);
                    writer.WritePropertyName("text");
                    writer.WriteValue(hyperlinkBlock.Text);
                    break;
                case QuoteBlock quoteBlock:
                    writer.WritePropertyName("quote");
                    writer.WriteValue(quoteBlock.Quote);
                    if (!string.IsNullOrEmpty(quoteBlock.Author))
                    {
                        writer.WritePropertyName("author");
                        writer.WriteValue(quoteBlock.Author);
                    }
                    break;
                case ListBlock listBlock:
                    writer.WritePropertyName("items");
                    serializer.Serialize(writer, listBlock.Items);
                    writer.WritePropertyName("ordered");
                    writer.WriteValue(listBlock.Ordered);
                    break;
            }

            writer.WriteEndObject();
        }
    }


}