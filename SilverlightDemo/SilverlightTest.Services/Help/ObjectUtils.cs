using System;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace SilverlightTest.Services.Help
{
    public static class ObjectUtils
    {
        public static bool TryCopyTo(object source, object target)
        {
            PropertyInfo[] sourceProperies = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo targetProperty;
            try
            {
                for (int i = 0; i < sourceProperies.Length; i++)
                {
                    targetProperty = target.GetType().GetProperty(sourceProperies[i].Name, BindingFlags.Instance | BindingFlags.Public);
                    if (targetProperty != null && targetProperty.CanWrite && sourceProperies[i].CanRead)
                    {
                        try
                        {
                            targetProperty.SetValue(target, sourceProperies[i].GetValue(source, null), null);
                        }
                        catch (InvalidOperationException)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool CleanPropertyValue(this object model, string propertyName)
        {
            var type = model.GetType();
            var emptyModel = type.Assembly.CreateInstance(type.FullName);
            var targetProperty = model.GetType().GetProperty(propertyName);

            if (targetProperty != null && targetProperty.CanWrite)
            {
                try
                {
                    targetProperty.SetValue(model, targetProperty.GetValue(emptyModel, null), null);
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
            }

            return true;
        }


        public static T CloneEx<T>(object src) where T : new()
        {

            PropertyInfo[] infos;
            PropertyInfo property;
            int a, b;

            T t;

            t = new T();

            if (src == null)
                return t;
            infos = src.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            b = infos.Length;
            for (a = 0; a < b; a++)
            {
                property = t.GetType().GetProperty(infos[a].Name, BindingFlags.Instance | BindingFlags.Public);

                if (infos[a].CanRead && infos[a].CanWrite && property != null)
                {
                    SetValue(t, infos[a].Name, infos[a].GetValue(src, null));
                }
            }
            return t;
        }

        /// <summary>
        /// 集合,數列Null的話返回空集合,數列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T DefaultIfNull<T>(this T list) where T : class ,IEnumerable, new()
        {
            return list ?? new T();
        }

        public static void SetValue(object obj, string propertyName, object value)
        {
            if (value == null)
            {
                return;
            }

            PropertyInfo info;
            info = obj.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
            if (info != null && info.CanWrite)
            {
                try
                {
                    //object tmp = Convert.ChangeType(value, info.PropertyType, null);

                    info.SetValue(obj, value, null);
                }
                catch
                {

                }
            }
        }

        public static object GetValue(object obj, string propertyName)
        {
            object value = null;
            PropertyInfo info;
            info = obj.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
            if (info != null && info.CanRead)
                value = info.GetValue(obj, null);

            return value;
        }

        public static string GetPropertyName<T, TProperty>(T obj, Expression<Func<T, TProperty>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression != null)
            {
                return memberExpression.Member.Name;
            }
            else
                return string.Empty;
        }

    }
}
