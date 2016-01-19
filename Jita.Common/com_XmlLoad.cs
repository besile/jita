using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Jita.Common
{
    /// <summary>
    /// Class XmlRead
    /// 配置文件加载单例
    /// </summary>
    public sealed class com_XmlLoad
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="com_XmlLoad" /> class from being created.
        /// </summary>
        private com_XmlLoad() { ;}

        /// <summary>
        /// The _instance
        /// </summary>
        private static XElement _instance = null;

        /// <summary>
        /// The sync
        /// </summary>
        private static readonly object sync = new object();

        /// <summary>
        /// The kv
        /// </summary>
        private static Hashtable kv = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Gets the config.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>XElement.</returns>
        public static XElement LoadXmlConfig(string filePath)
        {
            //lock (sync)
            //{
            //    _instance = kv.ContainsKey(filePath) ? kv[filePath] as XElement : null;
            //}
            //if (_instance == null)
            //{
            //    lock (sync)
            //    {
            //        if (_instance == null)
            //        {
            //            _instance = XElement.Load(filePath);
            //            kv.Add(filePath, _instance);
            //        }
            //    }
            //}
            //return _instance;

            _instance = kv.ContainsKey(filePath) ? kv[filePath] as XElement : null;
            if (_instance == null)
            {
                _instance = XElement.Load(filePath);
                lock (sync)
                {
                    kv.Add(filePath, _instance);
                }
            }
            return _instance;
        }
    }
}
