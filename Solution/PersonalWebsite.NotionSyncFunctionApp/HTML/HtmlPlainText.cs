using System.Collections.Generic;
using System.Web;

namespace PersonalWebsite.NotionSyncFunctionApp.HTML;

public class HtmlPlainText : HtmlElement
{
	private string  _content;
	public override string? Tag { get; }
	public override List<HtmlElement>? Children { get; set; } = null;

	public string Content
	{
		get => HttpUtility.HtmlEncode(_content);
		set =>  _content = value;
	}
}