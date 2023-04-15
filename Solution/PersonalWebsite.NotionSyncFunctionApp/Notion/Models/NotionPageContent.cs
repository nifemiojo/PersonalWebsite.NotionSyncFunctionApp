using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models;

internal class NotionPageContent
{
    public string PageId { get; set; }

    public List<NotionBlock> Content { get; set; }
}