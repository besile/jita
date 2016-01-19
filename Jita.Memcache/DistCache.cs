﻿using System.Configuration.Provider;
using System.Web.Configuration;
using Jita.Common.Config;

namespace Jita.Memcache
{
    using System.Collections;
    using System.Collections.Generic;
    internal static class DistCache
    {
        private static object _lock = new object();
        private static CacheProvider _objProvider = null;
        private static CacheProviderCollection _objProviders = null;

        public static bool Add(string strKey, object objValue)
        {
            LoadProvider();
            return _objProvider.Add(strKey, objValue);
        }

        public static bool Add(string strKey, object objValue, bool bDefaultTimeSpan)
        {
            LoadProvider();
            return _objProvider.Add(strKey, objValue, bDefaultTimeSpan);
        }

        public static bool Add(string strKey, object objValue, long lNumofMilliSeconds)
        {
            LoadProvider();
            return _objProvider.Add(strKey, objValue, lNumofMilliSeconds);
        }

        public static object Get(string strKey)
        {
            LoadProvider();
            return _objProvider.Get(strKey);
        }

        public static IDictionary<string, object> GetMultiValue(IList<string> keys)
        {
            LoadProvider();
            return _objProvider.GetMultiValue(keys);
        }

        public static object[] GetMultiValueArray(IList<string> keys)
        {
            LoadProvider();
            return _objProvider.GetMultiValueArray(keys);
        }

        public static Hashtable GetStats()
        {
            LoadProvider();
            return _objProvider.GetStats();
        }

        public static bool KeyExists(string strKey)
        {
            LoadProvider();
            return _objProvider.KeyExists(strKey);
        }

        private static void LoadProvider()
        {
            if (_objProvider == null)
            {
                lock (_lock)
                {
                    if (_objProvider == null)
                    {
                        CacheConfig cacheConfig = CommonPlatformConfiguration.GetCacheConfig();
                        _objProviders = new CacheProviderCollection();
                        ProvidersHelper.InstantiateProviders(cacheConfig.Providers, _objProviders, typeof(CacheProvider));
                        _objProvider = _objProviders[cacheConfig.DefaultProvider];
                        if (_objProvider == null)
                        {
                            throw new ProviderException("Unable to load default cache provider");
                        }
                    }
                }
            }
        }

        public static object Remove(string strKey)
        {
            LoadProvider();
            return _objProvider.Remove(strKey);
        }

        public static void RemoveAll()
        {
            LoadProvider();
            _objProvider.RemoveAll();
        }

        public static void Shutdown()
        {
            LoadProvider();
            _objProvider.Shutdown();
            _objProvider = null;
        }

        public static long Count
        {
            get
            {
                LoadProvider();
                return _objProvider.Count;
            }
        }

        public static long DefaultExpireTime
        {
            get
            {
                LoadProvider();
                return _objProvider.DefaultExpireTime;
            }
            set
            {
                LoadProvider();
                _objProvider.DefaultExpireTime = value;
            }
        }

        public static string[] Servers
        {
            get
            {
                LoadProvider();
                return _objProvider.Servers;
            }
        }
    }
}
