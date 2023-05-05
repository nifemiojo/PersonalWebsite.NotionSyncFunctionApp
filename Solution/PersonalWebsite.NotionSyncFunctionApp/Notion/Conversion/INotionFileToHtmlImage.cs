using System.Threading.Tasks;
using PersonalWebsite.NotionSyncFunctionApp.HTML;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Conversion;

public interface INotionFileToHtmlImage
{
	Task<HtmlElement> ConvertToImageElement(NotionBlock notionBlock);
}