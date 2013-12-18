using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MvcComponent.Component
{
    public class BaseBuilder<TComponent, TBuilder>
        where TComponent : BaseComponent
        where TBuilder : BaseBuilder<TComponent, TBuilder>
    {
        private TComponent _component;

        public TComponent Component
        {
            get { return _component; }
            set { _component = value; }
        }

        public ViewContext Context
        {
            get { return Component.ViewContext; }
        }


        public BaseBuilder(TComponent component)
        {
            this.Component = component;
        }

        public TBuilder GenerateId()
        {
            if (string.IsNullOrEmpty(Component.Name))
            {
                string prefix = Component.GetType().Name;
                string key = prefix + "element";
                int seq = 1;

                if (Context.HttpContext.Items.Contains(key))
                {
                    seq = (int)Context.HttpContext.Items[key] + 1;
                    Context.HttpContext.Items[key] = seq;
                }
                else
                {
                    Context.HttpContext.Items.Add(key, seq);
                }
                Component.Name = prefix + seq.ToString();
            }
            return this as TBuilder;
        }

        public TBuilder SetHtmlStyle(Models.StyleCss item)
        {
            Type type = item.GetType();
            StringBuilder sb = new StringBuilder();
            sb.Append(" style='");
            foreach (var pro in type.GetProperties())
            {
                if (pro.GetValue(item) != null && pro.GetValue(item) != "")
                {

                }
            }
            if (sb.ToString() != " style='")
                Component.HtmlStyle = sb.ToString();
            return this as TBuilder;
        }

        public TBuilder SetHtmlClass(string css)
        {
            if (!string.IsNullOrEmpty(css))
            {
                Component.HtmlClass = " class=" + css;
            }
            return this as TBuilder;
        }


        public virtual TBuilder Name(string name)
        {
            Component.Name = name;
            return this as TBuilder;
        }

        public virtual void Render()
        {
            RenderComponent();
        }

        protected void RenderComponent()
        {
            using (var writer = new HtmlTextWriter(Context.Writer))
            {
                Component.Render(writer);
            }
        }

        public virtual MvcHtmlString GetHtml()
        {
            return MvcHtmlString.Create(Component.GetHtml());
        }



    }
}