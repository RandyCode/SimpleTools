using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcComponent.Component
{
    public  class LinkComponent:BaseComponent
    {
        public LinkComponent(ViewContext context) : base(context) { }

        public string Title { get; set; }

        public string Url { get; set; }

        public override string TagName
        {
            get
            {
                return "li";
            }
        }

        public override string HtmlStyle
        {
            get
            {
                return " style=list-style-type:none";
            }
            set
            {
                base.HtmlStyle = value;
            }
        }

        public override void RenderConent(System.Web.UI.HtmlTextWriter writer)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", Url);
            a.InnerHtml = Title;
            writer.Write(a.ToString());
        }
    }
}