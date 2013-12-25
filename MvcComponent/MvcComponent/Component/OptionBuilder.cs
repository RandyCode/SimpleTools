using MvcComponent.Component.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace MvcComponent.Component
{
    public class OptionBuilder
    {
        // Fields
        private Dictionary<string, string> opts = new Dictionary<string, string>();

        // Methods
        public OptionBuilder AddFunctionOption(string name, Action<StringBuilder> value)
        {
            return this.AddFunctionOption(name, null, value);
        }

        public OptionBuilder AddFunctionOption(string name, string script)
        {
            return this.AddFunctionOption(name, script, null);
        }

        public OptionBuilder AddFunctionOption(string name, string script, string[] functionParams)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("function");
            builder.Append("(");
            if ((functionParams != null) && (functionParams.Length > 0))
            {
                builder.Append(string.Join(",", functionParams));
            }
            builder.Append(")");
            builder.Append("{" + script + "}");
            return this.AddOption(name, builder.ToString());
        }

        public OptionBuilder AddFunctionOption(string name, string[] functionParams, Action<StringBuilder> value)
        {
            StringBuilder builder = new StringBuilder();
            value(builder);
            return this.AddFunctionOption(name, builder.ToString(), functionParams);
        }

        public OptionBuilder AddJSON(string name, object value)
        {
            if (value.GetType() == typeof(string))
            {
                return this.AddOption(name, value.ToString(), false);
            }
            if (value.GetType() == typeof(OptionBuilder))
            {
                return this.AddOption(name, ((OptionBuilder)value).ToString(), false);
            }
            return this.AddOption(name, new JavaScriptSerializer().Serialize(value), false);
        }

        public OptionBuilder AddOption(string name, bool value)
        {
            this.opts.Add(name, value.ToString().ToLower());
            return this;
        }

        public OptionBuilder AddOption(string name, decimal value)
        {
            this.opts.Add(name, value.ToString());
            return this;
        }

        public OptionBuilder AddOption(string name, double value)
        {
            this.opts.Add(name, value.ToString());
            return this;
        }

        //public OptionBuilder AddOption(string name, Enum value)
        //{
        //    this.opts.Add(name, "'" + value.ToString().ToLower() + "'");
        //    return this;
        //}

        public OptionBuilder AddOption(string name, int value)
        {
            this.opts.Add(name, value.ToString());
            return this;
        }

        public OptionBuilder AddOption(string name, object value)
        {
            if (value != null)
            {
                Type type = value.GetType();
                if (type == typeof(string))
                {
                    this.AddOption(name, value.ToString(), true);
                    return this;
                }
                if (type == typeof(string[]))
                {
                    this.AddOption(name, (string[])value);
                    return this;
                }
                if (type == typeof(int))
                {
                    this.AddOption(name, (int)value);
                    return this;
                }
                if (type == typeof(int[]))
                {
                    this.AddOption(name, (int[])value);
                    return this;
                }
                if (type == typeof(float))
                {
                    this.AddOption(name, (float)value);
                    return this;
                }
                if (type == typeof(float[]))
                {
                    this.AddOption(name, (float[])value);
                    return this;
                }
                if (type == typeof(double))
                {
                    this.AddOption(name, (double)value);
                    return this;
                }
                if (type == typeof(decimal))
                {
                    this.AddOption(name, (decimal)value);
                    return this;
                }
                if (type == typeof(bool))
                {
                    this.AddOption(name, (bool)value);
                    return this;
                }
                //if (type.IsEnum)
                //{
                //    this.AddOption(name, (Enum)value);
                //    return this;
                //}
                if (type.IsGenericType)
                {
                    this.AddJSON(name, value);
                    return this;
                }
            }
            return this;
        }

        public OptionBuilder AddOption(string name, float value)
        {
            this.opts.Add(name, value.ToString());
            return this;
        }

        public OptionBuilder AddOption(string name, string value)
        {
            if (this.opts.ContainsKey(name))
            {
                this.opts[name] = value;
            }
            else
            {
                this.opts.Add(name, value);
            }
            return this;
        }

        public OptionBuilder AddOption(string name, int[] value)
        {
            string[] strArray = new string[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                strArray[i] = value[i].ToString();
            }
            this.opts.Add(name, "[" + string.Join(",", strArray) + "]");
            return this;
        }

        public OptionBuilder AddOption(string name, float[] value)
        {
            string[] strArray = new string[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                strArray[i] = value[i].ToString();
            }
            this.opts.Add(name, "[" + string.Join(",", strArray) + "]");
            return this;
        }

        public void AddOption(string name, string[] value)
        {
            string[] strArray = new string[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i].StartsWith("'"))
                {
                    strArray[i] = value[i];
                }
                else
                {
                    strArray[i] = "'" + value[i] + "'";
                }
            }
            this.opts.Add(name, "[" + string.Join(",", strArray) + "]");
        }

        public OptionBuilder AddOption(string name, string value, bool format)
        {
            if (format)
            {
                this.AddOption(name, "'" + value + "'");
            }
            else
            {
                this.AddOption(name, value);
            }
            return this;
        }

        public OptionBuilder AddOption(string name, Unit value, bool isStringValue)
        {
            if (isStringValue)
            {
                this.AddOption(name, value.ToString(), true);
            }
            else
            {
                this.AddOption(name, value.Value);
            }
            return this;
        }


        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (this.opts.Count > 0)
            {
                builder.Append("{");
                int num = 0;
                foreach (string str in this.opts.Keys)
                {
                    if (num > 0)
                    {
                        builder.Append(",");
                    }
                    builder.Append(str + ":" + this.opts[str]);
                    num++;
                }
                builder.Append("}");
            }
            return builder.ToString();
        }

        // Properties
        public Dictionary<string, string> Options
        {
            get
            {
                return this.opts;
            }
        }
    }





}