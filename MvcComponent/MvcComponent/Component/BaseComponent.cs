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
        private string _id;

        public ViewContext ViewContext
        {
            get { return _viewContext; }
        }

        public virtual string Id
        {
            get
            {
                if ((string.IsNullOrEmpty(_id)) && (!string.IsNullOrEmpty(Name)))
                {
                    var _tag = new TagBuilder(TagName);
                    _tag.GenerateId(Name);
                    if (_tag.Attributes.ContainsKey("id"))
                        _id = _tag.Attributes["id"];
                    else
                        _id = Name;
                }
                return _id;
            }
            private set { _id = value; }
        }

        public virtual string Name { get; set; }

        public virtual string TagName
        {
            get
            {
                return "div";
            }
        }

        //自定義html屬性  《-- 應提供個重寫方法。。返回當前空間

        public virtual string HtmlStyle { get; set; }

        public virtual string HtmlClass { get; set; }

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
            writer.WriteFullBeginTag(TagName + HtmlClass + HtmlStyle);
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