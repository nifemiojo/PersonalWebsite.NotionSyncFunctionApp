using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block.Types;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Misc;

public class NotionFile : NotionIcon
{
    public string Type { get; set; }
    public NotionHostedFileObject File { get; set; }
    public NotionExternalFileObject External { get; set; }
}

public class NotionExternalFileObject
{
    public string Url { get; set; }
}

public class NotionHostedFileObject
{
    public string Url { get; set; }

    [JsonPropertyName("expiry_time")]
    public string ExpiryTime { get; set; }
}