using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcComponent.Component
{
    public class AjaxButtonComponent:BaseComponent
    {
        public AjaxButtonComponent(ViewContext viewContext) : base(viewContext) { }

        public string Text { get; set; }

        public override string TagName
        {
            get
            {
                return "div";
            }
        }

        public override void RenderConent(System.Web.UI.HtmlTextWriter writer)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", "#");
            a.MergeAttribute("style", "text-decoration:none");
            a.InnerHtml = Text;
            writer.Write(a.ToString());
        }
    }
}