using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Properties;
using System.Globalization;
using System;
using System.Linq;
using PersonalWebsite.NotionSyncFunctionApp.Common;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Pages;

class NotionPlaylistPageDto : NotionPageDto
{
	public NotionPlaylistProperties Properties { get; set; }

	public override IDomainEntity Map()
	{
		return new Playlist
		{
			NotionPageId = Id,
			Name = Properties.Title.Title.Single().PlainText,
			Description = Properties.Description.RichText.Single().PlainText,
			Category = Properties.Category.Relation.Single().Id,
			Posts = Properties.Posts.Relation.Select(pageReference => pageReference.Id).ToList(),
			CreatedAt = Iso8601FormattedDateTime.CreateFromValid(CreatedTime),
			LastEditedTime = Iso8601FormattedDateTime.CreateFromValid(LastEditedTime)
		};
	}
}