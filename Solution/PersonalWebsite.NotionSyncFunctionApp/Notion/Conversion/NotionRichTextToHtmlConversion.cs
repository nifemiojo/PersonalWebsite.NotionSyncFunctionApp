using System;
using System.Collections.Generic;
using System.Linq;
using PersonalWebsite.ContentSyncFunction.Common;
using PersonalWebsite.ContentSyncFunction.HTML;
using PersonalWebsite.ContentSyncFunction.Notion.Models.Values;

namespace PersonalWebsite.ContentSyncFunction.Notion.Conversion;

public class NotionRichTextToHtmlConversion
{
	private readonly HtmlElement _rootElement;
	private readonly List<NotionRichText> _notionRichTexts;

	public NotionRichTextToHtmlConversion(HtmlElement rootElement, List<NotionRichText> notionRichTexts)
	{
		_rootElement = rootElement;
		_notionRichTexts = notionRichTexts;
	}

	public HtmlElement Convert()
	{
		HtmlElement currentParentElement = _rootElement;
		var nextIndex = 1;

		foreach (var notionRichText in _notionRichTexts)
		{
			currentParentElement = DetermineCurrentParentElement(currentParentElement, notionRichText);

			List<HtmlElement> candidateElements = notionRichText.GetHtmlElements();

			if (candidateElements.Count == 0)
			{
				var text = CreateHtmlPlainTextElement(notionRichText.PlainText);
				_rootElement.AddChild(text);
			}

			else if (candidateElements.Count == 1)
			{
				var newHtmlElement = candidateElements.Single();
				var htmlPlainTextElement = CreateHtmlPlainTextElement(notionRichText.PlainText);

				if (newHtmlElement.IsEquivalentTo(currentParentElement))
				{
					currentParentElement.AddChild(htmlPlainTextElement);
				}
				else
				{
					newHtmlElement.AddChild(htmlPlainTextElement);

					currentParentElement
						.AddChild(newHtmlElement);

					currentParentElement = newHtmlElement;
				}
			}
			else
			{
				var currentParentNewestDescendantHierarchy = GetNewestDescendantHierarchyInclusive(currentParentElement);

				var candidateElementsOrdered = CalculateNewCandidateParentElement(nextIndex, candidateElements, currentParentNewestDescendantHierarchy, notionRichText);


				if (currentParentNewestDescendantHierarchy.Count == 0)
				{
					if (!candidateElementsOrdered.First().IsEquivalentTo(currentParentElement))
					{
						currentParentElement.AddChild(candidateElementsOrdered.First());
					}
				}
				else
				{
					var allMatching = true;
					for (int i = 0; i < Math.Min(candidateElementsOrdered.Count, currentParentNewestDescendantHierarchy.Count); i++)
					{
						
						if (!candidateElementsOrdered[i].IsEquivalentTo(currentParentNewestDescendantHierarchy[i]))
						{
							currentParentNewestDescendantHierarchy[i].AddSibling(candidateElementsOrdered[i]);
							allMatching = false;
							break;
						}
					}

					if (allMatching)
					{
						GetLastElementInSourcePresentInTarget(currentParentNewestDescendantHierarchy, candidateElementsOrdered)
							.AddChild(
								GetLastElementInSourcePresentInTarget(candidateElementsOrdered, currentParentNewestDescendantHierarchy)
									.Children!
									.Single());
					}
				}
				
				if (!candidateElementsOrdered.First().IsEquivalentTo(currentParentElement))
					currentParentElement = candidateElementsOrdered.First();
			}

			nextIndex++;
		}

		return _rootElement;
	}

	private static HtmlElement GetLastElementInSourcePresentInTarget(List<HtmlElement> source, List<HtmlElement> target)
	{
		return source
			.Last( x => target.Any(x.IsEquivalentTo));
	}

	private List<HtmlElement> CalculateNewCandidateParentElement(
		int nextIndex,
		List<HtmlElement> candidateElements,
		List<HtmlElement> currentParentNewestDescendantHierarchy, 
		NotionRichText notionRichText)
	{
		var newElementsOrdered = DetermineOrderOfCandidateElements(
			nextIndex,
			candidateElements,
			currentParentNewestDescendantHierarchy);

		newElementsOrdered.Last().AddChild(new HtmlPlainText { Content = notionRichText.PlainText });

		for (int i = 0; i < newElementsOrdered.Count - 1; i++)
		{
			newElementsOrdered[i].AddChild(newElementsOrdered[i + 1]);
		}

		return newElementsOrdered;
	}

	private List<HtmlElement> DetermineOrderOfCandidateElements(
		int nextIndex,
		List<HtmlElement> candidateElements,
		List<HtmlElement> currentParentNewestDescendantHierarchy)
	{
		var newElementsOrdered = new List<HtmlElement>();

		if (nextIndex < _notionRichTexts.Count)
		{
			var remainingRichTexts = GetRemainingRichTexts(nextIndex);
			newElementsOrdered = DetermineNewParentElement(
				candidateElements,
				remainingRichTexts,
				currentParentNewestDescendantHierarchy);
		}
		else
		{
			DetermineParentElementUsingCurrentParent(
				newElementsOrdered,
				candidateElements,
				currentParentNewestDescendantHierarchy);
		}

		return newElementsOrdered;
	}

