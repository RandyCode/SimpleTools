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

        public static void RegisterThemeScript(ViewContext viewContext, string script)
        {
            if (viewContext.TempData["InitThemeScripts"] != null)
            {
                TempDataDictionary dictionary;
                (dictionary = viewContext.TempData)["InitThemeScripts"] = dictionary["InitThemeScripts"] + "\r\n" + script;
            }
            else
            {
                viewContext.TempData["InitThemeScripts"] = script;
            }
        }

        //初始化主題屬性
        public static string Init2JsObject()
        {

            return "randy";
        }

    }
}