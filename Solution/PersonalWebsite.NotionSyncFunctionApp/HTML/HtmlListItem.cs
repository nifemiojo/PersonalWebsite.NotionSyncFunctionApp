using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.HTML.Base;

namespace PersonalWebsite.NotionSyncFunctionApp.HTML;

internal class HtmlListItem : HtmlElement
{
    public override string? Tag { get; }
    public override List<HtmlElement>? Children { get; set; }
}