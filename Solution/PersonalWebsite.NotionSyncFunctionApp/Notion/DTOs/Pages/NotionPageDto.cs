using System.Text.Json.Serialization;
using PersonalWebsite.NotionSyncFunctionApp.Domain;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Pages;

public abstract class NotionPageDto
{
	public string Object { get; set; }

	public string Id { get; set; }

	[JsonPropertyName("created_time")]
	public string CreatedTime { get; set; }

	[JsonPropertyName("last_edited_time")]
	public string LastEditedTime { get; set; }

	public abstract IDomainEntity Map();
}