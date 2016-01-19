// ***********************************************************************
// Assembly         : KouBei.Cache.Service
// Author           : baiyu
// Created          : 12-01-2014
//
// Last Modified By : baiyu
// Last Modified On : 12-10-2014
// ***********************************************************************
// <copyright file="srv_CacheManager.cs" company="bitauto.com">
//     Copyright (c) bitauto.com. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
/// <summary>
/// The Service namespace.
/// </summary>
using Jita.Cache.Service;
using Jita.Cache.Service.model;
using Jita.Common;
using Jita.Common.Attr;

namespace KouBei.Cache.Service
{
    /// <summary>
    /// Class srv_CacheManager. This class cannot be inherited.
    /// </summary>
    public sealed class srv_CacheManager
    {
        #region 私有
        /// <summary>
        /// Prevents a default instance of the <see cref="srv_CacheManager"/> class from being created.
        /// </summary>
        private srv_CacheManager() { ;}

        /// <summary>
        /// 将调用得到的实体对象缓存键拼接成字符串数组
        /// <code>baiyu 2014-12-19</code>
        /// </summary>
        /// <param name="seed">The seed.</param>
        /// <returns>System.String[][].</returns>
        private static string[] formatCacheKeySeed(object seed)
        {
            Type t = seed.GetType();

            if (t.IsCollection())
            {
                var arry = (IEnumerable)seed;
                List<string> list = new List<string>();
                foreach (var item in arry)
                {
                    list.Add(item.ToString());
                }
                return list.ToArray();
            }
            else
            {
                return new string[] { seed.ToString() };
            }
        }

        /// <summary>
        /// 通过配置文件构建缓存数据
        /// <code>baiyu 2014-12-19</code>
        /// </summary>
        /// <param name="isCache">if set to <c>true</c> [is cache].</param>
        /// <param name="cacheMethod">The cache method.</param>
        /// <param name="cacheKeys">The cache keys.</param>
        /// <returns>System.Object.</returns>
        private static object buildCache(bool isCache, MethodInfo cacheMethod, string[] cacheKeys)
        {
            if (cacheMethod == null) return null;
            if (cacheKeys == null || cacheKeys.Length == 0) return null;


            object[] cacheAttrs = cacheMethod.GetCustomAttributes(typeof(Attr_CacheDataAttribute), false);
            if (cacheAttrs == null) return null;

            object data = null;
            foreach (Attr_CacheDataAttribute attr in cacheAttrs)
            {
                if (attr == null) return null;

                string key = attr.WhatCase;

                m_CacheConfig invokeConfig = srv_CacheConfig.GetConfig(key);
                data = invokeData(isCache, invokeConfig, cacheKeys);
            }

            return data;
        }

        /// <summary>
        /// 根据配置获取源数据
        /// <code>baiyu 2014-12-19</code>
        /// </summary>
        /// <param name="invokeConfig">The invoke configuration.</param>
        /// <param name="prams">The prams.</param>
        /// <returns>System.Object.</returns>
        private static object invokeData(m_CacheConfig invokeConfig, params string[] prams)
        {
            if (invokeConfig == null) return null;
            if (prams == null || prams.Length == 0) return null;

            m_CacheConfig mcc = invokeConfig;

            string invokePrams = string.Join(",", prams);

            //Type type = Type.GetType(Assembly.CreateQualifiedName(mcc.AssemblyPath, mcc.ClassName));
            //MethodInfo mi = type.GetMethod(mcc.MethodName);
            //var instance = mi.IsStatic ? null : Activator.CreateInstance(type);

            ArrayList objMethod = com_GlobalDic.Pop(mcc.ID) as ArrayList;
            MethodInfo mi = objMethod[1] as MethodInfo;

            ParameterInfo pi = mi.GetParameters()[0];
            var invokeP = Convert.ChangeType(invokePrams, pi.ParameterType);
            object[] parameter = new object[] { invokeP };

            //com_DynamicMethodExecutor executor = new com_DynamicMethodExecutor(mi);
            //var obj = executor.Execute(instance, parameter);

            var obj = mi.Invoke(objMethod[0], new object[] { invokeP });

            return obj;
        }

        /// <summary>
        /// 通过配置获取源数据，缓存介入，并对缓存进行填充
        /// <code>baiyu 2014-12-19</code>
        /// </summary>
        /// <param name="isCache">if set to <c>true</c> [is cache].</param>
        /// <param name="invokeConfig">The invoke configuration.</param>
        /// <param name="prams">The prams.</param>
        /// <returns>System.Object.</returns>
        private static object invokeData(bool isCache, m_CacheConfig invokeConfig, params string[] prams)
        {
            if (invokeConfig == null) return null;
            if (prams == null || prams.Length == 0) return null;

            m_CacheConfig mcc = invokeConfig;

