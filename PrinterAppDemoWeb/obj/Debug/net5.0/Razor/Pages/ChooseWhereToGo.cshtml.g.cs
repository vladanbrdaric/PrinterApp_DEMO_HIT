#pragma checksum "C:\Users\Vlado\Desktop\PrinterAppDEMO_HIT\PrinterAppDemoWeb\Pages\ChooseWhereToGo.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1f765455785e1a202762ed41787d9a8b23045159"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(PrinterAppDemoWeb.Pages.Pages_ChooseWhereToGo), @"mvc.1.0.razor-page", @"/Pages/ChooseWhereToGo.cshtml")]
namespace PrinterAppDemoWeb.Pages
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
#line 1 "C:\Users\Vlado\Desktop\PrinterAppDEMO_HIT\PrinterAppDemoWeb\Pages\_ViewImports.cshtml"
using PrinterAppDemoWeb;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1f765455785e1a202762ed41787d9a8b23045159", @"/Pages/ChooseWhereToGo.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"97356e73bb5037ae1b6cd8043a3bf9636d36e0d0", @"/Pages/_ViewImports.cshtml")]
    public class Pages_ChooseWhereToGo : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<script src=""https://kit.fontawesome.com/3a1451b7ba.js"" crossorigin=""anonymous""></script>

<style>

    body {
        background-color: black;
    }
    .h1Style {
        text-align: center;
        margin-top: 80px;
        font-size: 65px;
        margin-bottom: 80px;
        color: white;
    }

    .divStyle {
        margin-top: 130px;
    }

    .img1:hover {
        border: 1px solid;
        border-color: darkorange;
        padding: 4px 4px 4px 4px;
    }

    .img2:hover {
        border: 1px solid;
        border-color: darkorange;
        padding: 4px 4px 4px 4px;
    }

    .img3:hover {
        border: 1px solid;
        border-color: darkorange;
        padding: 4px 4px 4px 4px;

    }

</style>

<h1 class=""h1Style"" style=""font-size: 55px; margin-top: 120px;"">WHAT'S YOUR NEXT PAGE?</h1>


<div class=""divStyle"" style=""width: auto; height: auto; margin-bottom: 20em;"">
    <a href=""/AddNewPrinter"">
        <i class=""far fa-plus-square img1"" style=""c");
            WriteLiteral(@"olor: white; font-size: 130px; width: 33%; height: auto; text-align: center;""></i>
    </a>
    <a href=""/Index"">
        <i class=""fas fa-home img2"" style=""color: white; font-size: 130px; width: 33%; height: auto; text-align: center;""></i>
    </a>
    <a href=""/Status"">
        <i class=""fas fa-tachometer-alt img3"" style=""color: white; font-size: 130px; width: 33%; height: auto; text-align: center;""></i>
    </a>
</div>


");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PrinterApplicationWebUI.Pages.ChooseWhereToGoModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<PrinterApplicationWebUI.Pages.ChooseWhereToGoModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<PrinterApplicationWebUI.Pages.ChooseWhereToGoModel>)PageContext?.ViewData;
        public PrinterApplicationWebUI.Pages.ChooseWhereToGoModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
