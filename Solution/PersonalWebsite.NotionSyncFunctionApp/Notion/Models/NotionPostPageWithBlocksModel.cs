using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Conversion;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models;

public class NotionPostPageWithBlocksModel : NotionPageWithBlocksModel
{
    public override IDomainEntity Map()
    {
        var domainEntity = Page.MapToDomain();

        string contentAsHtml = NotionConversion.ConvertPageBlocksToHtml(Blocks);

        (domainEntity as Post).Content = new PostContent { Html = contentAsHtml };

        return domainEntity;
    }
}