using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace System.Web.Mvc
{
    public static class Class1
    {
        public static MvcHtmlString Box(this HtmlHelper htmlHelper, string sm)
        {
            var a = htmlHelper.ViewData;
            return MvcHtmlString.Create(sm);
        }
    }
}