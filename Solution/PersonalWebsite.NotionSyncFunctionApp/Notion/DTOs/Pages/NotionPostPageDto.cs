using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Properties;
using System.Globalization;
using System;
using System.Linq;
using PersonalWebsite.NotionSyncFunctionApp.Common;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Pages;

class NotionPostPageDto : NotionPageDto
{
	public NotionPostProperties Properties { get; set; }

	public override IDomainEntity Map()
	{
		return new Post
		{
			NotionPageId = Id,
			Name = Properties.Title.Title.Single().PlainText,
			Description = Properties.Description.RichText.Single().PlainText,
			Playlists = Properties.Playlists.Relation.Select(pageReference => pageReference.Id).ToList(),
			CreatedAt = Iso8601FormattedDateTime.CreateFromValid(CreatedTime),
			LastEditedTime = Iso8601FormattedDateTime.CreateFromValid(LastEditedTime),
		};
	}
}