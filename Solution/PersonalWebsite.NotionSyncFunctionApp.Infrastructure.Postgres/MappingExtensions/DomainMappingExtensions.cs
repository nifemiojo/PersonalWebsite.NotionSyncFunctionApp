using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.MappingExtensions;

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