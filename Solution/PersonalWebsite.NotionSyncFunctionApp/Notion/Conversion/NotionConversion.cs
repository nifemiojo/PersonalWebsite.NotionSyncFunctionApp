using System;
using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.HTML;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Conversion;

public class NotionConversion : INotionConversion
{
	private readonly INotionRichTextToHtmlConversion _notionRichTextToHtmlConversion;

	public NotionConversion(IBlockConverter blockConverter, INotionRichTextToHtmlConversion notionRichTextToHtmlConversion)
	{
		_notionRichTextToHtmlConversion = notionRichTextToHtmlConversion;
	}

	public string ConvertBlocksToHtml(List<NotionBlock> blocks)
	{
		for (int topLevelBlockIndex = 0; topLevelBlockIndex < blocks.Count; topLevelBlockIndex++)
		{
			if (blocks[topLevelBlockIndex].Type  == "bulleted_list_item")
			{
				//  Get the blocks in the list
				List<NotionBlock> blocksThatMakeUpList = ExtractBlocksThatMakeUpTheList(topLevelBlockIndex, blocks);

				// Build top level list -- recursively attain list items
				HtmlElement unorderedElement = GetUnorderedList(blocksThatMakeUpList);

				// Add top level block to the list

				// Adjust the index to skip the blocks that make up the list
			}
			
		}
    }

	private HtmlElement GetUnorderedList(List<NotionBlock> topLevelListBlocks)
	{
		HtmlUnorderedListElement unorderedListElement = new HtmlUnorderedListElement();

		foreach (NotionBlock topLevelListBlock in topLevelListBlocks)
		{
			// Recursively get the list items
			HtmlListItem listItem = GenerateListItem(topLevelListBlock);
		}

		return unorderedListElement;
	}

	private HtmlListItem GenerateListItem(NotionBlock topLevelListBlock)
	{
		HtmlUnorderedListElement nestedUnorderedListElement = new HtmlUnorderedListElement();

		if (topLevelListBlock.HasChildren)
		{
			foreach (NotionBlock childBlock in topLevelListBlock.ChildBlocks)
			{
				if (childBlock.Type == "bulleted_list_item")
				{
					HtmlListItem nestedListItem = GenerateListItem(childBlock);
					nestedUnorderedListElement.AddChild(nestedListItem);
				}
			}
		}

		HtmlListItem listItem = (HtmlListItem)_notionRichTextToHtmlConversion.Convert(new HtmlListItem(), topLevelListBlock.BulletedListItem.RichText);
		listItem.AddChild(nestedUnorderedListElement);

		return listItem;
	}

	private List<NotionBlock> ExtractBlocksThatMakeUpTheList(int topLevelBlockIndex, List<NotionBlock> blocks)
	{
		List<NotionBlock> blocksThatMakeUpTheList = new List<NotionBlock>();

		while (blocks[topLevelBlockIndex].Type == "bulleted_list_item")
		{
			blocksThatMakeUpTheList.Add(blocks[topLevelBlockIndex]);
			topLevelBlockIndex++;
		}

		return blocksThatMakeUpTheList;
	}

	private string ConvertLeafBlockToHtml(NotionBlock block)
    {
	    switch (block.Type)
	    {
		    case "bulleted_list_item":
			    return $"<li>{block.BulletedListItem.Text[0].PlainText}</li>";
		    /*case "numbered_list_item":
			    return $"<li>{block.NumberedListItem.Text[0].PlainText}</li>";
		    case "heading_1":
			    return $"<h1>{block.HeadingOne.Text[0].PlainText}</h1>";
		    case "heading_2":
			    return $"<h2>{block.HeadingTwo.Text[0].PlainText}</h2>";
		    case "heading_3":
			    return $"<h3>{block.HeadingThree.Text[0].PlainText}</h3>";
		    case "paragraph":
			    return $"<p>{block.Paragraph.Text[0].PlainText}</p>";
		    case "quote":
			    return $"<blockquote>{block.Quote.Text[0].PlainText}</blockquote>";
		    case "code":
			    return $"<pre><code>{block.Code.Text[0].PlainText}</code></pre>";
		    case "image":
			    return $"<img src=\"{block.Image.File.Url}\" alt=\"{block.Image.Caption[0].PlainText}\" />";*/
		    default:
			    throw new ArgumentOutOfRangeException();
	    }	
    }
}

public interface IBlockConverter
{
}

public interface INotionConversion
{
    string ConvertBlocksToHtml(List<NotionBlock> blocks);
}