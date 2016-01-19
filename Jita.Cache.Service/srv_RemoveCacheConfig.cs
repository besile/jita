using System.Linq;
using System.Xml.Linq;
using Jita.Cache.Service.model;
using Jita.Common;
using Jita.Config;

namespace Jita.Cache.Service
{
    internal sealed class srv_RemoveCacheConfig
    {
        public static m_CacheRemoveConfig GetConfig(string key)
        {
            string _cacheConfigFilePath = AppConfig.GetConfigFilePath(
                AppConfig.GetApp("RemoveCacheCaseFilePath"));
            XElement xmlConfig = com_XmlLoad.LoadXmlConfig(_cacheConfigFilePath);
            var config = from c in xmlConfig.Descendants("KeyItem")
                         where c.Attribute("ID").Value == key
                         select new m_CacheRemoveConfig
                         {
                             ID = key,
                             Pre = c.Element("Pre") == null ? string.Empty : c.Element("Pre").Value,
                             AssemblyPath = c.Element("AssemblyPath") == null ? string.Empty : c.Element("AssemblyPath").Value,
                             ClassName = c.Element("ClassName") == null ? string.Empty : c.Element("ClassName").Value,
                             MethodName = c.Element("MethodName") == null ? string.Empty : c.Element("MethodName").Value,
                             GuidCacheHandel = c.Element("GuidCacheHandel") == null ? false : bool.Parse(c.Element("GuidCacheHandel").Value),
                             GuidPre = c.Element("GuidPre") == null ? string.Empty : c.Element("GuidPre").Value,
                         };
            return config.First();
        }
    }
}
