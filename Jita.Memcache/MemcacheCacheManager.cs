using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jita.Memcache
{
    /// <summary>
    /// memcache管理
    /// </summary>
    public sealed class MemcacheCacheManager
    {
        public MemcacheCacheManager() { ;}

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireMin"></param>
        public static void Add(string key, object value, int expireMin)
        {
            DistCacheWrapper.Insert(key, value, expireMin * 1000);
        }

        /// <summary>
        /// 取单个
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return DistCacheWrapper.Get(key);
        }

        /// <summary>
        /// 获取多个
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static IDictionary<string, object> Get(params string[] keys)
        {
            return DistCacheWrapper.GetMultiValue(keys.Distinct().ToList());
        }

        /// <summary>
        /// 移除缓存项
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            DistCacheWrapper.Remove(key);
        }
    }
}
