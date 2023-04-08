using System.Text.Json.Serialization;

namespace PersonalWebsite.ContentSyncFunction.Notion.Request;

internal class NotionDateFilter
{
    [JsonPropertyName("on_or_after")]
    public string OnOrAfter { get; set; }
}