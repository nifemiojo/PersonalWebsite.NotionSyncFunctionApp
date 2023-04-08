using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Block;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Objects;

internal class NotionFile : NotionIcon
{
	public string Type { get; set; }
}

class NotionExternalFile : NotionFile
{
	public NotionExternalFileObject External { get; set; }
}

class NotionHostedFile : NotionFile
{
	public NotionHostedFileObject File { get; set; }
}

internal class NotionExternalFileObject
{
	public string Url { get; set; }
}

internal class NotionHostedFileObject
{
	public string Url { get; set; }

	[JsonPropertyName("expiry_time")]
	public string ExpiryTime { get; set; }
}