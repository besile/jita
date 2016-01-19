namespace Jita.Memcache
{
    using Enyim.Caching;
    using Enyim.Caching.Memcached;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public class EnyimProvider : CacheProvider
    {
        private static MemcachedClient m_CacheClient;
        private long m_DefaultExpireTime = 0x4c4b40L;
        private string m_ServerList;

        public override bool Add(string cacheKey, object cacheValue)
        {
            try
            {
                return this.Add(cacheKey, cacheValue, true);
            }
            catch
            {
                return false;
            }
        }

        public override bool Add(string cacheKey, object cacheValue, bool useDefaultExpireTime)
        {
            try
            {
                if (useDefaultExpireTime)
                {
                    return CacheClient.Store(StoreMode.Set, cacheKey, cacheValue, DateTime.Now.AddMilliseconds((double)this.m_DefaultExpireTime));
                }
                return CacheClient.Store(StoreMode.Set, cacheKey, cacheValue);
            }
            catch
            {
                return false;
            }
        }

        public override bool Add(string cacheKey, object cacheValue, long numOfMilliSeconds)
        {
            return CacheClient.Store(StoreMode.Set, cacheKey, cacheValue, DateTime.Now.AddMilliseconds((double)numOfMilliSeconds));
        }

        public override object Get(string cacheKey)
        {
            try
            {
                return CacheClient.Get(cacheKey);
            }
            catch
            {
                return null;
            }
        }

        public override IDictionary<string, object> GetMultiValue(IList<string> keys)
        {
            if (keys == null)
            {
                return null;
            }
            IDictionary<string, object> dictionary = CacheClient.Get(keys);
            foreach (string str in keys)
            {
                if (!dictionary.ContainsKey(str))
                {
                    dictionary.Add(str, null);
                }
            }
            return dictionary;
        }

        private string[] GetServerArray(string servers)
        {
            servers = servers.Replace(" ", "");
            string[] array = servers.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Array.Sort<string>(array);
            return array;
        }

        public override Hashtable GetStats()
        {
            return new Hashtable();
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "EnyimProvider";
            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Enyim Provider");
            }
            base.Initialize(name, config);
            this.m_DefaultExpireTime = Convert.ToInt64(5000000);
        }

        public override bool KeyExists(string cacheKey)
        {
            if (CacheClient.Get(cacheKey) == null)
            {
                return false;
            }
            return true;
        }

        public override object Remove(string cacheKey)
        {
            try
            {
                object obj2 = CacheClient.Get(cacheKey);
                CacheClient.Remove(cacheKey);
                return obj2;
            }
            catch
            {
                return null;
            }
        }

        public override bool RemoveAll()
        {
            try
            {
                CacheClient.FlushAll();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override void Shutdown()
        {
        }

        private static MemcachedClient CacheClient
        {
            get
            {
                if (m_CacheClient == null)
                {
                    m_CacheClient = new MemcachedClient();
                }
                return m_CacheClient;
            }
        }

        public override long Count
        {
            get
            {
                return -1L;
            }
        }

        public override long DefaultExpireTime
        {
            get
            {
                return this.m_DefaultExpireTime;
            }
            set
            {
                this.m_DefaultExpireTime = value;
            }
        }

        public override string Description
        {
            get
            {
                return "Enyim Provider";
            }
        }

        public override string Name
        {
            get
            {
                return "EnyimProvider";
            }
        }

        public override string[] Servers
        {
            get
            {
                return this.GetServerArray(this.m_ServerList);
            }
        }
    }
}
