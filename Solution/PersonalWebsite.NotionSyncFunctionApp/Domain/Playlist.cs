using System;
using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.Common;

namespace PersonalWebsite.NotionSyncFunctionApp.Domain;

internal class Playlist : IDomainEntity
{
	public string NotionPageId { get; set; }

	public string Name { get; set; }

	public string Description { get; set; }

	public Iso8601FormattedDateTime CreatedAt { get; set; }

	public string Category { get; set; }

	public List<string> Posts { get; set; }

	public Iso8601FormattedDateTime LastEditedTime { get; set; }
}