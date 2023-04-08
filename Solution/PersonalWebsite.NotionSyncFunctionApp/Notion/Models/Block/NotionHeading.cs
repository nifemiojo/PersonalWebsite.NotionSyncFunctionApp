using PersonalWebsite.ContentSyncFunction.Notion.Models.Values;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PersonalWebsite.ContentSyncFunction.Notion.Models.Block;

internal class NotionHeading
{
	[JsonPropertyName("rich_text")]
	public List<NotionRichText> RichText { get; set; }

	[JsonPropertyName("is_toggleable")]
	public bool IsToggleable { get; set; }

	[JsonPropertyName("color")]
	public string Colour { get; set; }
}