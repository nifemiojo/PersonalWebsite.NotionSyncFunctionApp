using PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Objects;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Block;

internal class NotionCallout
{
	[JsonPropertyName("rich_text")]
	public List<NotionRichText> RichText { get; set; }

	public NotionIcon Icon { get; set; }

	[JsonPropertyName("color")]
	public string Colour { get; set; }
}