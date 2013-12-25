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
            if (helper.ViewContext.TempData["StartupScripts"] != null)
            {
                sb.AppendLine("<script type='text/javascript'>");
                sb.AppendLine("$(function(){");
                sb.Append(helper.ViewContext.TempData["StartupScripts"].ToString());
                sb.AppendLine("});");
                sb.AppendLine("</script>");
                helper.ViewContext.TempData.Remove("StartupScripts");
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}