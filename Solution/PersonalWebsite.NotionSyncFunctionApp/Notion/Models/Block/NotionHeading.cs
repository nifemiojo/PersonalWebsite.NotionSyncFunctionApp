using PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Objects;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Block;

internal class NotionHeading
{
	[JsonPropertyName("rich_text")]
	public List<NotionRichText> RichText { get; set; }

	[JsonPropertyName("is_toggleable")]
	public bool IsToggleable { get; set; }

	[JsonPropertyName("color")]
	public string Colour { get; set; }
}