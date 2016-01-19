using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jita.Common
{
    public class com_GlobalDic
    {
        private static Hashtable dic = null;
        private static readonly object syn = new object();

        static com_GlobalDic()
        {
            dic = Hashtable.Synchronized(new Hashtable());
        }

        public static void Push(string key, object value)
        {
            if (dic.ContainsKey(key))
            {
                lock (syn)
                {
                    dic[key] = value;
                }
            }
            else
            {
                dic.Add(key, value);
            }
        }

        public static object Pop(string key)
        {
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            else
            {
                return null;
            }
        }
    }
}
