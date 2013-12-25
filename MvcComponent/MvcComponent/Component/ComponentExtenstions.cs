using MvcComponent.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;


namespace System.Web.Mvc
{
    public static class ComponentExtenstions
    {
        public static LinkListBuilder LinkList(this HtmlHelper helper)
        {
           //helper.ViewContext.TempData[StartupScritp]  //JQ Scritp is all here
            return new LinkListBuilder(new LinkListComponent(helper.ViewContext));
            
        }

        //public static AjaxButtonBuilder

    }
}
