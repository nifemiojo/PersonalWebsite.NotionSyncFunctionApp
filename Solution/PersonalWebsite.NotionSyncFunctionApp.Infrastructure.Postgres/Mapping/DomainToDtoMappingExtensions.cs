using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Dtos;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Mapping;

internal static class DomainToDtoMappingExtensions
{
	public static BlogEntityDto ToDto(this Category category)
	{
		return new CategoryDto
		{
			Name = category.Name
		};
	}

	public static BlogEntityDto ToDto(this Post post)
	{
		return new PostDto
		{
			NotionId = post.NotionId,
			Title = post.Title,
			Slug = post.Slug,
			Content = post.Content,
			IsPublished = post.IsPublished,
			PublishedOn = post.PublishedOn,
			CategoryId = post.CategoryId
		};
	}

	public static BlogEntityDto ToDto(this Playlist playlist)
	{
		return new PlaylistDto
		{
			NotionId = playlist.NotionId,
			Title = playlist.Title,
			Slug = playlist.Slug,
			Content = playlist.Content,
			IsPublished = playlist.IsPublished,
			PublishedOn = playlist.PublishedOn,
			CategoryId = playlist.CategoryId
		};
	}
}