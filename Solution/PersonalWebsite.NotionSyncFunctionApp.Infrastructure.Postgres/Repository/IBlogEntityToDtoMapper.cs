using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Repository;

public interface IBlogEntityToDtoMapper
{
	List<BlogEntityDto> MapToDtos(List<BlogEntity> entitiesToUpsert);
}