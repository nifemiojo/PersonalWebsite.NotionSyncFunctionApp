using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWebsite.NotionSyncFunctionApp.HTML;

public abstract class HtmlElement
{
	public abstract string? Tag { get; }

	public virtual HtmlElement? Parent { get; set; } = null;

	public abstract List<HtmlElement>? Children { get; set; }

	public void AddChild(HtmlElement htmlElement)
	{
		Children.Add(htmlElement);
		htmlElement.Parent = this;
	}

	public void AddChildren(IEnumerable<HtmlElement> htmlElements)
	{
		Children.AddRange(htmlElements);

		foreach (var htmlElement in htmlElements)
		{
			htmlElement.Parent = this;
		}
	}

	public void AddSibling(HtmlElement htmlElement)
	{
		if (Parent == null)
			throw new InvalidOperationException("Cannot add sibling to root element");

		Parent.AddChild(htmlElement);
		htmlElement.Parent = Parent;
	}

	// To validate the state, every element should have a plain text element (leaf) as the deepest element on any branch
	public bool IsEquivalentTo(HtmlElement htmlElement)
	{
		if (GetType() != htmlElement.GetType())
			return false;

		if (this is HtmlHyperlink linkElement)
		{
			return linkElement.Href == ((HtmlHyperlink) htmlElement).Href;
		}

		return true;
	}

	public bool IsEquivalentTo(Type type)
	{
		return GetType() == type;
	}

	public List<HtmlElement> GetDescendantElements()
	{
		var descendantElements = new List<HtmlElement>();
		foreach (var child in Children)
		{
			descendantElements.Add(child);
			descendantElements.AddRange(child.GetDescendantElements());
		}

		return descendantElements;
	}

	public List<HtmlElement> GetNewestDescendantElementsInclusive()
	{
		var mostRecentDescendantElements = new List<HtmlElement>();
		var mostRecentChildElement = this;

		while (mostRecentChildElement != null)
		{
			mostRecentDescendantElements.Add(mostRecentChildElement);
			mostRecentChildElement = mostRecentChildElement.Children?.LastOrDefault();
		}

		return mostRecentDescendantElements;
	}
}