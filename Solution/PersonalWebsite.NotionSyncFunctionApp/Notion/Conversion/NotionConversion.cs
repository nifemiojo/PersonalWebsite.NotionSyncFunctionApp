using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalWebsite.NotionSyncFunctionApp.HTML;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Misc;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Conversion;

public class NotionConversion : INotionConversion
{
	private readonly INotionRichTextToHtmlConversion _notionRichTextToHtmlConversion;
	private readonly INotionListBlockToHtmlListElement _notionListBlockToHtmlListElement;
	private readonly INotionFileToHtmlImage _notionFileToHtmlImage;

	public NotionConversion(INotionRichTextToHtmlConversion notionRichTextToHtmlConversion,
		INotionListBlockToHtmlListElement notionListBlockToHtmlListElement,
		INotionFileToHtmlImage notionFileToHtmlImage)
	{
		_notionRichTextToHtmlConversion = notionRichTextToHtmlConversion;
		_notionListBlockToHtmlListElement = notionListBlockToHtmlListElement;
		_notionFileToHtmlImage = notionFileToHtmlImage;
	}

	public async Task<string> ConvertBlocksToHtml(List<NotionBlock> blocks)
	{
		List<HtmlElement> blocksAsHtmlElements = new List<HtmlElement>();

		List<NotionBlock> processedListBlocks = new List<NotionBlock>();

		for (int topLevelBlockIndex = 0; topLevelBlockIndex < blocks.Count; topLevelBlockIndex++)
		{
			if (processedListBlocks.Contains(blocks[topLevelBlockIndex]))
				continue;

			var notionBlock = blocks[topLevelBlockIndex];

			switch (notionBlock.Type)
			{
				case "bulleted_list_item":
				case "numbered_list_item":
				{
					List<NotionBlock> blocksThatMakeUpList = ExtractBlocksThatMakeUpTheList(notionBlock, blocks);
					processedListBlocks.AddRange(blocksThatMakeUpList);

					HtmlElement listElement = _notionListBlockToHtmlListElement.Convert(blocksThatMakeUpList);

					blocksAsHtmlElements.Add(listElement);
					break;
				}
				case "image":
				{
					var imageElement = await ConvertToImageElement(notionBlock);

					blocksAsHtmlElements.Add(imageElement);
					break;
				}
				case "divider":
				{
					HtmlElement dividerElement = new HtmlDivElement();
					dividerElement.Class = "divider";
					dividerElement.Role = "separator";

					blocksAsHtmlElements.Add(dividerElement);
					break;
				}
				case "code":
				{
					var preformattedElement = new HtmlPreformattedElement();
					var codeElement = new HtmlCodeElement();
					preformattedElement.AddChild(codeElement);

					ConvertRichTextToHtmlElement(codeElement, notionBlock.Code.RichText                                                                                                                                                                                                                                                                                                                                                                             );

					blocksAsHtmlElements.Add(preformattedElement);
					break;
				}
				case "heading_1":
				{
					HtmlElement headingElement = ConvertRichTextToHtmlElement(new HtmlHeadingElement(1), notionBlock.HeadingOne.RichText);

					blocksAsHtmlElements.Add(headingElement);
					break;
				}
				case "heading_2":
				{
					HtmlElement headingElement = ConvertRichTextToHtmlElement(new HtmlHeadingElement(2), notionBlock.HeadingTwo.RichText);

					blocksAsHtmlElements.Add(headingElement);
					break;
				}
				case "heading_3":
				{
					HtmlElement headingElement = ConvertRichTextToHtmlElement(new HtmlHeadingElement(3), notionBlock.HeadingThree.RichText);

					blocksAsHtmlElements.Add(headingElement);
					break;
				}
				case "paragraph":
				{
					HtmlElement paragraphElement = ConvertRichTextToHtmlElement(new HtmlParagraphElement(), notionBlock.Paragraph.RichText);

					blocksAsHtmlElements.Add(paragraphElement);
					break;
				}
				case "quote":
				{
					HtmlElement paragraphElement = ConvertRichTextToHtmlElement(new HtmlBlockQuoteElement(), notionBlock.Paragraph.RichText);

					blocksAsHtmlElements.Add(paragraphElement);
					break;
				}
			}
		}

		return "";
    }

	public async Task<HtmlElement> ConvertToImageElement(NotionBlock notionBlock)
	{
		return await _notionFileToHtmlImage.ConvertToImageElement(notionBlock);
	}

	private HtmlElement ConvertRichTextToHtmlElement(HtmlElement element, List<NotionRichText> notionRichText)
	{
		return _notionRichTextToHtmlConversion.Convert(element, notionRichText);
	}


	private List<NotionBlock> ExtractBlocksThatMakeUpTheList(NotionBlock startingNotionListBlock, List<NotionBlock> blocks)
	{
		List<NotionBlock> blocksThatMakeUpTheList = new List<NotionBlock>();

		var notionListBlockIndex = blocks.IndexOf(startingNotionListBlock);
		if (notionListBlockIndex == -1)
			throw new Exception("Could not find starting block in list of blocks");

		var listType = startingNotionListBlock.Type;

		while (blocks[notionListBlockIndex].Type == listType)
		{
			blocksThatMakeUpTheList.Add(blocks[notionListBlockIndex]);
			notionListBlockIndex++;
		}

		return blocksThatMakeUpTheList;
	}
}