	private List<NotionRichText> GetRemainingRichTexts(int nextIndex)
	{
		return _notionRichTexts.GetRange(nextIndex, _notionRichTexts.Count - nextIndex);
	}

	private static HtmlPlainText CreateHtmlPlainTextElement(string text)
	{
		return new HtmlPlainText { Content = text };
	}

	private HtmlElement DetermineCurrentParentElement(HtmlElement currentParentElement, NotionRichText notionRichText)
	{
		if (currentParentElement == _rootElement) 
			return currentParentElement;

		var parentElementSemanticType = InlineTextSemanticMapping.GetSemanticTextType(currentParentElement);

		if (notionRichText.TextSemanticsIncludes(parentElementSemanticType, (currentParentElement as HtmlHyperlink)?.Href))
			return currentParentElement;

		return currentParentElement.Parent ?? throw new NullReferenceException();
	}

	private void DetermineParentElementUsingCurrentParent(List<HtmlElement> orderedElements, List<HtmlElement> annotationElements, List<HtmlElement> currentParentHierarchy)
	{
		foreach (var element in currentParentHierarchy)
		{
			var foundeElem = annotationElements.SingleOrDefault(x => x.IsEquivalentTo(element));
			if (foundeElem != null)
			{
				orderedElements.Add(foundeElem);
			}
		}

		orderedElements.AddRange(
		annotationElements.Where(x => !orderedElements.Contains(x)));
	}

	private List<HtmlElement> GetNewestDescendantHierarchyInclusive(HtmlElement? currentParent)
	{
		var currentParentHierarchy = currentParent.GetNewestDescendantElementsInclusive();

		if(currentParentHierarchy.First().IsEquivalentTo(_rootElement))
			currentParentHierarchy.RemoveAt(0);

		var plainTextElementsRemoved = currentParentHierarchy
			.Where(x => !x.IsEquivalentTo(typeof(HtmlPlainText)))
			.ToList();

		return plainTextElementsRemoved;
	}

	private List<HtmlElement> DetermineNewParentElement(List<HtmlElement> candidateElements, List<NotionRichText> remainingRichTexts, List<HtmlElement> currentParentHierarchy)
	{
		var orderedElements = new List<HtmlElement>();

		if (remainingRichTexts.First().TextSemanticsCount == 0)
		{
			DetermineParentElementUsingCurrentParent(orderedElements, candidateElements, currentParentHierarchy);
			return orderedElements;
		}

		for (int i = 0; i < remainingRichTexts.Count; i++)
		{
			if (candidateElements.Count - orderedElements.Count == 1)
			{
				orderedElements.Insert(0, candidateElements
					.Single(x => !orderedElements.Contains(x)));

				break;
			}

			for (int j = 0; j < candidateElements.Count; j++)
			{
				switch (InlineTextSemanticMapping.GetSemanticTextType(candidateElements[j]))
				{
					case InlineTextSemantic.Bold:
						if (orderedElements.Count(x => x is HtmlBold) == 0 && !remainingRichTexts[i].GetTextSemanticState(InlineTextSemantic.Bold))
						{
							orderedElements.Insert(0, candidateElements[j]);
						}
						break;
					case InlineTextSemantic.Italic:
						if (orderedElements.Count(x => x is HtmlItalic) == 0 && !remainingRichTexts[i].GetTextSemanticState(InlineTextSemantic.Italic))
						{
							orderedElements.Insert(0, candidateElements[j]);
						}
						break;
					case InlineTextSemantic.Strikethrough:
						if (orderedElements.Count(x => x is HtmlStrikethrough) == 0 && !remainingRichTexts[i].GetTextSemanticState(InlineTextSemantic.Strikethrough))
						{
							orderedElements.Insert(0, candidateElements[j]);
						}
						break;
					case InlineTextSemantic.Underline:
						if (orderedElements.Count(x => x is HtmlUnderline) == 0 && !remainingRichTexts[i].GetTextSemanticState(InlineTextSemantic.Underline))
						{
							orderedElements.Insert(0, candidateElements[j]);
						}
						break;
					case InlineTextSemantic.Code:
						if (orderedElements.Count(x => x is HtmlCode) == 0 && !remainingRichTexts[i].GetTextSemanticState(InlineTextSemantic.Code))
						{
							orderedElements.Insert(0, candidateElements[j]);
						}
						break;
					case InlineTextSemantic.Link:
						if (orderedElements.Count(x => x is HtmlHyperlink) == 0 && !remainingRichTexts[i].TextSemanticsIncludes(InlineTextSemantic.Link, (candidateElements[j] as HtmlHyperlink).Href))
						{
							orderedElements.Insert(0, candidateElements[j]);
						}
						break;
				}
			}
		}

		var elementsNeverSetToFalse = candidateElements.Where(x => !orderedElements.Contains(x)).ToList();
		if (elementsNeverSetToFalse.Any())
		{
			List<HtmlElement> remElements = new List<HtmlElement>();
			DetermineParentElementUsingCurrentParent(remElements, elementsNeverSetToFalse, currentParentHierarchy);
			orderedElements.InsertRange(0, remElements);
		}

		return orderedElements;
	}
}
