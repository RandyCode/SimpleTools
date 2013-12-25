using MvcComponent.Component.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcComponent.Component.Attributes
{
    public class JqueryOptionAttribute:Attribute
    {
        private JavaScriptTypesEnum _valueType;

        public JavaScriptTypesEnum ValueType
        {
            get { return _valueType; }
            set { _valueType = value; }
        }

        public bool AllowNull { get; set; }

        public object DefaultValue { get; set; }

        public string Name { get; set; }

        public JqueryOptionAttribute(string name)
        {
            this._valueType = JavaScriptTypesEnum.String;
            this.Name = name;
        }

        public JqueryOptionAttribute(string name,object defaultValue)
        {
            this._valueType = JavaScriptTypesEnum.String;
            this.Name = name;
            this.DefaultValue = defaultValue;
        }
    }
}