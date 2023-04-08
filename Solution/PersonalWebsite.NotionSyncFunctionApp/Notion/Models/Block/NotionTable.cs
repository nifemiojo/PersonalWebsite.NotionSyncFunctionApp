using System.Collections.Generic;
using System.Text.Json.Serialization;
using PersonalWebsite.ContentSyncFunction.Notion.Models.Values;

namespace PersonalWebsite.ContentSyncFunction.Notion.Models.Block;

class NotionTable : NotionBlock
{
	[JsonPropertyName("table_width")]
	public int TableWidth { get; set; }

	[JsonPropertyName("has_column_header")]
	public bool HasColumnHeader { get; set; }

	[JsonPropertyName("has_row_header")]
	public bool HasRowHeader { get; set; }
}

class NotionTableRow : NotionBlock
{
	public List<NotionRichText> Cells { get; set; }
}