using PersonalWebsite.NotionSyncFunctionApp.Common;
using PersonalWebsite.NotionSyncFunctionApp.UnitTests.Tests;

namespace PersonalWebsite.ContentSync.Tests.Tests;

/// <summary>
/// Covers all cases where each NotionRichTextObject only has one inline text semantic active
/// </summary>
partial class NonNestedSemanticTextTests : NotionRichTextToHtmlConversionTests
{
	public static IEnumerable<object> SingleSemanticTypeCases => new object[]
	{
		new object[] { InlineTextSemantic.Bold },
		new object[] { InlineTextSemantic.Italic },
		new object[] { InlineTextSemantic.Strikethrough },
		new object[] { InlineTextSemantic.Underline },
		new object[] { InlineTextSemantic.Code },
		new object[] { InlineTextSemantic.Link },
	};
}