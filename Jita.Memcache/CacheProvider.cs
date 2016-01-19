using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Provider;

namespace Jita.Memcache
{
    public abstract class CacheProvider : ProviderBase
    {
        protected CacheProvider()
        {
        }

        public abstract bool Add(string strKey, object objValue);
        public abstract bool Add(string strKey, object objValue, bool bDefaultExpire);
        public abstract bool Add(string strKey, object objValue, long lNumofMilliSeconds);
        public abstract object Get(string strKey);
        public abstract IDictionary<string, object> GetMultiValue(IList<string> keys);
        public virtual object[] GetMultiValueArray(IList<string> keys)
        {
            if (keys == null)
            {
                return null;
            }
            IDictionary<string, object> multiValue = this.GetMultiValue(keys);
            object[] objArray = new object[keys.Count];
            for (int i = 0; i < keys.Count; i++)
            {
                objArray[i] = multiValue[keys[i]];
            }
            return objArray;
        }

        public abstract Hashtable GetStats();
        public abstract bool KeyExists(string strKey);
        public abstract object Remove(string strKey);
        public abstract bool RemoveAll();
        public abstract void Shutdown();

        public abstract long Count { get; }

        public abstract long DefaultExpireTime { get; set; }

        public abstract string[] Servers { get; }
    }
}
