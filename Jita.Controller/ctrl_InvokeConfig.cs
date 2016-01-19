using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Jita.Common;
using Jita.Config;
using Jita.Controller.model;

namespace Jita.Controller
{
    internal sealed class ctrl_InvokeConfig
    {
        public static m_InvokeConfig UpdateConfig(string key)
        {
            string cacheKey = "m_InvokeConfig_" + key;
            m_InvokeConfig invokeConfig = com_GlobalDic.Pop(cacheKey) as m_InvokeConfig;
            if (invokeConfig == null)
            {
                string _cacheConfigFilePath = AppConfig.GetConfigFilePath(
                    AppConfig.GetApp("UpdateCaseFilePath"));
                XElement xmlConfig = com_XmlLoad.LoadXmlConfig(_cacheConfigFilePath);
                var config = from c in xmlConfig.Descendants("KeyItem")
                             where c.Attribute("ID").Value == key
                             select new m_InvokeConfig
                             {
                                 ID = key,
                                 AssemblyPath = c.Element("AssemblyPath") == null ? string.Empty : c.Element("AssemblyPath").Value,
                                 ClassName = c.Element("ClassName") == null ? string.Empty : c.Element("ClassName").Value,
                                 MethodName = c.Element("MethodName") == null ? string.Empty : c.Element("MethodName").Value,
                             };
                invokeConfig = config.First();
                com_GlobalDic.Push(cacheKey, invokeConfig);
            }
            return invokeConfig;
        }

        public static m_InvokeConfig GetConfig(string key)
        {
            string cacheKey = "m_InvokeConfig_" + key;
            m_InvokeConfig invokeConfig = com_GlobalDic.Pop(cacheKey) as m_InvokeConfig;
            if (invokeConfig == null)
            {
                string _cacheConfigFilePath = AppConfig.GetConfigFilePath(
                    AppConfig.GetApp("GetCaseFilePath"));
                XElement xmlConfig = com_XmlLoad.LoadXmlConfig(_cacheConfigFilePath);
                var config = from c in xmlConfig.Descendants("KeyItem")
                             where c.Attribute("ID").Value == key
                             select new m_InvokeConfig
                             {
                                 ID = key,
                                 AssemblyPath = c.Element("AssemblyPath") == null ? string.Empty : c.Element("AssemblyPath").Value,
                                 ClassName = c.Element("ClassName") == null ? string.Empty : c.Element("ClassName").Value,
                                 MethodName = c.Element("MethodName") == null ? string.Empty : c.Element("MethodName").Value,
                             };
                invokeConfig = config.First();
                com_GlobalDic.Push(cacheKey, invokeConfig);
            }
            return invokeConfig;
        }
        /// <summary>
        /// 获取添加操作的程序集信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static m_InvokeConfig AddConfig(string key)
        {
            return InvokeConfig("AddCaseFilePath", key);
        }
        /// <summary>
        /// 获取程序集信息
        /// </summary>
        /// <param name="appsettingPath"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static m_InvokeConfig InvokeConfig(string appsettingPath, string key)
        {
            string _cacheConfigFilePath = AppConfig.GetConfigFilePath(
                AppConfig.GetApp(appsettingPath));
            XElement xmlConfig = com_XmlLoad.LoadXmlConfig(_cacheConfigFilePath);
            var config = from c in xmlConfig.Descendants("KeyItem")
                         where c.Attribute("ID").Value == key
                         select new m_InvokeConfig
                         {
                             ID = key,
                             AssemblyPath = c.Element("AssemblyPath") == null ? string.Empty : c.Element("AssemblyPath").Value,
                             ClassName = c.Element("ClassName") == null ? string.Empty : c.Element("ClassName").Value,
                             MethodName = c.Element("MethodName") == null ? string.Empty : c.Element("MethodName").Value,
                         };
            return config.First();
        }
    }
}
