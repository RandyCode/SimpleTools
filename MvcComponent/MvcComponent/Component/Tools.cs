using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcComponent.Component
{
    public static class Tools
    {
        public static void RegisterStartupScript(ViewContext viewContext, string script)
        {
            if (viewContext.TempData["StartupScripts"] != null)
            {
                TempDataDictionary dictionary;
                (dictionary = viewContext.TempData)["StartupScripts"] = dictionary["StartupScripts"] + "\r\n" + script;
            }
            else
            {
                viewContext.TempData["StartupScripts"] = script;
            }
        }

        public static void JQuery(string selector, string pluginName, object options)
        {
            string str = selector;
            if (!selector.StartsWith(".") && !selector.StartsWith("#") && !selector.Equals("body") && !selector.Equals("document"))
            {
                str = "#" + selector;
            }
        }
    }
}