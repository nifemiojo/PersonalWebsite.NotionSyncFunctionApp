using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Mapping;

internal static class DomainMappingExtensions
{
	public static CategoryDto ToDto(this Category category)
	{
		return new CategoryDto
		{
			Name = category.Name
		};
	}
}