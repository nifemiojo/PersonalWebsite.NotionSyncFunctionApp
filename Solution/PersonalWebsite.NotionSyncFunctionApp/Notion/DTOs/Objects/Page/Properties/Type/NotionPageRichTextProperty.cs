using System.Collections.Generic;
using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Misc;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page.Properties.Type;

public class NotionPageRichTextProperty : NotionPageProperty
{
    [JsonPropertyName("rich_text")]
    public List<NotionRichText> RichText { get; set; }
}