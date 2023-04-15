using System.Linq;
using PersonalWebsite.NotionSyncFunctionApp.Common;
using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page.Properties.Collections;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page;

public class NotionCategoryPage : NotionPage
{
    public NotionCategoryPagePropertiesCollection Properties { get; set; }

    public override IDomainEntity MapToDomain()
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