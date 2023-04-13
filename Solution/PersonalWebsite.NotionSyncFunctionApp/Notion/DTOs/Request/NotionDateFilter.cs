using System.Text.Json.Serialization;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Request;

public class NotionDateFilter
{
    [JsonPropertyName("on_or_after")]
    public string OnOrAfter { get; set; }
}