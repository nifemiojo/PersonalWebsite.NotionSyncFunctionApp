using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Properties.Types;

internal class NotionTitlePropertyType : NotionPagePropertyType
{
	public List<NotionRichText> Title { get; set; }
}