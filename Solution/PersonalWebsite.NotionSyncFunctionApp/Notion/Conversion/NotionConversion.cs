using System;
using System.Collections.Generic;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Page;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Conversion;

public class NotionConversion : INotionConversion
{
	public NotionConversion(IBlockConverter blockConverter)
	{
		
	}

	public string ConvertBlocksToHtml(List<NotionBlock> blocks)
	{
		List<string> convertedBlocks = new List<string>();

		foreach (var topLevelBlock in blocks)
	    {
		    if (topLevelBlock.HasChildren)
		    {
			    foreach (var block in topLevelBlock.ChildBlocks)
			    {
				    convertedBlocks.Add(ConvertBlocksToHtml(block));
			    }
				convertedBlocks.Add(ConvertBlocksToHtml(topLevelBlock));
		    }
	    }

	    return "";
    }
    
    private string ConvertBlocksToHtml(NotionBlock block)
    {
	    if (block.HasChildren)
	    {
			
	    }
		var convertedLeaf = ConvertLeafBlockToHtml(block);
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