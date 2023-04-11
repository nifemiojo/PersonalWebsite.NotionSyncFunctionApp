using System;
using System.Globalization;
using System.Linq;
using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Pages;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Mapping;

internal class NotionMapper
{
	private static string NotionTimeFormat => "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK";

	public static Category Map(NotionCategoryPageDto notionCategory)
	{
		return new Category()
		{
			Id = notionCategory.Id,
			Name = notionCategory.Properties.Title.Title.Single().PlainText,
			LastEditedTime = notionCategory.LastEditedTime
		};
	}

	public static Playlist Map(NotionPlaylistPageDto notionPlaylist)
	{
		return new Playlist()
		{
			Id = notionPlaylist.Id,
			Name = notionPlaylist.Properties.Title.Title.Single().PlainText,
			Description = notionPlaylist.Properties.Description.RichText.Single().PlainText,
			Category = notionPlaylist.Properties.Category.Relation.Single().Id,
			Posts = notionPlaylist.Properties.Posts.Relation.Select(pageReference => pageReference.Id).ToList(),
			CreatedAt = DateTime.ParseExact(notionPlaylist.CreatedTime, NotionTimeFormat, CultureInfo.InvariantCulture),
			LastEditedTime = DateTime.ParseExact(notionPlaylist.LastEditedTime, NotionTimeFormat, CultureInfo.InvariantCulture)
		};
	}

	public static Post Map(NotionPostPageDto notionPost)
	{
		return new Post()
		{
			Id = notionPost.Id,
			Name = notionPost.Properties.Title.Title.Single().PlainText,
			Description = notionPost.Properties.Description.RichText.Single().PlainText,
			Playlists = notionPost.Properties.Playlists.Relation.Select(pageReference => pageReference.Id).ToList(),
			CreatedAt = DateTime.ParseExact(notionPost.CreatedTime, NotionTimeFormat, CultureInfo.InvariantCulture),
			LastEditedTime = DateTime.ParseExact(notionPost.LastEditedTime, NotionTimeFormat, CultureInfo.InvariantCulture)
		};
	}
}