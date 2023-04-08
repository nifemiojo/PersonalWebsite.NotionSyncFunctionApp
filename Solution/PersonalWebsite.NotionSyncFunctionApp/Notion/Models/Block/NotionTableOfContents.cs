using System.Text.Json.Serialization;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Block;

internal class NotionTableOfContents
{
	[JsonPropertyName("color")]
	public string Colour { get; set; }
}