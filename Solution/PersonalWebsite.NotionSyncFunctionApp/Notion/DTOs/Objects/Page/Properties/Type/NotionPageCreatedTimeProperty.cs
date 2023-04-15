using System.Text.Json.Serialization;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page.Properties.Type;

internal class NotionPageCreatedTimeProperty : NotionPageProperty
{
    [JsonPropertyName("created_time")]
    public string CreatedTime { get; set; }
}