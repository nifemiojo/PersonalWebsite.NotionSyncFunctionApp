using System.Collections.Generic;
using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Block;

public class NotionParagraph
{
	[JsonPropertyName("rich_text")]
	public List<NotionRichText> RichText { get; set; }

	[JsonPropertyName("color")]
	public string Colour { get; set; }

	public List<NotionBlockDto> Children { get; set; }
}