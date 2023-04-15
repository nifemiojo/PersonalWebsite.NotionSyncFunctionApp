using System.Text.Json.Serialization;

namespace PersonalWebsite.NotionSyncFunctionApp.Notion.DTOs.Objects.Misc;

public class NotionTextAnnotation
{
    public bool Bold { get; set; }

    public bool Italic { get; set; }

    public bool Strikethrough { get; set; }

    public bool Underline { get; set; }

    public bool Code { get; set; }

    [JsonPropertyName("color")]
    public string Colour { get; set; }
}