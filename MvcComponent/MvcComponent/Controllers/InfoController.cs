using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcComponent.Controllers
{
    public class InfoController : Controller
    {
        //
        // GET: /Info/

        [HttpGet]
        public ActionResult Index()
        {
            List<Models.Foo> list = new List<Models.Foo>()
            {
                new Models.Foo{ Key="1",Value="index"},
                new Models.Foo{Key="2",Value="foo2"}
            };
            return View(list);
            //return View();
        }

        [HttpPost]
        public ActionResult Index(string id)
        {
            List<Models.Foo> list = new List<Models.Foo>()
            {
                new Models.Foo{ Key="3",Value="foo1"},
                new Models.Foo{Key="4",Value="foo2"}
            };
            return View(list);
        }

    }
}
