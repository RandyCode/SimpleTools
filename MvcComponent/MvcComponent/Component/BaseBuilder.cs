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

        public BaseBuilder() { }

        public BaseBuilder(TComponent component)
        {
            this.Component = component;
        }

        public TBuilder GenerateId(string name)
        {
            Component.Name = name;
            Component.HtmlAttribute.Append(MergeAttribute("id", name));
            Component.HtmlAttribute.Append(MergeAttribute("name", name));
            return this as TBuilder;
        }

        public TBuilder GenerateId()
        {
            if (string.IsNullOrEmpty(this.Component.Name))
            {
                string name = this.Component.GetType().Name;
                string key = name;
                int num = 1;
                if (this.Context.HttpContext.Items.Contains(key))
                {
                    num = ((int)this.Context.HttpContext.Items[key]) + 1;
                    this.Context.HttpContext.Items[key] = num;
                }
                else
                {
                    this.Context.HttpContext.Items.Add(key, num);
                }
                this.Component.Name = name + num.ToString();
                Component.HtmlAttribute.Append(MergeAttribute("id", this.Component.Name));
                Component.HtmlAttribute.Append(MergeAttribute("name", this.Component.Name));
            }
            return (this as TBuilder);
        }



        public TBuilder SetHtmlAttributes(Models.HtmlStyleAttribute item)
        {
            Type type = item.GetType();
            StringBuilder sb = new StringBuilder();
            foreach (var pro in type.GetProperties())
            {
                if (pro.GetValue(item) != null && pro.GetValue(item).ToString() != "")
                {
                    var name = pro.Name.ToLower().ToString();
                    var value = pro.GetValue(item).ToString();
                    sb.AppendFormat("{0}:{1};", name, value);

                }
            }
            if (!string.IsNullOrWhiteSpace(sb.ToString()))
            {
                Component.HtmlAttribute.Append(MergeAttribute("style", sb.ToString()));
            }

            return this as TBuilder;
        }

        public TBuilder SetCssClass(string css)
        {
            if (!string.IsNullOrEmpty(css))
            {
                Component.HtmlAttribute.Append(MergeAttribute("class", css));
            }
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


        private string MergeAttribute(string key, string value)
        {
            return string.Format(" {0}='{1}' ", key, value);
        }


    }
}