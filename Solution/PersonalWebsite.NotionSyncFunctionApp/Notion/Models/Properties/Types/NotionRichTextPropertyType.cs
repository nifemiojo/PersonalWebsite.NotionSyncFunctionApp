using System.Collections.Generic;
using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Objects;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Properties.Types;

internal class NotionRichTextPropertyType : NotionPagePropertyType
{
	[JsonPropertyName("rich_text")]
	public List<NotionRichText> RichText { get; set; }
}