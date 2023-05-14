using PersonalWebsite.NotionSyncFunctionApp.Domain.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Dtos;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;

public abstract class BlogEntityDto
{
	public static BlogEntityDto Map(BlogEntity entity)
	{
		if (entity is Category)
		{
			var category = (Category) entity;
			return new CategoryDto
			{
				Id = category.Id,
				Name = category.Name
			};
		}
		else if (entity is Playlist)
		{
			var playlist = (Playlist) entity;
			return new PlaylistDto
			{
				Id = playlist.Id,
				Name = playlist.Name,
				Description = playlist.Description
			};
		}
		else if (entity is Post)
		{
			var post = (Post) entity;
			return new PostDto
			{
				Id = post.Id,
				Title = post.Title,
				Content = post.Content,
				CategoryId = post.CategoryId,
				PlaylistId = post.PlaylistId
			};
		}
		else
		{
			throw new NotSupportedException("Entity type not supported");
		}
	}