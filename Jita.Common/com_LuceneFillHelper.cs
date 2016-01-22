using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Jita.Common.Attr;

namespace Jita.Common
{
    public class com_LuceneFillHelper
    {
        public static void ToFillByLucene<T>(T obj)
        {
            PropertyInfo[] pis = typeof(T).GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                object[] attributes = pi.GetCustomAttributes(typeof(Attr_LuceneAttribute), false);
                foreach (Attr_LuceneAttribute attribute in attributes)
                {
                    if (attribute == null)
                    {
                        continue;
                    }

                }
            }
        }
    }
}
