#pragma checksum "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8ffdeead4dec0bd37264052858894ab276947112"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ProfessorPublicationLinkers_Index), @"mvc.1.0.view", @"/Views/ProfessorPublicationLinkers/Index.cshtml")]
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
#line 1 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\_ViewImports.cshtml"
using Lab1;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\_ViewImports.cshtml"
using Lab1.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8ffdeead4dec0bd37264052858894ab276947112", @"/Views/ProfessorPublicationLinkers/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"59419ef1b1476a13ca8a84f44f57646c39f2efca", @"/Views/_ViewImports.cshtml")]
    public class Views_ProfessorPublicationLinkers_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Lab1.ProfessorPublicationLinker>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("text-decoration:none;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
  
    ViewData["Title"] = "Публікації науковця";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <h1>Публікації науковця ");
#nullable restore
#line 7 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
                       Write(ViewBag.ProfessorName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 7 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
                                              Write(ViewBag.ProfessorSurname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n    <p>\r\n        ");
#nullable restore
#line 10 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
   Write(Html.ActionLink("Додати нову публікацію", "Create","ProfessorPublicationLinkers", new { professorId = @ViewBag.ProfessorId }, new { style="text-decoration:none; color:#0ce7e7;"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr style=\"font-size:120%; \">\r\n            <th>\r\n                ");
#nullable restore
#line 16 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Publication.NamePublication));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 19 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Publication.Publishing));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 22 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Publication.PageAmount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 25 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Publication.Version));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 31 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr style=\"font-size:120%; \">\r\n            <td>\r\n                ");
#nullable restore
#line 34 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Publication.NamePublication));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 37 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Publication.Publishing.PublishingName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 40 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Publication.PageAmount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 43 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Publication.Version));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 46 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
           Write(Html.ActionLink("Edit", "Edit", "ProfessorPublicationLinkers", new { id = @item.Id, professorId = @ViewBag.ProfessorId }, new { style = "text-decoration:none" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                <!--<a asp-action=\"Details\" asp-route-id=\"");
#nullable restore
#line 47 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
                                                     Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" style=\"text-decoration:none;\">Details</a> |-->\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8ffdeead4dec0bd37264052858894ab2769471128893", async() => {
                WriteLiteral("Delete");
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
#line 48 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
                                         WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 51 "D:\2 курс 2 сем\ІСТП\Lab1\Lab1\Views\ProfessorPublicationLinkers\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Lab1.ProfessorPublicationLinker>> Html { get; private set; }
    }
}
#pragma warning restore 1591
