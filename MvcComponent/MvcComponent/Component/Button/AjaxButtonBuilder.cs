using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcComponent.Component
{
    public class AjaxButtonBuilder :BaseBuilder<AjaxButtonComponent,AjaxButtonBuilder>
    {
        public AjaxButtonBuilder(AjaxButtonComponent component) : base(component) 
        {
            if (string.IsNullOrEmpty(Component.Name))
            {
                base.GenerateId();
            }
        }

        public AjaxButtonBuilder Text(string text)
        {
            this.Component.Text = text;
            return this;
        }

   
    }
}