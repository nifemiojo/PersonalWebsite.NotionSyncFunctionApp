using System.Collections.Generic;

namespace PersonalWebsite.NotionSyncFunctionApp.HTML;

public class HtmlPreformattedElement : HtmlElement
{
    public override string? Tag => "pre";
    public override List<HtmlElement>? Children { get; set; }
}