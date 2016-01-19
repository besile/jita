using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jita.Common
{
    public static class com_TypeHelper
    {
        static com_TypeHelper() { ;}
        public static bool IsCollection(this Type t)
        {
            return ((!t.Equals(typeof(string)))
                && (t.IsArray
                    || (t.GetInterface("IEnumerable") != null
                        && t.GetInterface("IEnumerable").IsAssignableFrom(t)
                )));
        }
    }
}
