using MvcComponent.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class JqueryExtension
    {
        public static MvcHtmlString RenderScripts(this HtmlHelper helper)
        {
            StringBuilder sb = new StringBuilder();
            Tools.RegisterThemeScript(helper.ViewContext, Tools.Init2JsObject());
            sb.AppendLine("<script type='text/javascript'>");
            if (helper.ViewContext.TempData["InitThemeScripts"] != null)
            {
                sb.Append("(function init() {");
                sb.Append(helper.ViewContext.TempData["InitThemeScripts"].ToString());
                sb.AppendLine("}());");
                helper.ViewContext.TempData.Remove("InitThemeScripts");
            }
            if (helper.ViewContext.TempData["StartupScripts"] != null)
            {
                sb.AppendLine("$(function(){");
                sb.Append(helper.ViewContext.TempData["StartupScripts"].ToString());
                sb.AppendLine("});");
                helper.ViewContext.TempData.Remove("StartupScripts");
            }
            sb.AppendLine("</script>");
            return MvcHtmlString.Create(sb.ToString());
        }
    }

}