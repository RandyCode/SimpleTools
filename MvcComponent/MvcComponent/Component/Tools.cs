using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                (dictionary = viewContext.TempData)["InitThemeScripts"] = dictionary["InitThemeScripts"] + script;
            }
            else
            {
                viewContext.TempData["InitThemeScripts"] = script;
            }
        }

        //初始化主題屬性
        public static string Init2JsObject()
        {
            StringBuilder sb = new StringBuilder();
            //JSThemeProperty.TabInColor = "yellowgreen";
            //JSThemeProperty.TabOutColor = "lightyellow";
            if (!string.IsNullOrEmpty(JSThemeProperty.TabInColor))
                sb.Append(MergeThemeProp("tabInColor", JSThemeProperty.TabInColor));

            if (!string.IsNullOrEmpty(JSThemeProperty.TabInColor))
                sb.Append(MergeThemeProp("tabOutColor", JSThemeProperty.TabOutColor));

            return sb.ToString();
        }

        private static string MergeThemeProp(string key, string value)
        {
            return string.Format("theme.{0}='{1}';", key, value);
        }

    }
}