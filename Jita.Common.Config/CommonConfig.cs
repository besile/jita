using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Jita.Common.Config
{
    /// <summary>
    /// 公用平台配置类
    /// </summary>
    public class CommonConfig : CommonConfigBase
    {
        /// <summary>
        /// 缺省构造方法
        /// </summary>
        public CommonConfig()
        {
        }

        /// <summary>
        /// 根据key初始化
        /// </summary>
        /// <param name="key">配置名</param>
        public CommonConfig(string key)
        {
            this.Key = key;
        }

        /// <summary>
        /// 配置名
        /// </summary>
        [ConfigurationProperty("name", IsRequired = false, IsKey = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{};'\"|\\", MinLength = 0, MaxLength = 600)]
        public string Key
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }
    }
}
