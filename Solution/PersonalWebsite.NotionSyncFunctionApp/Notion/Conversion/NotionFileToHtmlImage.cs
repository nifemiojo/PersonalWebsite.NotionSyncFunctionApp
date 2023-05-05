using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PersonalWebsite.NotionSyncFunctionApp.Application;
using PersonalWebsite.NotionSyncFunctionApp.HTML;
using PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Block;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.Conversion;

public class NotionFileToHtmlImage : INotionFileToHtmlImage
{
	private readonly HttpClient _httpClient;
	private readonly IAzureBlobContainer _azureBlobContainer;


	public NotionFileToHtmlImage(HttpClient httpClient,
		IAzureBlobContainer azureBlobContainer)
	{
		_httpClient = httpClient;
		_azureBlobContainer = azureBlobContainer;
	}

	public async Task<HtmlElement> ConvertToImageElement(NotionBlock notionBlock)
	{
		HtmlElement imageElement;

		if (notionBlock.Image.Type == "file")
		{
			string imageName = ExtractImageNameFromUrl(notionBlock.Image.File.Url);

			await using var imageStream = await _httpClient.GetStreamAsync(notionBlock.Image.File.Url);

			var imageUri = await _azureBlobContainer.UploadBlobAsync(imageName, imageStream);

			imageElement = new HtmlImageElement(imageUri);
		}
		else if (notionBlock.Image.Type == "external")
		{
			imageElement = new HtmlImageElement(
				new Uri(notionBlock.Image.External.Url));
		}
		else
		{
			throw new Exception($"Unknown image type: {notionBlock.Image.Type}");
		}

		return imageElement;
	}

	private string ExtractImageNameFromUrl(string fileUrl)
	{
		var supportedImageFileExtensions = new List<string> { "jpg", "jpeg", "png", "gif" };

		var fileExtensionsJoined = string.Join("|", supportedImageFileExtensions);

		string pattern = $@"\b\w*\.({fileExtensionsJoined})\b";

		Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

		var match = regex.Match(fileUrl);

		if(!match.Success)
			throw new Exception("Could not extract image name from URL");

		return match.Value;
	}
}