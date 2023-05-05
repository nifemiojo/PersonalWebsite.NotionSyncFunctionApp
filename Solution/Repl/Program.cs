using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

var fileUrl =
	"https://s3.us-west-2.amazonaws.com/secure.notion-static.com/6f1174b7-c7a0-4367-b158-b181fedfc10a/DB.mp3?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Content-Sha256=UNSIGNED-PAYLOAD&X-Amz-Credential=AKIAT73L2G45EIPT3X45%2F20230420%2Fus-west-2%2Fs3%2Faws4_request&X-Amz-Date=20230420T162129Z&X-Amz-Expires=3600&X-Amz-Signature=bb9804ce5a128ad0e978c7fdfa8fb41b6c9802376b9a6f4c19e699aa5a8554b3&X-Amz-SignedHeaders=host&x-id=GetObject";

var supportedImageFileExtensions = new List<string> { "jpg", "jpeg", "png", "gif" };
var join = string.Join("|", supportedImageFileExtensions);

string pattern = $@"\b\w*\.({join})\b";

Regex r = new Regex(pattern, RegexOptions.IgnoreCase);

Match m = r.Match(fileUrl);

Console.WriteLine(m.Success + " " + m.Value + " found at " + m.Index);
