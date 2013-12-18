using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcComponent.Component
{
    public class LinkListComponent:BaseComponent
    {
        public LinkListComponent(ViewContext context) : base(context) 
        {
            Links = new List<LinkComponent>(); 
        }

        public virtual ICollection<LinkComponent> Links { get; set; }

        public override string TagName
        {
            get
            {
                return "ul";
            }
        }

        public override string HtmlStyle
        {
            get
            {
                return " style=margin:15px;";
            }
            set
            {
                base.HtmlStyle = value;
            }
        }

        public override void RenderConent(System.Web.UI.HtmlTextWriter writer)
        {

            foreach (var link in Links)
            {
                link.Render(writer);
            }
        }
    }
}