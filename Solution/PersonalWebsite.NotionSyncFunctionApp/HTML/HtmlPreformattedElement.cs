using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.HTML.Base;

namespace PersonalWebsite.NotionSyncFunctionApp.HTML;

public class HtmlPreformattedElement : HtmlElement
{
    public override string? Tag => "pre";
    public override List<HtmlElement>? Children { get; set; }
}