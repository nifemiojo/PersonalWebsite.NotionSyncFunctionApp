using System.Collections.Generic;
using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Misc;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block.Types;

public class NotionParagraph
{
    [JsonPropertyName("rich_text")]
    public List<NotionRichText> RichText { get; set; }

    [JsonPropertyName("color")]
    public string Colour { get; set; }

    public List<NotionBlock> Children { get; set; }
}