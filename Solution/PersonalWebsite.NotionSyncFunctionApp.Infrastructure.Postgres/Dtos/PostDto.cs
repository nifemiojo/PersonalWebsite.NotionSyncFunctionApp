using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Dtos;

public class PostDto : BlogEntityDto
{
	public string NotionId { get; set; }

	public string Title { get; set; }

	public string Description { get; set; }

	public List<string> PlaylistNotionIds { get; set; }
}