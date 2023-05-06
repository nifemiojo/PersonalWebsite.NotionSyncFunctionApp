using System;
using System.Collections.Generic;
using System.Linq;
using PersonalWebsite.NotionSyncFunctionApp.HTML;
using PersonalWebsite.NotionSyncFunctionApp.HTML.Base;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Conversion;

public interface INotionListBlockToHtmlListElement
{
	HtmlElement Convert(List<NotionBlock> blocksThatMakeUpList);
}

class NotionListBlockToHtmlListElement : INotionListBlockToHtmlListElement
{
	private readonly INotionRichTextToHtmlConversion _notionRichTextToHtmlConversion;

	public NotionListBlockToHtmlListElement(INotionRichTextToHtmlConversion notionRichTextToHtmlConversion)
	{
		_notionRichTextToHtmlConversion = notionRichTextToHtmlConversion;
	}

	public HtmlElement Convert(List<NotionBlock> blocksThatMakeUpList)
	{
		return ConvertToListElement(blocksThatMakeUpList);
	}

	private HtmlElement ConvertToListElement(List<NotionBlock> notionListBlocks)
	{
		HtmlElement listElement = GenerateListElement(notionListBlocks.First());

		foreach (NotionBlock listBlock in notionListBlocks)
		{
			HtmlListItem listItem = GenerateListItem(listBlock);
			listElement.AddChild(listItem);
		}

		return listElement;
	}

	private static HtmlElement GenerateListElement(NotionBlock notionBlock)
	{
		return notionBlock.Type == "bulleted_list_item" ? new HtmlUnorderedListElement() : new HtmlOrderedListElement();
	}

	private HtmlListItem GenerateListItem(NotionBlock notionBlock)
	{
		HtmlListItem listItem = (HtmlListItem) _notionRichTextToHtmlConversion.Convert(new HtmlListItem(), notionBlock.BulletedListItem.RichText);

		if (notionBlock.HasChildren)
		{
			foreach (NotionBlock childBlock in notionBlock.ChildBlocks)
			{
				if (childBlock.Type == "bulleted_list_item" || childBlock.Type == "numbered_list_item")
				{
					HtmlElement nestedListElement = GenerateListElement(notionBlock.ChildBlocks.First());
					HtmlListItem nestedListItem = GenerateListItem(childBlock);
					nestedListElement.AddChild(nestedListItem);
					listItem.AddChild(nestedListElement);
				}
				else
				{
					throw new Exception($"Notion block (id = {notionBlock.Id}) has children that are not list items");
				}
			}
		}

		return listItem;
	}
}