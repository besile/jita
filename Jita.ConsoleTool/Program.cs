using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Jita.Cache.Service.model;
using Jita.Common;
using Jita.Config;
using Jita.Controller;
using Jita.Controller.model;
using Jita.Data.Model;
using Jita.LuceneManger;

namespace Jita.ConsoleTool
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 获取帖子列表测试 通过
            //LoadGetCaseXml();
            //LoadCacheCaseXml();
            //var list = ctrl_ServiceClient.GetService<T_Humor_HumorInfo>(AppConfig.OpenCache, "通过逻辑层获取帖子列表", null);
            #endregion

            #region 帖子搜索测试 通过
            //int total;
            //var list = LuceneIndex.GetList<T_Humor_HumorInfo>("屌爆了", 1, 10, new string[] { "title", "content" }, out total);
            #endregion

            #region 测试添加索引 通过
            //foreach (var humor in list)
            //{
            //    LuceneIndex.InsertToIndex<T_Humor_HumorInfo>(humor);
            //}
            #endregion
            
        }


        private static void LoadCacheCaseXml()
        {
            string _cacheConfigFilePath = AppConfig.GetConfigFilePath(
                    AppConfig.GetApp("CacheCaseFilePath"));
            XElement xmlConfig = com_XmlLoad.LoadXmlConfig(_cacheConfigFilePath);
            var config = from c in xmlConfig.Descendants("KeyItem")
                         //where c.Attribute("ID").Value == key
                         select new m_CacheConfig
                         {
                             ID = c.Attribute("ID") == null ? string.Empty : c.Attribute("ID").Value,
                             AssemblyPath = c.Element("AssemblyPath") == null ? string.Empty : c.Element("AssemblyPath").Value,
                             ClassName = c.Element("ClassName") == null ? string.Empty : c.Element("ClassName").Value,
                             MethodName = c.Element("MethodName") == null ? string.Empty : c.Element("MethodName").Value,
                             ExpTime = c.Element("ExpTime") == null ? 0 : int.Parse(c.Element("ExpTime").Value),
                             Pre = c.Element("Pre") == null ? string.Empty : c.Element("Pre").Value
                         };

            foreach (var item in config)
            {

                Type type = Type.GetType(Assembly.CreateQualifiedName(item.AssemblyPath, item.ClassName));
                MethodInfo mi = type.GetMethod(item.MethodName);
                var instance = mi.IsStatic ? null : Activator.CreateInstance(type);

                com_GlobalDic.Push("m_CacheConfig_" + item.ID, item);
                com_GlobalDic.Push(item.ID, new ArrayList() { instance, mi });
            }
        }

        private static void LoadGetCaseXml()
        {
            string _cacheConfigFilePath = AppConfig.GetConfigFilePath(
                    AppConfig.GetApp("GetCaseFilePath"));
            XElement xmlConfig = com_XmlLoad.LoadXmlConfig(_cacheConfigFilePath);
            var config = from c in xmlConfig.Descendants("KeyItem")
                         //where c.Attribute("ID").Value == key
                         select new m_InvokeConfig
                         {
                             ID = c.Attribute("ID") == null ? string.Empty : c.Attribute("ID").Value,
                             AssemblyPath = c.Element("AssemblyPath") == null ? string.Empty : c.Element("AssemblyPath").Value,
                             ClassName = c.Element("ClassName") == null ? string.Empty : c.Element("ClassName").Value,
                             MethodName = c.Element("MethodName") == null ? string.Empty : c.Element("MethodName").Value,
                         };
            foreach (var item in config)
            {

                Type type = Type.GetType(Assembly.CreateQualifiedName(item.AssemblyPath, item.ClassName));
                MethodInfo mi = type.GetMethod(item.MethodName);
                var instance = mi.IsStatic ? null : Activator.CreateInstance(type);

                com_GlobalDic.Push("m_InvokeConfig_" + item.ID, item);
                com_GlobalDic.Push(item.ID, new ArrayList() { instance, mi });
            }
        }
    }
}
