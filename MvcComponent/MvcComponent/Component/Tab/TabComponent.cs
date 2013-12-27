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

        //如果沒用調用GenerateId()  此屬性有問題。！name=null
        protected string JquerySelector
        {
            get
            {
                return ("$(\"#" + base.Name + "\")");
            }
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
                tbTitleLi.MergeAttribute("style", "cursor:pointer");
                tbTitleLi.InnerHtml = item.Title;
                tbTitleUl.InnerHtml += "\r\n" + tbTitleLi.ToString();

                TagBuilder tbContent = new TagBuilder("div");
                tbContent.MergeAttribute("style", "display:none");
                tbContent.InnerHtml = item.Content;
                divContent += tbContent.ToString();
            }


            string htmlString = tbTitleUl.ToString() + divContent;
            writer.Write(htmlString);
            Tools.RegisterStartupScript(ViewContext, "$('#"+base.Name+"').tabClick().tabHover()");

            ////JavaScript  第一種。 c#寫多點 js少點
            //JqueryBuilder.Singleton.Selector("selfTab li:nth-child(1)")
            //                        .Click((r) => { r.Selector("selfTab div").Hide(); r.Selector("selfTab div:nth-child(2)").Show(); })
            //                        .RenderScript(ViewContext);

            //JqueryBuilder.Singleton.Selector("selfTab li:nth-child(2)")
            //                      .Click((r) => { r.Selector("selfTab div").Hide(); r.Selector("selfTab div:nth-child(3)").Show(); })
            //                      .RenderScript(ViewContext);
 
            //JavaScript 第二種 用Options  js多點  C#少點

        }
    }


}