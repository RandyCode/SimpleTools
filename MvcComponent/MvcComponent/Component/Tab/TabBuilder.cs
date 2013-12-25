using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcComponent.Component
{
    public class TabBuilder:BaseBuilder<TabComponent,TabBuilder>
    {
        public TabBuilder(TabComponent component) : base(component) { }

        public TabBuilder AddTab(string title, string content)
        {
            Component.Collection.Add(new TabOptions { Content = content, Title = title });
            return this;
        }

    }
}