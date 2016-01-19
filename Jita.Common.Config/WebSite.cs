using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Jita.Common.Config
{
    /// <summary>
    /// 单点登录站点配置处理节点
    /// </summary>
    public class WebSite : ConfigurationElement
    {

        public WebSite()
        {

        }

        public WebSite(string elementName)
        {
            Name = elementName;
        }

        /// <summary>
        /// 单点登录的网站应用，可以是一个应用附录，但url要完整
        /// </summary>
        [ConfigurationProperty("WebSite", IsRequired = true, IsKey = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{};'\"|\\", MinLength = 0, MaxLength = 600)]
        public string Name
        {
            get
            {
                return (string)this["WebSite"];
            }
            set
            {
                this["WebSite"] = value;
            }
        }
    }
}
