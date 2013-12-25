using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MvcComponent.Component
{
    public abstract class BaseComponent
    {
        private ViewContext _viewContext;
        private StringBuilder _htmlAttribute;


        public ViewContext ViewContext
        {
            get { return _viewContext; }
        }

        public virtual string Name { get; set; }

        //default container
        public virtual string TagName
        {
            get
            {
                return "div";
            }
        }

        public virtual StringBuilder HtmlAttribute
        {
            get 
            {
                if (this._htmlAttribute == null)
                    _htmlAttribute = new StringBuilder();

                return _htmlAttribute;
            }
            set
            {
                _htmlAttribute = value;
            }
        }

        public BaseComponent(ViewContext context)
        {
            this._viewContext = context;

        }


        public virtual void Render(HtmlTextWriter writer)
        {
            RenderBeginContent(writer);

            RenderConent(writer);

            RenderEndConent(writer);
        }


        //輸出頭部+樣式如:  <a class='' style=''>
        public virtual void RenderBeginContent(HtmlTextWriter writer)
        {
            writer.WriteFullBeginTag(TagName + HtmlAttribute.ToString());
        }

        //當前component下<a>包含的內容  如:  <a><span></span></a>  <span></span>為content
        public virtual void RenderConent(HtmlTextWriter writer) { }


        //輸出結束如:  </a>
        public virtual void RenderEndConent(HtmlTextWriter writer)
        {
             writer.WriteEndTag(TagName);
        }


        public string GetHtml()
        {
            var result = new StringBuilder();
            using (var writer = new HtmlTextWriter(new StringWriter(result)))
            {
                Render(writer);
            }
            return result.ToString();
        }


    }
}