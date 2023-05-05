using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Conversion;

public interface INotionConversion
{
	Task<string> ConvertBlocksToHtml(List<NotionBlock> blocks);
}