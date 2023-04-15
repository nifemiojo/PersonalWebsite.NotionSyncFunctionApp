using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models;

public abstract class NotionPageWithBlocksModel
{
    public NotionPage Page { get; set; }

    public List<NotionBlock> Blocks { get; set; }

    public abstract IDomainEntity Map();
}