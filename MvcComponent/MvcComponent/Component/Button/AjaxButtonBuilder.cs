using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace MvcComponent.Component
{
    public class AjaxButtonBuilder : BaseBuilder<AjaxButtonComponent, AjaxButtonBuilder>
    {
        public AjaxButtonBuilder(AjaxButtonComponent component)
            : base(component)
        {
            if (string.IsNullOrEmpty(Component.Name))
            {
                GenerateId();
            }
        }

        public AjaxButtonBuilder Text(string text)
        {
            this.Component.Text = text;
            return this;
        }

        public AjaxButtonBuilder Ajax(string action, string controller, string httpType, object routeValue)
        {
            this.Component.Options.Url = string.Format("{0}/{1}", controller, action);
            Component.Options.DataType = "json";
            Component.Options.Data = routeValue;
            Component.Options.HttpMethod = httpType;
            Component.Options.Cache = false;
            return this;
        }
        



    }
}