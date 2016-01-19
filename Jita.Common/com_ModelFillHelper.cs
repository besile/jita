using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Jita.Common.Attr;

namespace Jita.Common
{
    /// <summary>
    /// Class ModelFillHelper.
    /// </summary>
    public class com_ModelFillHelper
    {
        /// <summary>
        /// Sets the property value.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="pi">The pi.</param>
        /// <param name="rc">The source.</param>
        private static void SetPropertyValue(object instance, PropertyInfo pi, string rc)
        {
            Type t = pi.PropertyType;
            if (t.IsEnum)
            {
                int eInt;
                if (Enum.IsDefined(t, rc) || int.TryParse(rc, out eInt))
                {
                    pi.SetValue(instance, Enum.Parse(t, rc), null);
                }
                else
                {
                    pi.SetValue(instance, Activator.CreateInstance(t), null);
                }
            }
            else if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                Type[] typeArray = t.GetGenericArguments();
                pi.SetValue(instance, Convert.ChangeType(rc, typeArray[0]), null);
            }
            else if (t == typeof(Guid))
            {
                Guid result = default(Guid);
                Guid.TryParse(rc, out result);
                pi.SetValue(instance, result, null);
            }
            else
            {
                pi.SetValue(instance, Convert.ChangeType(rc, t), null);
            }
        }

        /// <summary>
        /// Matches the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row">The row.</param>
        /// <param name="columns">The columns.</param>
        /// <returns>``0.</returns>
        private static T MatchProperty<T>(DataRow row, DataColumnCollection columns) where T : new()
        {
            if (row == null) return default(T);
            if (columns == null || columns.Count == 0) return default(T);

            PropertyInfo[] pis = (typeof(T)).GetProperties();
            T obj = new T();
            foreach (PropertyInfo item in pis)
            {
                object[] attrs = item.GetCustomAttributes(typeof(Attr_MatchFieldAttribute), false);
                if (attrs == null || attrs.Length == 0) continue;
                else
                {
                    foreach (Attr_MatchFieldAttribute attr in attrs)
                    {
                        if (attr != null)
                        {
                            string key = attr.Key;
                            if (columns.Contains(key) && row[key] != DBNull.Value)
                            {
                                SetPropertyValue(obj, item, row[key].ToString());
                            }
                            else
                            {
                                if (item.PropertyType == typeof(string))
                                    item.SetValue(obj, string.Empty, null);
                            }
                        }
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// Fills the model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt">The dt.</param>
        /// <returns>``0.</returns>
        public static T FillModel<T>(DataTable dt) where T : new()
        {
            if (dt == null || dt.Rows.Count == 0) return default(T);
            else
            {
                return MatchProperty<T>(dt.Rows[0], dt.Columns);
            }
        }

        /// <summary>
        /// Fills the model list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt">The dt.</param>
        /// <returns>List{``0}.</returns>
        public static List<T> FillModelList<T>(DataTable dt) where T : new()
        {
            if (dt == null || dt.Rows.Count == 0) return null;
            List<T> list = new List<T>(dt.Rows.Count);
            DataColumnCollection cloums = dt.Columns;
            foreach (DataRow row in dt.Rows)
            {
                var obj = MatchProperty<T>(row, cloums);
                list.Add(obj);
            }
            return list;
        }
    }
}
