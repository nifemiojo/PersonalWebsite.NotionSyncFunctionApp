using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Block;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models;

internal class NotionPageContent
{
    public string PageId { get; set; }

    public List<NotionBlockDto> Content { get; set; }
}