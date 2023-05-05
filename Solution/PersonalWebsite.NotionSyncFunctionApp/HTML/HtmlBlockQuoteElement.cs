using System.Collections.Generic;

namespace PersonalWebsite.NotionSyncFunctionApp.HTML;

public class HtmlBlockQuoteElement : HtmlElement
{
    public override string? Tag { get; }
    public override List<HtmlElement>? Children { get; set; }
}