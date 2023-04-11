using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Block;

internal class NotionBlockDto
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

	public NotionQuote Quote { get; set; }

	public NotionCode Code { get; set; }
}