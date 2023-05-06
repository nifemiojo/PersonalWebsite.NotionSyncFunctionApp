﻿using System.Collections.Generic;
using System.Web;
using PersonalWebsite.NotionSyncFunctionApp.HTML.Base;

namespace PersonalWebsite.NotionSyncFunctionApp.HTML;

public class HtmlPlainText : HtmlElement
{
	private string  _content;
	public override string? Tag { get; }
	public override List<HtmlElement>? Children { get; set; } = null;

	public string Content
	{
		get => _content;
		set =>  _content = HttpUtility.HtmlEncode(value);
	}
}