namespace Movies.App.Infrastructure.TagHelpers
{

    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    public class SearchTagHelper : TagHelper
    {

        public string SearchTerm { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            TagHelperContent tagHelperContent = await output.GetChildContentAsync();

            string textContent = tagHelperContent.GetContent();

            int startIndex = textContent.ToLower()
                .IndexOf(this.SearchTerm.ToLower(), StringComparison.InvariantCulture);

            output.TagName = "span";

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < textContent.Length; i++)
            {
                if (i == startIndex)
                {
                    sb.Append($@"<span class=""text-primary"">");

                    for (int j = i; j < i + this.SearchTerm.Length; j++)
                    {
                        sb.Append(textContent[j]);
                    }

                    sb.Append("</span>");

                    i += this.SearchTerm.Length - 1;
                }
                else
                {
                    sb.Append(textContent[i]);
                }
            }

            output.Content.SetHtmlContent($"{sb}");
        }

    }

}