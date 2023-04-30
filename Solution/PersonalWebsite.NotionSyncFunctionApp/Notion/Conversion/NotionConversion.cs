using System;
using System.Collections.Generic;
using System.Linq;
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
		List<HtmlElement> blocksAsHtmlElements = new List<HtmlElement>();

		for (int topLevelBlockIndex = 0; topLevelBlockIndex < blocks.Count; topLevelBlockIndex++)
		{
			if (blocks[topLevelBlockIndex].Type  == "bulleted_list_item" || blocks[topLevelBlockIndex].Type == "numbered_list_item")
			{
				List<NotionBlock> blocksThatMakeUpList = ExtractBlocksThatMakeUpTheList(topLevelBlockIndex, blocks);

				HtmlElement listElement = GetListElement(blocksThatMakeUpList);

				blocksAsHtmlElements.Add(listElement);

				topLevelBlockIndex += blocksThatMakeUpList.Count - 1;
			}

			if (blocks[topLevelBlockIndex].Type == "code")
			{
				var preformattedElement = new HtmlPreformattedElement();

				HtmlElement codeElement = _notionRichTextToHtmlConversion.Convert(new HtmlCodeElement(), blocks[topLevelBlockIndex].Paragraph.RichText);
				preformattedElement.AddChild(codeElement);

				blocksAsHtmlElements.Add(preformattedElement);
			}

			if (blocks[topLevelBlockIndex].Type == "embed")
			{
				HtmlElement embedElement = new HtmlEmbedElement(blocks[topLevelBlockIndex].Embed.Url);
				blocksAsHtmlElements.Add(embedElement);
			}

			if (blocks[topLevelBlockIndex].Type == "image")
			{
				if (blocks[topLevelBlockIndex].Image.Type == "file")
				{
					// Create a stream from the image data 

					// Upload the image stream to Azure Blob Storage

					// Create an img element with the src attribute pointing to the Azure Blob Storage URL
				}
				else
				{
					// Create an img element with the src attribute pointing to the public URL
				}
			}
		}

		return "";
    }

	private HtmlElement GetListElement(List<NotionBlock> notionListBlocks)
	{
		HtmlElement listElement = GetListElement(notionListBlocks.First());

		foreach (NotionBlock listBlock in notionListBlocks)
		{
			HtmlListItem listItem = GenerateListItem(listBlock);
			listElement.AddChild(listItem);
		}

		return listElement;
	}

	private static HtmlElement GetListElement(NotionBlock notionBlock)
	{
		return notionBlock.Type == "bulleted_list_item" ? new HtmlUnorderedListElement() : new HtmlOrderedListElement();
	}

	private HtmlListItem GenerateListItem(NotionBlock notionBlock)
	{
		HtmlListItem listItem = (HtmlListItem)_notionRichTextToHtmlConversion.Convert(new HtmlListItem(), notionBlock.BulletedListItem.RichText);

		if (notionBlock.HasChildren)
		{
			foreach (NotionBlock childBlock in notionBlock.ChildBlocks)
			{
				if (childBlock.Type == "bulleted_list_item" || childBlock.Type == "numbered_list_item")
				{
					HtmlElement nestedListElement = GetListElement(notionBlock.ChildBlocks.First());
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

	private List<NotionBlock> ExtractBlocksThatMakeUpTheList(int topLevelBlockIndex, List<NotionBlock> blocks)
	{
		List<NotionBlock> blocksThatMakeUpTheList = new List<NotionBlock>();

		var listType = blocks[topLevelBlockIndex].Type;

		while (blocks[topLevelBlockIndex].Type == listType)
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

public class HtmlEmbedElement : HtmlElement
{
	public string EmbedUrl { get; }

	public HtmlEmbedElement(string embedUrl)
	{
		EmbedUrl = embedUrl;
	}

	public override string? Tag { get; }
	public override List<HtmlElement>? Children { get; set; }
}

public interface IBlockConverter
{
}

public interface INotionConversion
{
    string ConvertBlocksToHtml(List<NotionBlock> blocks);
}