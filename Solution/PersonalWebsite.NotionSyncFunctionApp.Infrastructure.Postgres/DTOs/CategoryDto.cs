namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;

public class CategoryDto : BlogEntityDto
{
	public string NotionId { get; set; }

	public string Name { get; set; }
}