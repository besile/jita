using System;
using System.Reflection;
using Jita.Common.Attr;

namespace Jita.Common
{
    public sealed class com_RequestParameterHelper
    {
        private static void SetProperty(object instance, PropertyInfo pi, object values)
        {
            Type t = pi.PropertyType;
            if (values == null) return;
            else
            {
                if (t.IsEnum)
                {
                    if (Enum.IsDefined(t, values)) pi.SetValue(instance, Enum.Parse(t, values.ToString()), null);
                    else return;
                }
                else if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    Type[] typeArray = t.GetGenericArguments();
                    pi.SetValue(instance, Convert.ChangeType(values, typeArray[0]), null);
                }
                else
                {
                    pi.SetValue(instance, Convert.ChangeType(values, t), null);
                }
            }
        }

        public static void ToFillByRequest<T>(T obj)
        {
            if (obj == null || !(obj is T)) return;

            PropertyInfo[] pis = typeof(T).GetProperties();
            foreach (PropertyInfo item in pis)
            {
                object[] attributes = item.GetCustomAttributes(typeof(Attr_RequestParameterAttribute), false);
                if (attributes != null)
                {
                    foreach (Attr_RequestParameterAttribute attr in attributes)
                    {
                        if (attr != null)
                        {
                            string requestValue = RequestHelper.GetString(attr.Key);
                            if (requestValue == null || requestValue.Length == 0)
                            {
                                SetProperty(obj, item, attr.DefaultValue);
                            }
                            else
                            {
                                SetProperty(obj, item, requestValue);
                            }
                        }
                    }
                }
            }
        }
    }

}
