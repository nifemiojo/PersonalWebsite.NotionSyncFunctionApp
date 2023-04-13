using PersonalWebsite.NotionSyncFunctionApp.Common;
using System.Collections.Generic;

namespace PersonalWebsite.NotionSyncFunctionApp.Domain;

internal class Category : IDomainEntity
{
	public string NotionPageId { get; set; }

	public string Name { get; set; }

	public Iso8601FormattedDateTime CreatedAt { get; set; }

	public Iso8601FormattedDateTime LastEditedTime { get; set; }
}