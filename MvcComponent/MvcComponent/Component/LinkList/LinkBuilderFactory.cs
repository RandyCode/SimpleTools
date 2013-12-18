using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcComponent.Component
{
    public class LinkBuilderFactory
    {
        public LinkListComponent ItemContainer { get; set; }

        public LinkBuilderFactory(LinkListComponent container)
        {
            this.ItemContainer = container;
        }

        public LinkBuilder Add()
        {
            var item = new LinkComponent(ItemContainer.ViewContext);
            ItemContainer.Links.Add(item);
            return new LinkBuilder(item);
        }
    }
}