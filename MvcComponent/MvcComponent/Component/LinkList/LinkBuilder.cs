using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcComponent.Component
{
    public class LinkBuilder : BaseBuilder<LinkComponent, LinkBuilder>
    {
        public LinkBuilder(LinkComponent component):base(component) { }

        public LinkBuilder Title(string title)
        {
            this.Component.Title = title;
            return this;
        }

        public LinkBuilder Url(string url)
        { 
            this.Component.Url = url;
            return this;
        }

    }
}