using System.Text.Json.Serialization;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Request;

internal class NotionDateFilter
{
    [JsonPropertyName("on_or_after")]
    public string OnOrAfter { get; set; }
}