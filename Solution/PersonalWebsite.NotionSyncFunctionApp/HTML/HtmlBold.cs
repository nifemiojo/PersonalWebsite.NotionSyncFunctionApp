using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.HTML.Base;

namespace PersonalWebsite.NotionSyncFunctionApp.HTML;

public class HtmlBold : HtmlElement
{
	public override string? Tag { get; }
	public override List<HtmlElement>? Children { get; set; } = new List<HtmlElement>();
}