using System.Collections.Generic;
using PersonalWebsite.ContentSyncFunction.Notion.Models.Values;

namespace PersonalWebsite.ContentSyncFunction.Notion.Properties.PageProperty;

internal class NotionTitlePropertyType : NotionPagePropertyType
{
    public List<NotionRichText> Title { get; set; }
}