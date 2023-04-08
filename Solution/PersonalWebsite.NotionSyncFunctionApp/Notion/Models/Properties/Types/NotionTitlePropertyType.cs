using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Objects;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Models.Properties.Types;

internal class NotionTitlePropertyType : NotionPagePropertyType
{
	public List<NotionRichText> Title { get; set; }
}