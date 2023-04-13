using System.Linq;
using PersonalWebsite.NotionSyncFunctionApp.Common;
using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Properties;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Pages;

class NotionCategoryPageDto : NotionPageDto
{
	public NotionCategoryProperties Properties { get; set; }

	public override IDomainEntity Map()
	{
		return new Category
		{
			NotionPageId = Id,
			Name = Properties.Title.Title.Single().PlainText,
			CreatedAt = Iso8601FormattedDateTime.CreateFromValid(CreatedTime),
			LastEditedTime = Iso8601FormattedDateTime.CreateFromValid(LastEditedTime)
		};
	}
}