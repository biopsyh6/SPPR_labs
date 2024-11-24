using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WEB_253504_Kolesnikov.UI.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("Pager", TagStructure = TagStructure.WithoutEndTag)]
    public class PagerTagHelper : TagHelper
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PagerTagHelper(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Genre { get; set; }
        public bool Admin { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("pagination");

            var prevPage = CurrentPage > 1 ? CurrentPage - 1 : 1;
            var prevliTag = new TagBuilder("li");
            prevliTag.AddCssClass($"page-item {(CurrentPage == 1 ? "disabled" : "")}");
            var prevaTag = new TagBuilder("a");
            prevaTag.AddCssClass("page-link");
            prevaTag.Attributes.Add("aria-label", "Previous");
            prevaTag.Attributes.Add("data-page", (CurrentPage - 1).ToString());
            prevaTag.Attributes.Add("data-category", Genre);

            prevaTag.Attributes.Add("href", _linkGenerator.GetPathByAction(
                action: "Index",
                controller: "Movie",
                values: new { genre = Genre, page = prevPage }
            ));

            var prevspanTag = new TagBuilder("span");
            prevspanTag.Attributes.Add("aria-hidden", "true");
            prevspanTag.InnerHtml.AppendHtml("&laquo;");
            prevaTag.InnerHtml.AppendHtml(prevspanTag);
            prevliTag.InnerHtml.AppendHtml(prevaTag);
            ulTag.InnerHtml.AppendHtml(prevliTag);


            for(int i = 0; i < TotalPages; i++)
            {
                var pageliTag = new TagBuilder("li");
                pageliTag.AddCssClass($"page-item {(CurrentPage == i + 1 ? "active" : "")}");
                var pageaTag = new TagBuilder("a");
                pageaTag.AddCssClass("page-link");

                pageaTag.Attributes.Add("data-page", $"{i + 1}");
                pageaTag.Attributes.Add("data-category", Genre);

                pageaTag.Attributes.Add("href", _linkGenerator.GetPathByAction(
                    action: "Index",
                    controller: "Movie",
                    values: new { genre = Genre, page = i + 1 }
                ));

                pageaTag.InnerHtml.Append((i + 1).ToString());
                pageliTag.InnerHtml.AppendHtml(pageaTag);
                ulTag.InnerHtml.AppendHtml(pageliTag);
            }

            var nextPage = CurrentPage < TotalPages ? CurrentPage + 1 : TotalPages;
            var nextliTag = new TagBuilder("li");
            nextliTag.AddCssClass($"page-item {(CurrentPage == TotalPages ? "disabled" : "")}");
            var nextaTag = new TagBuilder("a");
            nextaTag.AddCssClass("page-link");

            nextaTag.Attributes.Add("aria-label", "Next");
            nextaTag.Attributes.Add("data-page", (CurrentPage + 1).ToString());
            nextaTag.Attributes.Add("data-category", Genre);

            nextaTag.Attributes.Add("href", _linkGenerator.GetPathByAction(
                action: "Index",
                controller: "Movie",
                values: new { genre = Genre, page = nextPage }
            ));

            var nextspanTag = new TagBuilder("span");
            nextspanTag.Attributes.Add("aria-hidden", "true");
            nextspanTag.InnerHtml.AppendHtml("&raquo;");
            nextaTag.InnerHtml.AppendHtml(nextspanTag);
            nextliTag.InnerHtml.AppendHtml(nextaTag);
            ulTag.InnerHtml.AppendHtml(nextliTag);


            output.TagName = "nav";
            output.Attributes.Add("aria-label", "Page navigation");
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(ulTag);
        }

    }
}
