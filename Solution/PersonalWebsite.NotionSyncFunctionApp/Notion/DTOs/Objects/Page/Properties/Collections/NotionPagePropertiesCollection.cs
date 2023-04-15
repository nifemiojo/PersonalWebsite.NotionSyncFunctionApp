using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page.Properties.Type;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page.Properties.Collections;

internal abstract class NotionPagePropertiesCollection
{
    [JsonPropertyName("Title")]
    public NotionPageTitleProperty Title { get; set; }
}