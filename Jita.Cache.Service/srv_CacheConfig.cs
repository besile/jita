using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Jita.Cache.Service.model;
using Jita.Common;
using Jita.Config;

namespace Jita.Cache.Service
{
    internal sealed class srv_CacheConfig
    {
        public static m_CacheConfig GetConfig(string key)
        {
            string cacheKey = "m_CacheConfig_" + key;

            m_CacheConfig cacheConfig = com_GlobalDic.Pop(key) as m_CacheConfig;
            if (cacheConfig == null)
            {
                string _cacheConfigFilePath = AppConfig.GetConfigFilePath(
                    AppConfig.GetApp("CacheCaseFilePath"));
                XElement xmlConfig = com_XmlLoad.LoadXmlConfig(_cacheConfigFilePath);
                var config = from c in xmlConfig.Descendants("KeyItem")
                             where c.Attribute("ID").Value == key
                             select new m_CacheConfig
                             {
                                 ID = key,
                                 AssemblyPath = c.Element("AssemblyPath") == null ? string.Empty : c.Element("AssemblyPath").Value,
                                 ClassName = c.Element("ClassName") == null ? string.Empty : c.Element("ClassName").Value,
                                 MethodName = c.Element("MethodName") == null ? string.Empty : c.Element("MethodName").Value,
                                 ExpTime = c.Element("ExpTime") == null ? 0 : int.Parse(c.Element("ExpTime").Value),
                                 Pre = c.Element("Pre") == null ? string.Empty : c.Element("Pre").Value
                             };
                cacheConfig = config.First();
                com_GlobalDic.Push(cacheKey, cacheConfig);
            }
            return cacheConfig;
        }
    }
}
