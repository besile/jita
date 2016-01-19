using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Jita.Common.Config
{
    /// <summary>
    /// 公用平台配置基类
    /// </summary>
    public class CommonConfigBase : ConfigurationElement
    {
        /// <summary>
        /// 缺省构造方法
        /// </summary>
        public CommonConfigBase()
        {
        }

        /// <summary>
        /// 验证码配置
        /// </summary>
        [ConfigurationProperty("ValidateCodeConfig", IsRequired = false, IsKey = false)]
        public ValidatecodeConfigRoot ValidatecodeConfigRoot
        {
            get
            {
                return (ValidatecodeConfigRoot)this["ValidateCodeConfig"];
            }
        }

        /// <summary>
        /// FckEditor配置
        /// </summary>
        [ConfigurationProperty("FckEditor", IsRequired = false, IsKey = false)]
        public FckEditorConfig FckEditor
        {
            get
            {
                return (FckEditorConfig)this["FckEditor"];
            }
        }

        /// <summary>
        /// 缓存配置
        /// </summary>
        [ConfigurationProperty("CacheConfig", IsRequired = false, IsKey = false)]
        public CacheConfig CacheConfig
        {
            get
            {
                return (CacheConfig)this["CacheConfig"];
            }
        }

        /// <summary>
        /// 系统日志配置
        /// </summary>
        [ConfigurationProperty("SysLogConfig", IsRequired = false, IsKey = false)]
        public SysLogConfig SysLogConfig
        {
            get
            {
                return (SysLogConfig)this["SysLogConfig"];
            }
        }

        /// <summary>
        /// SQL注入安全模块配置
        /// </summary>
        [ConfigurationProperty("SQLSecurityConfig", IsRequired = false, IsKey = false)]
        public SQLSecurityConfig SQLSecurityConfig
        {
            get
            {
                return (SQLSecurityConfig)this["SQLSecurityConfig"];
            }
        }

        /// <summary>
        /// 敏感词过滤配置
        /// </summary>
        [ConfigurationProperty("PoliticsConfig", IsRequired = false, IsKey = false)]
        public PoliticsConfig PoliticsConfig
        {
            get
            {
                return (PoliticsConfig)this["PoliticsConfig"];
            }
        }
    }
}
