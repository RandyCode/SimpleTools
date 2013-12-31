using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcComponent.Component
{
    public static class JSThemeProperty
    {
        //tab property
        //tabIndex: -1,
        //tabInColor: "lightgreen",
        //tabOutColor:"green"

        public static string TabInColor { get; set; }

        public static string TabOutColor { get; set; }

        public static string TabFontColor { get; set; }

    }


    //public abstract class JQueryBuilder2<TOptions, TComponent, TBuilder> : BaseBuilder<TComponent, TBuilder>
    //    where TComponent : BaseComponent
    //    where TBuilder : BaseBuilder<TComponent, TBuilder>
    //{
    //    protected TOptions options;

    //    public TBuilder Options(Action<TOptions> value)
    //    {
    //        if (this.options == null)
    //            this.options = Activator.CreateInstance<TOptions>();

    //        value(this.options);
    //        return this as TBuilder;
    //    }
    //}
}



