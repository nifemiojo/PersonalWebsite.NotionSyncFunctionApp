using System.Collections.Generic;
using PersonalWebsite.ContentSyncFunction.Notion.Models.Block;

namespace PersonalWebsite.ContentSyncFunction.Notion;

internal class NotionPageContent
{
	public string PageId { get; set; }

	public List<NotionBlock> Content { get; set; }
}