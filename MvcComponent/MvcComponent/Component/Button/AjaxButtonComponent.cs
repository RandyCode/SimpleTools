using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcComponent.Component
{
    public class AjaxButtonComponent : BaseComponent
    {
        public AjaxButtonComponent(ViewContext viewContext) : base(viewContext) { Options = new AjaxButtonOptions(); }

        public string Text { get; set; }

        public AjaxButtonOptions Options { get; set; }

        public override string TagName
        {
            get
            {
                return "div";
            }
        }

        public override void RenderConent(System.Web.UI.HtmlTextWriter writer)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", "#");
            a.MergeAttribute("style", "text-decoration:none");
            a.InnerHtml = Text;
            writer.Write(a.ToString());
            Tools.RegisterStartupScript(base.ViewContext, CreateScripts(base.Name, Options));
        }

        private string CreateScripts(string id, AjaxButtonOptions options)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("$('#");
            sb.Append(id);
            sb.Append("').click(function () {"); 
            sb.Append(ConvertOption2String(options));
            sb.Append(" });");
            return sb.ToString();
        }

        private string ConvertOption2String(AjaxButtonOptions options)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("$.ajax({");
            sb.AppendFormat("url:'{0}',", options.Url);
            sb.AppendFormat("cache:{0},", options.Cache.ToString().ToLower());
            sb.AppendFormat("type:'{0}',", options.HttpMethod);
            sb.AppendFormat("dataType:'{0}',", options.DataType);
            sb.AppendFormat("data:'{0}',", "name=rand");
            sb.AppendFormat("complete:{0},", "function (data) {alert('ajax complete:'+data); }");

            sb.Append("});");
            return sb.ToString();
        }
    }
}