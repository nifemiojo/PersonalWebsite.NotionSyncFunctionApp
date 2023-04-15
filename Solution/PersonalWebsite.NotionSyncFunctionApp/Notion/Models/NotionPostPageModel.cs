using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Conversion;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models;

public class NotionPostPageModel : NotionPageModel
{
    public override IDomainEntity Map()
    {
        var domainEntity = Properties.Map();

        string contentAsHtml = NotionConversion.ConvertPageBlocksToHtml(Content);

        (domainEntity as Post).Content = new PostContent { Html = contentAsHtml };

        return domainEntity;
    }
}