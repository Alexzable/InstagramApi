#pragma checksum "C:\Users\alexandru.boldisor\source\repos\JobFromLevi\backend\WebApi\WebApi\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0989ce72bfd42cb2f2388578f45b2430017f3d0f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "C:\Users\alexandru.boldisor\source\repos\JobFromLevi\backend\WebApi\WebApi\Views\_ViewImports.cshtml"
using WebApi;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\alexandru.boldisor\source\repos\JobFromLevi\backend\WebApi\WebApi\Views\_ViewImports.cshtml"
using WebApi.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0989ce72bfd42cb2f2388578f45b2430017f3d0f", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"250ef025d2a241ff78b8e1158b6f12845e284cf8", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<User>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\alexandru.boldisor\source\repos\JobFromLevi\backend\WebApi\WebApi\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4\">Welcome</h1>\r\n    <p>Learn about <a href=\"https://docs.microsoft.com/aspnet/core\">building Web apps with ASP.NET Core</a>.</p>\r\n    <button>\r\n        ");
#nullable restore
#line 10 "C:\Users\alexandru.boldisor\source\repos\JobFromLevi\backend\WebApi\WebApi\Views\Home\Index.cshtml"
   Write(Html.ActionLink("AuthWithInstagram",
        "Authorization",  // <-- ActionMethod
        "CallBack" ,   // <-- Controller Name
        new {}, // <-- Route arguments.
        null  // <-- htmlArguments .. which are none. You need this value
        //     otherwise you call the WRONG method ...
        //     (refer to comments, below).
        ));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </button>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<User> Html { get; private set; }
    }
}
#pragma warning restore 1591
