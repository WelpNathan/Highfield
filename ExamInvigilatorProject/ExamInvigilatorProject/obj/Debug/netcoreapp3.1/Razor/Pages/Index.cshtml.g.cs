#pragma checksum "C:\Users\Student\Source\Repos\CStorey86\Highfield\ExamInvigilatorProject\ExamInvigilatorProject\Pages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "02e977beb166f4c3244305f306a7e916382563d6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(ExamInvigilatorProject.Pages.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
namespace ExamInvigilatorProject.Pages
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
#line 1 "C:\Users\Student\Source\Repos\CStorey86\Highfield\ExamInvigilatorProject\ExamInvigilatorProject\Pages\_ViewImports.cshtml"
using ExamInvigilatorProject;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"02e977beb166f4c3244305f306a7e916382563d6", @"/Pages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"445f110198505bc186245420e857ac6cf81ec239", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Student\Source\Repos\CStorey86\Highfield\ExamInvigilatorProject\ExamInvigilatorProject\Pages\Index.cshtml"
  
    ViewData["Title"] = "Highfield Education - Exam Invigilation Software ";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "02e977beb166f4c3244305f306a7e916382563d63404", async() => {
                WriteLiteral(@"
    <meta charset=""UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <title>Highfield Education - Exam Invigilation Software </title>

    <!-- links and includes-->
    <link href=""css/mobile.css"" rel=""stylesheet"" />
    <link href=""css/desktop.css"" rel=""stylesheet"" media=""only screen and (min-width : 720px)"" />
    <link href=""css/bootstrap/bootstrap-grid.min.css"" rel=""stylesheet"">
    <link rel=""stylesheet"" href=""https://use.fontawesome.com/releases/v5.7.1/css/all.css"">

");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "02e977beb166f4c3244305f306a7e916382563d64923", async() => {
                WriteLiteral(@"
    <div class=""container"">

        <!--  Navigation Bar  -->

        <div class=""topnav"">
            <a href=""index.html"" class=""active"">
                <img class=""logo1"" src=""img/logo.PNG"" alt=""Highfield Education Logo"">
            </a>

            <div id=""myLinks"">
                <a href=""index.html"">Home</a>
                <a href=""login.html"">Login</a>
                <a href=""register.html"">Register</a>
                <a href=""contact.html"">Contact Us</a>
            </div>

            <a href=""javascript:void(0);"" class=""webicon"" onclick=""topNavBar()"">
                <i class=""fa fa-bars fa-2x""></i>
            </a>
            <a href=""javascript:void(0);"" class=""icon"" onclick=""topNavBar()"">
                <i class=""fa fa-bars""></i>
            </a>
        </div>

        <!-- end navigation bar -->

        <div class=""title"">
            <h1>Highfield Education - Exam Invigilation</h1>
        </div>
        <!-- main content -->
        <div class=""mai");
                WriteLiteral("nContent\">\r\n            <div class=\"section1\">\r\n                <h2>Home</h2>\r\n                <p class=\"text1\"> </p>\r\n\r\n                <img");
                BeginWriteAttribute("id", " id=\"", 1829, "\"", 1834, 0);
                EndWriteAttribute();
                BeginWriteAttribute("alt", " alt=\"", 1835, "\"", 1841, 0);
                EndWriteAttribute();
                BeginWriteAttribute("src", " src=\"", 1842, "\"", 1848, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n\r\n\r\n                <p class=\"text2\">\r\n                </p>\r\n            </div>\r\n        </div>\r\n        <!-- footer -->\r\n        <!-- end footer -->\r\n\r\n    </div>\r\n\r\n    <!-- Javascript links here -->\r\n    <script src=\"main.js\"></script>\r\n\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel>)PageContext?.ViewData;
        public IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
