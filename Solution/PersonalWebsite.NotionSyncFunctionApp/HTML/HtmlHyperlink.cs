using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.HTML.Base;

namespace PersonalWebsite.NotionSyncFunctionApp.HTML;

public class HtmlHyperlink : HtmlElement
{
	public override string? Tag { get; }
	public override List<HtmlElement>? Children { get; set; } = new List<HtmlElement>();
	public string Href { get; set; }
}