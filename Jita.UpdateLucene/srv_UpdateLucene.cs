using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Jita.Cache.Service.bll;
using Jita.Common.Attr;

namespace Jita.UpdateLucene
{
    public class srv_UpdateLucene
    {
        /// <summary>
        /// 添加solr
        /// </summary>
        /// <param name="mi"></param>
        /// <param name="prams"></param>
        public static void AddHumorLucene(MethodInfo mi, object[] prams)
        {
            object[] attrs = mi.GetCustomAttributes(typeof(Attr_AddLuceneAttribute), false);
            foreach (Attr_AddLuceneAttribute attr in attrs)
            {
                if (attr == null)
                {
                    continue;
                }
                int index = attr.PramIndex;
                if (index >= 0 && index < prams.Length)
                {
                    //直接是传过来参数
                    if (string.IsNullOrWhiteSpace(attr.Key))
                    {
                        string id = prams[index].ToString();
                        srv_HumorLucene.AddLuceneHumor(Convert.ToInt32(id));
                    }
                    else
                    {
                        Type type = attr.Key.GetType();
                        PropertyInfo pi = type.GetProperty(attr.Key);
                        var id = pi.GetValue(prams[index], null);
                        srv_HumorLucene.AddLuceneHumor(Convert.ToInt32(id));
                    }
                }
            }
        }
    }
}
