using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcComponent.Component
{
    public class JqueryComponentBuilder<TOptions, TComponent, Tbuilder> : BaseBuilder<TComponent, Tbuilder>
        where TComponent : BaseComponent
        where Tbuilder : BaseBuilder<TComponent, Tbuilder>
    {
        //Fields
        protected TOptions _options;

        //Properties
        protected string JquerySelector
        {
            get { return ("$(\"#" + base.Component.Name + "\")"); }
        }

        //Methods
        public JqueryComponentBuilder(TComponent component, ViewContext context, IViewDataContainer container):base(component)
        {       
        }

        public JqueryComponentBuilder(TComponent component):base(component)
        {
        }

        protected Tbuilder Options(Action<TOptions> value)
        {
            if (this._options == null)
            {
                this._options = Activator.CreateInstance<TOptions>();
            }
            value(this._options);
            return this as Tbuilder;
        }

        public override void Render()
        {
            base.Render();
        }




    }
}