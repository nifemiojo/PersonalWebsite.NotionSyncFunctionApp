using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Repository;

class BlogEntityToDtoMapper : IBlogEntityToDtoMapper
{
	public List<BlogEntityDto> MapToDtos(List<BlogEntity> entitiesToUpsert)
	{
		return entitiesToUpsert
			.Select(BlogEntityDto.Map)
			.ToList();
	}
}