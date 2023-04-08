using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Block;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion;

internal class NotionPageContent
{
	public string PageId { get; set; }

	public List<NotionBlock> Content { get; set; }
}