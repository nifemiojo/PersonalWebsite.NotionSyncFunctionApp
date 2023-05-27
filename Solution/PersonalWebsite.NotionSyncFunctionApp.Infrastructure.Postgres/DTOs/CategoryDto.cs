using System.Text.Json.Serialization;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;

public class CategoryDto : BlogEntityDto
{
	[JsonPropertyName("notion_entity_id")]
	public string NotionEntityId { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; }
}