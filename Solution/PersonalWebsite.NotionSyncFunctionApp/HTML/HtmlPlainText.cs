using System.Collections.Generic;
using System.Web;
using PersonalWebsite.NotionSyncFunctionApp.HTML.Base;

namespace PersonalWebsite.NotionSyncFunctionApp.HTML;

public class HtmlPlainText : HtmlElement
{
	public override string? Tag { get; } = null;
	public override List<HtmlElement>? Children { get; set; } = null;

	public HtmlPlainText(string content)
	{
		Content = content;
	}
}