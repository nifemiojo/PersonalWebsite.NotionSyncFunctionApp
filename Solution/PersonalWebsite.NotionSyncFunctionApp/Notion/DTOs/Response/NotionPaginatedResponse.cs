using System.Collections.Generic;
using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Response;

public class NotionPaginatedResponse<TNotionObject> where TNotionObject : BaseNotionObject
{
    public string Object { get; set; }

    public List<TNotionObject> Results { get; set; }

    [JsonPropertyName("next_cursor")]
    public string NextCursor { get; set; }

    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }

    public string Type { get; set; }
}