using MvcComponent.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;


namespace System.Web.Mvc
{
    public static class Extenstions
    {
        public static LinkListBuilder LinkList(this HtmlHelper helper)
        {
            return new LinkListBuilder(new LinkListComponent(helper.ViewContext));
        }

    }
}
