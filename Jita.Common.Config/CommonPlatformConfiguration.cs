using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Jita.Common.Config
{
    /// <summary>
    /// 公用平台配置入口类
    /// </summary>
    public class CommonPlatformConfiguration
    {
        /// <summary>
        /// 公用平台配置实例
        /// </summary>
        private static CommonPlatformConfiurationSectionHandler instance;

        /// <summary>
        /// 配置属性
        /// </summary>
        public static CommonPlatformConfiurationSectionHandler Instance
        {
            get
            {
                // Uses "Lazy initialization"
                if (instance == null)
                {
                    instance = (CommonPlatformConfiurationSectionHandler)ConfigurationManager.GetSection("Jita.Utils.CommonPlatformConfig");
                }
                return instance;
            }
        }

        /// <summary>
        /// 获得验证码配置
        /// </summary>
        /// <returns>验证码的配置对象</returns>
        public static ValidatecodeConfigRoot GetValidatecodeConfigRoot()
        {
            return Instance.CommonConfig.ValidatecodeConfigRoot;
        }

        /// <summary>
        /// 获得验证码配置
        /// </summary>
        /// <param name="configName"></param>
        /// <returns>验证码的配置对象</returns>
        public static ValidatecodeConfigRoot GetValidatecodeConfigRoot(string configName)
        {
            if (string.IsNullOrEmpty(configName))
            {
                return GetValidatecodeConfigRoot();
            }
            return (Instance.CommonConfigs[configName] == null || Instance.CommonConfigs[configName].ValidatecodeConfigRoot == null) ?
                   GetValidatecodeConfigRoot() :
                   Instance.CommonConfigs[configName].ValidatecodeConfigRoot;
        }

       
       
        /// <summary>
        /// 读取FckEditor配置
        /// </summary>
        /// <returns>FckEditor配置对象</returns>
        public static FckEditorConfig GetFckEditorConfig()
        {
            return Instance.CommonConfig.FckEditor;
        }

        /// <summary>
        /// 读取FckEditor配置
        /// </summary>
        /// <param name="configName">配置名</param>
        /// <returns>FckEditor配置对象</returns>
        public static FckEditorConfig GetFckEditorConfig(string configName)
        {
            if (string.IsNullOrEmpty(configName))
            {
                return GetFckEditorConfig();
            }
            return (Instance.CommonConfigs[configName] == null || Instance.CommonConfigs[configName].FckEditor == null) ?
                   GetFckEditorConfig() :
                   Instance.CommonConfigs[configName].FckEditor;
        }

        /// <summary>
        /// 获取缓存配置
        /// </summary>
        /// <returns>缓存配置对象</returns>
        public static CacheConfig GetCacheConfig()
        {
            return Instance.CommonConfig.CacheConfig;
        }

        /// <summary>
        /// 获取缓存配置
        /// </summary>
        /// <param name="configName">配置名</param>
        /// <returns>缓存配置对象</returns>
        public static CacheConfig GetCacheConfig(string configName)
        {
            if (string.IsNullOrEmpty(configName))
            {
                return GetCacheConfig();
            }
            return (Instance.CommonConfigs[configName] == null || Instance.CommonConfigs[configName].CacheConfig == null) ?
                   GetCacheConfig() :
                   Instance.CommonConfigs[configName].CacheConfig;
        }

        /// <summary>
        /// 获取系统日志配置
        /// </summary>
        /// <returns>系统日志配置对象</returns>
        public static SysLogConfig GetSysLogConfig()
        {
            return Instance.CommonConfig.SysLogConfig;
        }

        /// <summary>
        /// 获取系统日志配置
        /// </summary>
        /// <param name="configName">配置名</param>
        /// <returns>系统日志配置对象</returns>
        public static SysLogConfig GetSysLogConfig(string configName)
        {
            if (string.IsNullOrEmpty(configName))
            {
                return GetSysLogConfig();
            }
            return (Instance.CommonConfigs[configName] == null || Instance.CommonConfigs[configName].SysLogConfig == null) ?
                   GetSysLogConfig() :
                   Instance.CommonConfigs[configName].SysLogConfig;
        }

        /// <summary>
        /// 读取SQLSecurityConfig配置
        /// </summary>
        /// <returns>安全配置对象</returns>
        public static SQLSecurityConfig GetSQLSecurityConfig()
        {
            return Instance.CommonConfig.SQLSecurityConfig;
        }

        /// <summary>
        /// 读取SQLSecurityConfig配置
        /// </summary>
        /// <param name="configName">配置名</param>
        /// <returns>安全配置对象</returns>
        public static SQLSecurityConfig GetSQLSecurityConfig(string configName)
        {
            if (string.IsNullOrEmpty(configName))
            {
                return GetSQLSecurityConfig();
            }
            return (Instance.CommonConfigs[configName] == null || Instance.CommonConfigs[configName].SQLSecurityConfig == null) ?
                   GetSQLSecurityConfig() :
                   Instance.CommonConfigs[configName].SQLSecurityConfig;
        }

        /// <summary>
        /// 读取敏感词过滤配置
        /// </summary>
        /// <returns>敏感词配置类</returns>
        public static PoliticsConfig GetPoliticsConfig()
        {
            return Instance.CommonConfig.PoliticsConfig;
        }

        /// <summary>
        /// 读取敏感词过滤配置
        /// </summary>
        /// <param name="configName">配置名</param>
        /// <returns>敏感词配置类</returns>
        public static PoliticsConfig GetPoliticsConfig(string configName)
        {
            if (string.IsNullOrEmpty(configName))
            {
                return GetPoliticsConfig();
            }
            return (Instance.CommonConfigs[configName] == null || Instance.CommonConfigs[configName].PoliticsConfig == null) ?
                   GetPoliticsConfig() :
                   Instance.CommonConfigs[configName].PoliticsConfig;
        }
    }
}
