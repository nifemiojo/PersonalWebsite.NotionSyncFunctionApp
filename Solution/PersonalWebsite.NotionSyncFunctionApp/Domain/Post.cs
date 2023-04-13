using PersonalWebsite.NotionSyncFunctionApp.Common;
using System;
using System.Collections.Generic;

namespace PersonalWebsite.NotionSyncFunctionApp.Domain;

internal class Post : IDomainEntity
{
	public string NotionPageId { get; set; }

	public string Name { get; set; }

	public string Description { get; set; }

	public Iso8601FormattedDateTime CreatedAt { get; set; }

	public List<string> Playlists { get; set; }

	public Iso8601FormattedDateTime LastEditedTime { get; set; }

	public string Content { get; set; }
}