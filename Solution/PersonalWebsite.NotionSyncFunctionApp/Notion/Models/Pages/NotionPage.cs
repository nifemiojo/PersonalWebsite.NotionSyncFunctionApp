using System.Text.Json.Serialization;

namespace PersonalWebsite.ContentSyncFunction.Notion.Pages;

internal abstract class NotionPage
{
    public string Object { get; set; }

    public string Id { get; set; }

    [JsonPropertyName("created_time")]
    public string CreatedTime { get; set; }
    
    [JsonPropertyName("last_edited_time")]
    public string LastEditedTime { get; set; }
}