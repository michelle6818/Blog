#pragma checksum "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "70e6a7488497c81adc53759160b8c32d71357ffa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_CategoryPosts_Index), @"mvc.1.0.view", @"/Views/CategoryPosts/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Owner\source\repos\Blog\Views\_ViewImports.cshtml"
using Blog;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Owner\source\repos\Blog\Views\_ViewImports.cshtml"
using Blog.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
using Blog.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
using PagedList;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
using PagedList.Core.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"70e6a7488497c81adc53759160b8c32d71357ffa", @"/Views/CategoryPosts/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"60de8826b8954e9153bb5ddebbd8520bddd0a921", @"/Views/_ViewImports.cshtml")]
    public class Views_CategoryPosts_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Blog.Models.CategoryPost>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 10 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n<section class=\"blog-list px-3 py-5 p-md-5\">\r\n    <div class=\"container\">\r\n");
            WriteLiteral("       \r\n");
#nullable restore
#line 24 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
         foreach (var categoryPost in Model.OrderByDescending(cp => cp.Created))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"item mb-5\">\r\n                <div class=\"media\">\r\n                    <img");
            BeginWriteAttribute("src", " src=\"", 689, "\"", 769, 1);
#nullable restore
#line 28 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
WriteAttributeValue("", 695, imageService.DecodeFile(categoryPost.ImageData, categoryPost.ContentType), 695, 74, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"image-fluid\">\r\n                    <div class=\"media-body\">\r\n                        <h3 class=\"title mb-1\">                           \r\n                                ");
#nullable restore
#line 31 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
                           Write(categoryPost.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("                           \r\n                        </h3>\r\n                        <span class=\"date meta mb-1\">\r\n                            Published ");
#nullable restore
#line 34 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
                                 Write(categoryPost.Created.ToString("MMM dd, yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </span>\r\n");
            WriteLiteral("                        <div class=\"col\"></div>\r\n                        <div class=\"col\"></div>\r\n                        ");
#nullable restore
#line 39 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
                   Write(Html.Raw(categoryPost.Abstract));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </div>\r\n                        <div class=\"intro\">\r\n                            ");
#nullable restore
#line 42 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
                       Write(Html.Raw(categoryPost.Abstract));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "70e6a7488497c81adc53759160b8c32d71357ffa7620", async() => {
                WriteLiteral("Read more &rarr;");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 44 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
                                                  WriteLiteral(categoryPost.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n");
            WriteLiteral("            <hr>\r\n");
#nullable restore
#line 49 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n</section>\r\n\r\n<div class=\"row\">\r\n    <div class=\"col\">\r\n        ");
#nullable restore
#line 56 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
   Write(ViewBag.PageXofY);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n</div>\r\n<div class=\"row\">\r\n    <div class=\"col\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "70e6a7488497c81adc53759160b8c32d71357ffa10467", async() => {
                WriteLiteral("\r\n            <input type=\"hidden\" name=\"searchString\"");
                BeginWriteAttribute("value", " value=\"", 2103, "\"", 2132, 1);
#nullable restore
#line 62 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
WriteAttributeValue("", 2111, ViewBag.SearchString, 2111, 21, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("/> \r\n");
#nullable restore
#line 63 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
             for (var loop = 1; loop <= ViewBag.TotalPages; loop++)
            {
                var myClass = "btn-success";
                if(loop == (int)ViewBag.PageNumber)
                {
                    myClass = "btn-outline-success";
                }

#line default
#line hidden
#nullable disable
                WriteLiteral("                <button");
                BeginWriteAttribute("class", " class=\"", 2436, "\"", 2463, 3);
                WriteAttributeValue("", 2444, "btn", 2444, 3, true);
                WriteAttributeValue(" ", 2447, "btn-sm", 2448, 7, true);
#nullable restore
#line 70 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
WriteAttributeValue(" ", 2454, myClass, 2455, 8, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" type=\"submit\" name=\"pageNumber\"");
                BeginWriteAttribute("value", " value=\"", 2496, "\"", 2509, 1);
#nullable restore
#line 70 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
WriteAttributeValue("", 2504, loop, 2504, 5, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 70 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
                                                                                             Write(loop);

#line default
#line hidden
#nullable disable
                WriteLiteral("</button>\r\n");
#nullable restore
#line 71 "C:\Users\Owner\source\repos\Blog\Views\CategoryPosts\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IImageService imageService { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Blog.Models.CategoryPost>> Html { get; private set; }
    }
}
#pragma warning restore 1591
