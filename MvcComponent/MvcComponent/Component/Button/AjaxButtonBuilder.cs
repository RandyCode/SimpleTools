using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcComponent.Component
{
    public class AjaxButtonBuilder : JqueryComponentBuilder<AjaxButtonOptions, AjaxButtonComponent, AjaxButtonBuilder>
    {
        public AjaxButtonBuilder(AjaxButtonComponent component) : base(component) { }

        public AjaxButtonBuilder Text(string text)
        {
            this.Component.Text = text;
            return this;
        }

        public AjaxButtonBuilder Click()
        {
            if (string.IsNullOrEmpty(Component.Name))
            {
                base.GenerateId();
            }
            Tools.RegisterStartupScript(Component.ViewContext, "$('#" + Component.Name + "').click(function () {alert('text button click'); });");
            //Component.ViewContext.TempData["StartupScripts"] = "$('#" + Component.Name + "').click(function () {alert('text button click'); });";
            return this;
        }
    }
}