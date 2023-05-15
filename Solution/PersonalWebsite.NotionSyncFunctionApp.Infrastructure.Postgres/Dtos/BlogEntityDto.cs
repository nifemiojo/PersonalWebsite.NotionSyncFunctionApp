using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Dtos;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;

public abstract class BlogEntityDto
{
	public static BlogEntityDto Map(BlogEntity entity)
	{
		if (entity is Category)
		{
			var category = (Category)entity;
			return new CategoryDto
			{
				NotionId = category.NotionPageId,
				Name = category.Name
			};
		}
		else if (entity is Playlist)
		{
			var playlist = (Playlist)entity;
			return new PlaylistDto
			{
				NotionId = playlist.NotionPageId,
				Name = playlist.Name,
				CategoryNotionId = playlist.CategoryNotionPageId
			};
		}
		else if (entity is Post)
		{
			var post = (Post)entity;
			return new PostDto
			{
				NotionId = post.NotionPageId,
				Title = post.Name,
				Description = post.Description,
				PlaylistNotionIds = post.Playlists
			};
		}
		else
		{
			throw new NotSupportedException("Entity type not supported");
		}
	}
}