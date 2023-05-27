using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Dtos;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;

public abstract class BlogEntityDto
{
	public static BlogEntityDto Map(BlogEntity entity)
	{
		return entity switch
		{
			Category category =>
				new CategoryDto
				{
					NotionEntityId = category.NotionPageId, Name = category.Name
				},

			Playlist playlist => 
				new PlaylistDto
				{
					NotionId = playlist.NotionPageId,
					Name = playlist.Name,
					CategoryNotionId = playlist.CategoryNotionPageId
				},

			Post post 
				=> new PostDto
				{
					NotionId = post.NotionPageId,
					Title = post.Name,
					Description = post.Description,
					PlaylistNotionIds = post.Playlists
				},
			_ => throw new NotSupportedException("Entity type not supported")
		};
	}
}