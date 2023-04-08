using PersonalWebsite.ContentSyncFunction.Notion.Models.Objects;
using System.Text.Json.Serialization;

namespace PersonalWebsite.ContentSyncFunction.Notion.Models.Block;

internal class NotionBlock
{
	public string Id { get; set; }

	[JsonPropertyName("has_children")]
	public string HasChildren { get; set; }

	// Remember to handle divider type based on this prop
	public string Type { get; set; }

	// Block Specific Types
	public NotionParagraph Paragraph { get; set; }

	[JsonPropertyName("heading_1")]
	public NotionHeading HeadingOne { get; set; }

	[JsonPropertyName("heading_2")]
	public NotionHeading HeadingTwo { get; set; }

	[JsonPropertyName("heading_3")]
	public NotionHeading HeadingThree { get; set; }

	[JsonPropertyName("bulleted_list_item")]
	public NotionBulletedListItem BulletedListItem { get; set; }

	[JsonPropertyName("numbered_list_item")]
	public NotionNumberedListItem NumberedListItem { get; set; }

	public NotionFile Image { get; set; }

	[JsonPropertyName("child_page")]
	public NotionChildPage ChildPage { get; set; }

	public NotionCallout Callout { get; set; }

	public NotionTable Table { get; set; }

	[JsonPropertyName("table_row")]
	public NotionTableRow TableRow { get; set; }

	public NotionQuote Quote { get; set; }

	public NotionCode Code { get; set; }

	public NotionToggle Toggle { get; set; }

	[JsonPropertyName("table_of_contents")]
	public NotionTableOfContents TableOfContents { get; set; }

	[JsonPropertyName("column_list")]
	public object ColumnList { get; set; }

	public object Column { get; set; }
}