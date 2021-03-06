using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement("tablehead")]
    public class TableHeadTagHelper : TagHelper
    {
        public string BgColor { get; set; } = "light";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "thead";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", $"bg-{BgColor} text-white text-center");
            var content = (await output.GetChildContentAsync()).GetContent();
            //output.Content.SetHtmlContent($"<tr><th colspan=\"2\">{content}</th></tr>");

            var header = new TagBuilder("th");
            header.Attributes["colspan"] = "2";
            header.InnerHtml.Append(content);
            var row = new TagBuilder("tr");
            row.InnerHtml.AppendHtml(header);
            output.Content.SetHtmlContent(row);
        }
    }
}