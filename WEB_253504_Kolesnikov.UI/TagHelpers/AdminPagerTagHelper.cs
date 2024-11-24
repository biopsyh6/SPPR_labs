using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WEB_253504_Kolesnikov.Domain.Entities;

namespace WEB_253504_Kolesnikov.UI.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("AdminPager", TagStructure = TagStructure.WithoutEndTag)]
    public class AdminPagerTagHelper : TagHelper
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminPagerTagHelper(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }
        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }

        [HtmlAttributeName("total-pages")]
        public int TotalPages { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext!;
            HttpRequest request = httpContext!.Request;


            var ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("pagination");

            var prevPage = CurrentPage > 1 ? CurrentPage - 1 : 1;
            var prevliTag = new TagBuilder("li");
            prevliTag.AddCssClass($"page-item {(CurrentPage == 1 ? "disabled" : "")}");
            var prevaTag = new TagBuilder("a");
            prevaTag.AddCssClass("page-link");

            //var prevhref = $"/Admin/Product?pageNo={prevPage}";
            var prevhref = $"{request.Scheme}://{request.Host}{request.Path}?pageNo={prevPage}";
            prevaTag.Attributes.Add("data-pageNo", (prevPage).ToString());
            prevaTag.Attributes.Add("data-page", "/Product/Index");

            prevaTag.Attributes.Add("href", prevhref);

            var prevspanTag = new TagBuilder("span");
            prevspanTag.Attributes.Add("aria-hidden", "true");
            prevspanTag.InnerHtml.AppendHtml("&laquo;");
            prevaTag.InnerHtml.AppendHtml(prevspanTag);
            prevliTag.InnerHtml.AppendHtml(prevaTag);
            ulTag.InnerHtml.AppendHtml(prevliTag);


            for (int i = 0; i < TotalPages; i++)
            {
                var pageliTag = new TagBuilder("li");
                pageliTag.AddCssClass($"page-item {(CurrentPage == i + 1 ? "active" : "")}");
                var pageaTag = new TagBuilder("a");
                pageaTag.AddCssClass("page-link");

                //var href = $"/Admin/Product?pageNo={i + 1}";
                var href = $"{request.Scheme}://{request.Host}{request.Path}?pageNo={i + 1}";
                pageaTag.Attributes.Add("data-pageNo", $"{i + 1}");
                pageaTag.Attributes.Add("data-page", "/Product/Index");
                pageaTag.Attributes.Add("href", href);

                pageaTag.InnerHtml.Append((i + 1).ToString());
                pageliTag.InnerHtml.AppendHtml(pageaTag);
                ulTag.InnerHtml.AppendHtml(pageliTag);
            }

            var nextPage = CurrentPage < TotalPages ? CurrentPage + 1 : TotalPages;
            var nextliTag = new TagBuilder("li");
            nextliTag.AddCssClass($"page-item {(CurrentPage == TotalPages ? "disabled" : "")}");
            var nextaTag = new TagBuilder("a");
            nextaTag.AddCssClass("page-link");
            //var nexthref = $"/Admin/Product?pageNo={nextPage}";
            var nexthref = $"{request.Scheme}://{request.Host}{request.Path}?pageNo={nextPage}";

            nextaTag.Attributes.Add("data-pageNo", (nextPage).ToString());
            nextaTag.Attributes.Add("data-page", "/Product/Index");
            nextaTag.Attributes.Add("href", nexthref);

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
