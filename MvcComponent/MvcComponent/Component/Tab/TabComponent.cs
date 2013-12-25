using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcComponent.Component
{
    public class TabComponent : BaseComponent
    {
        public TabComponent(ViewContext viewContext) : base(viewContext) { }

        protected string JquerySelector
        {
            get { return ("$(\"#" + base.Name + "\")"); }
        }

        private IList<TabOptions> _collection;
        public IList<TabOptions> Collection
        {
            get
            {
                if (_collection == null)
                    _collection = new List<TabOptions>();
                return _collection;
            }
        }

        public override void RenderConent(System.Web.UI.HtmlTextWriter writer)
        {
            TagBuilder tbTitleUl = new TagBuilder("ul");
            tbTitleUl.AddCssClass("tabul");
            string divContent = "";
            foreach (var item in Collection)
            {
                TagBuilder tbTitleLi = new TagBuilder("li");
                tbTitleLi.InnerHtml = item.Title;
                tbTitleUl.InnerHtml += "\r\n" + tbTitleLi.ToString();

                TagBuilder tbContent = new TagBuilder("div");
                tbContent.InnerHtml = item.Content;
                divContent += tbContent.ToString();
            }
            // how to show double content by jquery?

            string htmlString = tbTitleUl.ToString() + divContent;
            writer.Write(htmlString);
            //肯定要加jquery控制。

            Tools.RegisterStartupScript(ViewContext, "");
        }
    }


}