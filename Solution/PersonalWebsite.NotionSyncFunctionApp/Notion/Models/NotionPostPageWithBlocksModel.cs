using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Conversion;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models;

public class NotionPostPageWithBlocksModel : NotionPageWithBlocksModel
{
    public override BlogEntity Map(PostContent postContent)
    {
        var domainEntity = Page.MapToDomain();

        (domainEntity as Post).Content = postContent;

        return domainEntity;
    }
}