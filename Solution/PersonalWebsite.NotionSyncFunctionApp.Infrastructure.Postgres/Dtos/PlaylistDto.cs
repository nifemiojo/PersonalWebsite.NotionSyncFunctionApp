using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Dtos;

public class PlaylistDto : BlogEntityDto
{
	public string NotionId { get; set; }

	public string Name { get; set; }

	public string Description { get; set; }

	public string CategoryNotionId { get; set; }

	public List<string> PostNotionEntityIds { get; set; }
}