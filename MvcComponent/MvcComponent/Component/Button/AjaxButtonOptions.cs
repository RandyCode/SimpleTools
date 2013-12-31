using MvcComponent.Component.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcComponent.Component
{
    public class AjaxButtonOptions
    {
        //$.ajax({
        //            url: "info/index",
        //            cache: false,
        //            type: "post",
        //            dataType:"json",
        //            data: "name=randy&id=1",
        //            complete: function (data) {
        //                alert(data); 
        //            }
        //        });
        [JqueryOption("type")]
        public string HttpMethod { get; set; }

        [JqueryOption("url")]
        public string Url { get; set; }

        [JqueryOption("dataType")]
        public string DataType { get; set; }
      
        [JqueryOption("cache")]
        public bool Cache { get; set; }

        [JqueryOption("data")]
        public object Data { get; set; }

        [JqueryOption("complete")]
        public string Complete { get; set; }
    }

}