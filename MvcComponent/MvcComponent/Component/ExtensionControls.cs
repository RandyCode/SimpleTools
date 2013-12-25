using MvcComponent.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class ExtensionControls
    {
        public static LinkListBuilder LinkList(this HtmlHelper helper)
        {
            return new LinkListBuilder(new LinkListComponent(helper.ViewContext));
        }

        public static AjaxButtonBuilder AButton(this HtmlHelper helper)
        {
            return new AjaxButtonBuilder(new AjaxButtonComponent(helper.ViewContext));
        }

        public static TabBuilder Tab(this HtmlHelper helper)
        {
            return new TabBuilder(new TabComponent(helper.ViewContext));
        }
    }
}