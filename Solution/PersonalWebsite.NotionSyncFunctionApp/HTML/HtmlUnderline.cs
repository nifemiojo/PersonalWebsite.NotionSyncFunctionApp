using System.Collections.Generic;

namespace PersonalWebsite.NotionSyncFunctionApp.HTML;

class HtmlUnderline : HtmlElement
{
	public override string? Tag { get; }
	public override List<HtmlElement>? Children { get; set; } = new List<HtmlElement>();
}