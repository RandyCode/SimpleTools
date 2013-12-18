using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace MvcComponent.Component
{
    public class LinkListBuilder:BaseBuilder<LinkListComponent,LinkListBuilder>
    {
        public LinkListBuilder(LinkListComponent component) : base(component) { }

        public LinkListBuilder Items(Action<LinkBuilderFactory> items)
        {
            var factory = new LinkBuilderFactory(this.Component);

            if (items != null)
                items.Invoke(factory);
           
            return this;
        }

        public LinkListBuilder Bind<T, TKey, TValue>(IEnumerable<T> model, Expression<Func<T, TKey>> keySelector, Expression<Func<T, TValue>> valueSelector)
    where T : class
        {
            if (model != null)
            {
                this.Items(items =>

                {
                    foreach (var t in model)
                    {
                        items.Add()
                                .Title(keySelector.Compile().Invoke(t) as string)
                                .Url(valueSelector.Compile().Invoke(t) as string);
                    }
                });
            }
            return this;
        }

    }
}