            if (!isCache)
            {
                var obj = invokeData(invokeConfig, prams);
                if (obj == null) return obj;
                if (com_TypeHelper.IsCollection(obj.GetType()))
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>(prams.Length);
                    ArrayList al = new ArrayList(prams.Length);

                    var arrayObj = (IEnumerable)obj;
                    foreach (var item in arrayObj)
                    {
                        var t = item.GetType();
                        PropertyInfo[] pis = t.GetProperties();
                        if (pis == null) return null;
                        foreach (var pi in pis)
                        {
                            object[] cacheAttrs = pi.GetCustomAttributes(typeof(Attr_CachePrimaryKeyAttribute), false);
                            if (cacheAttrs == null || cacheAttrs.Length == 0) continue;

                            foreach (Attr_CachePrimaryKeyAttribute attr in cacheAttrs)
                            {
                                if (attr == null) return null;
                                string key = pi.GetValue(item, null).ToString();
                                dic.Add(key, item);
                            }
                        }
                    }
                    foreach (var item in prams)
                    {
                        if (dic.ContainsKey(item) && dic[item] != null)
                            al.Add(dic[item]);
                    }
                    return al.ToArray();
                }
                return obj;
            }
            else
            {
                string pre = mcc.Pre;

                Dictionary<string, string> keyDic = new Dictionary<string, string>(prams.Length);

                foreach (var p in prams)
                {
                    string cacheKey = string.Concat(pre, p);
                    if (!keyDic.ContainsKey(cacheKey))
                        keyDic.Add(cacheKey, p);
                }
                string[] cacheKeys = keyDic.Keys.ToArray();
                IDictionary<string, object> dic = srv_MemcacheCacheManager.Get(cacheKeys);
                if (dic == null || dic.Count == 0) return null;
                List<string> addObjKeys = new List<string>();
                foreach (KeyValuePair<string, object> item in dic)
                {
                    var obj = item.Value;
                    if (obj == null) addObjKeys.Add(keyDic[item.Key]);
                }

                if (addObjKeys.Count == 0)
                {
                    //因Memcache批量获取数据不排序，需要按照Key重新排序
                    ArrayList all = new ArrayList(prams.Length);
                    foreach (string key in cacheKeys)
                    {
                        if (dic.ContainsKey(key) && dic[key] != null)
                            all.Add(dic[key]);
                    }
                    return all.ToArray();
                }
                else
                {
                    var obj = invokeData(mcc, addObjKeys.ToArray());
                    if (obj != null)
                    {
                        Type objType = obj.GetType();

                        if (com_TypeHelper.IsCollection(objType))
                        {
                            var arryObj = (IEnumerable)obj;
                            foreach (var o in arryObj)
                            {
                                string cacheKey = string.Concat(pre, o.ToPrimaryKey());
                                srv_MemcacheCacheManager.Add(cacheKey, o, mcc.ExpTime);
                                dic[cacheKey] = o;
                            }
                        }
                        else
                        {
                            string cacheKey = string.Concat(pre, obj.ToPrimaryKey());
                            srv_MemcacheCacheManager.Add(cacheKey, obj, mcc.ExpTime);
                            dic[cacheKey] = obj;
                        }
                    }
                    //因Memcache批量获取数据不排序，需要按照Key重新排序
                    ArrayList all = new ArrayList(prams.Length);
                    foreach (string key in cacheKeys)
                    {
                        if (dic.ContainsKey(key) && dic[key] != null)
                            all.Add(dic[key]);
                    }
                    return all.ToArray();
                }
            }
        }
        #endregion

        #region 对外提供构建缓存方法
        /// <summary>
        /// 缓存散列对外接口
        /// <code>baiyu 2014-12-19</code>
        /// </summary>
        /// <param name="isCache">if set to <c>true</c> [is cache].</param>
        /// <param name="method">The method.</param>
        /// <param name="seed">The seed.</param>
        /// <returns>System.Object.</returns>
        public static object CacheData(bool isCache, MethodInfo method, object seed)
        {
            if (method == null) return null;
            if (seed == null) return null;
            string[] cacheKeySeed = formatCacheKeySeed(seed);
            var obj = buildCache(isCache, method, cacheKeySeed);
            return obj;
        }
        #endregion

        #region 缓存删除

        /// <summary>
        /// 通过配置文件删除缓存
        /// </summary>
        public static void RemoveCache(bool isCache, MethodInfo method, object[] prams)
        {
            if (method == null) return;
            if (prams == null) return;
            //if (!isCache) return;

            object[] removeCacheAttrs = method.GetCustomAttributes(typeof(Attr_RemoveCacheAttribute), false);
            if (removeCacheAttrs == null || removeCacheAttrs.Length == 0) return;

            foreach (Attr_RemoveCacheAttribute attr in removeCacheAttrs)
            {
                if (attr == null) return;
                if (prams == null || prams.Length == 0) return;
                if (attr.ArgsIndex < 0 || attr.ArgsIndex >= prams.Length) return;

                string configCase = attr.WhatCase;
                m_CacheRemoveConfig invokeConfig = srv_RemoveCacheConfig.GetConfig(configCase);
                object pra = null;

                if (string.IsNullOrWhiteSpace(attr.Key))
                {
                    pra = prams[attr.ArgsIndex];
                }
                else
                {
                    Type type = prams[attr.ArgsIndex].GetType();
                    PropertyInfo pi = type.GetProperty(attr.Key);
                    pra = pi.GetValue(prams[attr.ArgsIndex], null);
                }

                invokeData(invokeConfig, pra);
            }
        }

        /// <summary>
        /// 删除缓存操作
        /// </summary>
        private static void invokeData(m_CacheRemoveConfig invokeConfig, object pram)
        {
            if (invokeConfig == null) return;
            if (pram == null) return;

            m_CacheRemoveConfig mcc = invokeConfig;

            //删除整形键缓存
            var p = pram.ToString().Split(',');
            foreach (var item in p)
            {
                string cacheKey = string.Concat(mcc.Pre, item.ToString());
                srv_MemcacheCacheManager.Remove(cacheKey);
            }
        }

        
        #endregion
    }
}
