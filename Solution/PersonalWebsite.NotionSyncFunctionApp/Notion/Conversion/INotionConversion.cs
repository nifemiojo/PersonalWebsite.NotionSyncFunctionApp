using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Conversion;

public interface INotionConversion
{
	Task<string> ConvertNotionPostToHtmlString(string postTitle, List<NotionBlock> blocks);
}