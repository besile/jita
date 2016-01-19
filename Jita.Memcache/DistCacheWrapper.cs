namespace Jita.Memcache
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;

    public class DistCacheWrapper
    {
        private string m_BusiObjIdPropertyName;
        private Type m_BusiObjType;
        private List<string[]> m_UniqueIndexes;

        public DistCacheWrapper(Type busiObjType, string busiObjIdPropertyName)
        {
            this.m_UniqueIndexes = new List<string[]>();
            this.m_BusiObjType = busiObjType;
            this.m_BusiObjIdPropertyName = busiObjIdPropertyName;
        }

        public DistCacheWrapper(Type busiObjType, string buisObjIdPropertyName, params string[] singleUniqueIdxes)
        {
            this.m_UniqueIndexes = new List<string[]>();
            this.m_BusiObjType = busiObjType;
            this.m_BusiObjIdPropertyName = buisObjIdPropertyName;
            foreach (string str in singleUniqueIdxes)
            {
                this.AddUniqueIdx(new string[] { str });
            }
        }

        public void AddUniqueIdx(params string[] uniqueIndexes)
        {
            this.m_UniqueIndexes.Add(uniqueIndexes);
        }

        private static string ComputeHash(string str)
        {
            return Convert.ToBase64String(new SHA1Managed().ComputeHash(Encoding.Default.GetBytes(str)));
        }

        private string CreateIndexCacheKey(string[] colNames, object[] colValues)
        {
            string[] objects = new string[colNames.Length];
            for (int i = 0; i < colNames.Length; i++)
            {
                objects[i] = colNames[i] + colValues[i];
            }
            return GetCacheKey(objects);
        }

        private string CreateIndexCacheKey(object persistedObject, string[] colsInOneIndex)
        {
            object[] colValues = new object[colsInOneIndex.Length];
            for (int i = 0; i < colsInOneIndex.Length; i++)
            {
                colValues[i] = GetPropertyValue(persistedObject, colsInOneIndex[i]);
            }
            return this.CreateIndexCacheKey(colsInOneIndex, colValues);
        }

        public static object Get(string cacheKey)
        {
            return DistCache.Get(cacheKey);
        }

        public object GetBusiObj(object objectId)
        {
            return DistCache.Get(GetCacheKeyByUniqueIdx(this.m_BusiObjType, this.m_BusiObjIdPropertyName, objectId));
        }

        public object GetBusiObjByMultiColsUniqueIdx(string[] indexColNames, object[] colValues)
        {
            string strKey = (string)DistCache.Get(this.CreateIndexCacheKey(indexColNames, colValues));
            return DistCache.Get(strKey);
        }

        public object GetBusiObjBySingleColUniqueIdx(string singleUniqueIdx, object singleUniqueIdxValue)
        {
            object obj2 = DistCache.Get(GetCacheKeyByUniqueIdx(this.m_BusiObjType, singleUniqueIdx, singleUniqueIdxValue));
            if (obj2 == null)
            {
                return null;
            }
            return DistCache.Get(obj2.ToString());
        }

        public IList GetBusiObjs(MethodBase yourQueryMethod, params object[] methodParams)
        {
            string cacheKeyByMethodAndParams = GetCacheKeyByMethodAndParams(yourQueryMethod, methodParams);
            ArrayRelation relation = (ArrayRelation)DistCache.Get(cacheKeyByMethodAndParams);
            if (relation == null)
            {
                return null;
            }
            IList list = null;
            if (relation.IListType.IsArray)
            {
                list = Array.CreateInstance(relation.IListType.GetElementType(), relation.Value.Count);
            }
            else
            {
                list = (IList)relation.IListType.GetConstructor(Type.EmptyTypes).Invoke(null);
            }
            for (int i = 0; i < relation.Value.Count; i++)
            {
                object obj2 = DistCache.Get(relation.Value[i].ToString());
                if (obj2 == null)
                {
                    DistCache.Remove(cacheKeyByMethodAndParams);
                    return null;
                }
                list[i] = obj2;
            }
            return list;
        }

        private static string GetCacheKey(params object[] objects)
        {
            if (objects == null)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            foreach (object obj2 in objects)
            {
                if (builder.Length > 0)
                {
                    builder.Append(":");
                }
                builder.Append((obj2 == null) ? "" : obj2);
            }
            return ComputeHash(builder.ToString());
        }

        private static string GetCacheKeyByMethodAndParams(MethodBase persistedMethod, params object[] methodParams)
        {
            return GetCacheKey(new object[] { persistedMethod.DeclaringType.ToString(), persistedMethod.ToString(), GetCacheKey(methodParams) });
        }

        private static string GetCacheKeyByObject(object busiObj, string idPropertyName)
        {
            if (busiObj == null)
            {
                throw new ApplicationException("对象不能为空！");
            }
            return GetCacheKey(new object[] { busiObj.GetType().ToString(), idPropertyName, GetPropertyValue(busiObj, idPropertyName).ToString() });
        }

        private static string GetCacheKeyByUniqueIdx(Type busiObjType, string idPropertyName, object idValue)
        {
            return GetCacheKey(new object[] { busiObjType.ToString(), idPropertyName, idValue });
        }

        public static object GetFromQueryMethod(MethodBase yourQueryMethod, params object[] methodParams)
        {
            return DistCache.Get(GetCacheKeyByMethodAndParams(yourQueryMethod, methodParams));
        }

        public static IDictionary<string, object> GetMultiValue(IList<string> keys)
        {
            return DistCache.GetMultiValue(keys);
        }

        public static object[] GetMultiValueArray(IList<string> keys)
        {
            return DistCache.GetMultiValueArray(keys);
        }

        private static object GetPropertyValue(object busiObj, string propertyName)
        {
            return busiObj.GetType().GetProperty(propertyName).GetValue(busiObj, null);
        }

        public static void Insert(string cacheKey, object value)
        {
            Insert(cacheKey, value, DistCache.DefaultExpireTime);
        }

        public static void Insert(string cacheKey, object value, long numberOfMilliSeconds)
        {
            DistCache.Add(cacheKey, value, numberOfMilliSeconds);
        }

        public void InsertBusiObj(object busiObj)
        {
            this.InsertBusiObj(busiObj, DistCache.DefaultExpireTime);
        }

        public string InsertBusiObj(object busiObj, long numberOfMilliSeconds)
        {
            string objValue = this.InsertBusiObjById(busiObj, numberOfMilliSeconds);
            foreach (string[] strArray in this.m_UniqueIndexes)
            {
                DistCache.Add(this.CreateIndexCacheKey(busiObj, strArray), objValue, numberOfMilliSeconds);
            }
            return objValue;
        }

        private string InsertBusiObjById(object busiObj, long numberOfMilliSeconds)
        {
            string cacheKeyByObject = GetCacheKeyByObject(busiObj, this.m_BusiObjIdPropertyName);
            DistCache.Add(cacheKeyByObject, busiObj, numberOfMilliSeconds);
            return cacheKeyByObject;
        }

        public void InsertBusiObjs(IList busiObjs, MethodBase yourQueryMethod, params object[] methodParams)
        {
            this.InsertBusiObjs(busiObjs, DistCache.DefaultExpireTime, yourQueryMethod, methodParams);
        }

        public void InsertBusiObjs(IList busiObjs, long numberOfMilliSeconds, MethodBase yourQueryMethod, params object[] methodParams)
        {
            if ((busiObjs == null) || (busiObjs.Count == 0))
            {
                throw new ApplicationException("错误，对象不能为空");
            }
            ArrayRelation objValue = new ArrayRelation
            {
                Value = new ArrayList()
            };
            string cacheKeyByMethodAndParams = GetCacheKeyByMethodAndParams(yourQueryMethod, methodParams);
            objValue.IListType = busiObjs.GetType();
            for (int i = 0; i < busiObjs.Count; i++)
            {
                string str2 = this.InsertBusiObj(busiObjs[i], numberOfMilliSeconds);
                objValue.Value.Add(str2);
            }
            DistCache.Add(cacheKeyByMethodAndParams, objValue, numberOfMilliSeconds);
        }

        public static void InsertFromQueryMethod(object value, MethodBase yourQueryMethod, params object[] methodParams)
        {
            InsertFromQueryMethod(value, DistCache.DefaultExpireTime, yourQueryMethod, methodParams);
        }

        public static void InsertFromQueryMethod(object value, long numberOfMilliSeconds, MethodBase yourQueryMethod, params object[] methodParams)
        {
            DistCache.Add(GetCacheKeyByMethodAndParams(yourQueryMethod, methodParams), value, numberOfMilliSeconds);
        }

        public static void Remove(string cacheKey)
        {
            DistCache.Remove(cacheKey);
        }

        public void RemoveBusiObjByObjectId(object objectId)
        {
            DistCache.Remove(GetCacheKeyByUniqueIdx(this.m_BusiObjType, this.m_BusiObjIdPropertyName, objectId));
        }

        public static void RemoveFromQueryMethod(MethodBase yourQueryMethod, params object[] methodParams)
        {
            DistCache.Remove(GetCacheKeyByMethodAndParams(yourQueryMethod, methodParams));
        }

        public static void Shutdown()
        {
            DistCache.Shutdown();
        }
    }
}
