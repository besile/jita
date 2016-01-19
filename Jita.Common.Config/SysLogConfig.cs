using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Jita.Common.Config
{
    public class SysLogConfig : ConfigurationElement
    {
        //businesscode通用配置类
        /// <summary>
        /// 
        /// </summary>
        public SysLogConfig()
        {

        }

        [ConfigurationProperty("implClassName", IsRequired = false, IsKey = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{};'\"|\\", MinLength = 0, MaxLength = 600)]
        public string ImplClassName
        {
            get
            {
                return (string)this["implClassName"];
            }
            set
            {
                this["implClassName"] = value;
            }
        }

        [ConfigurationProperty("defaultNetTiersProviderName", IsRequired = false, IsKey = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{};'\"|\\", MinLength = 0, MaxLength = 600)]
        public string DefaultNetTiersProviderName
        {
            get
            {
                return (string)this["defaultNetTiersProviderName"];
            }
            set
            {
                this["defaultNetTiersProviderName"] = value;
            }
        }

        [ConfigurationProperty("remotingUrl", IsRequired = true, IsKey = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{};'\"|\\", MinLength = 0, MaxLength = 600)]
        public string RemotingUrl
        {
            get
            {
                return (string)this["remotingUrl"];
            }
            set
            {
                this["remotingUrl"] = value;
            }
        }

        /// <summary>
        /// 本地Log4net记录Info、Debug信息
        /// </summary>
        [ConfigurationProperty("isUseLog4net", DefaultValue = "false", IsRequired = false)]
        public bool IsUseLog4net
        {
            get
            { return (bool)this["isUseLog4net"]; }
            set
            { this["isUseLog4net"] = value; }
        }

        /// <summary>
        /// 当isUseLog4net为false时，本属性指定日志要记录到的项目名，如果为null，则根据Url判断项目名
        /// </summary>
        [ConfigurationProperty("DefaultLogProjectName", DefaultValue = "", IsRequired = false)]
        public string DefaultLogProjectName
        {
            get
            {
                return string.IsNullOrEmpty((string)this["DefaultLogProjectName"]) ? null : (string)this["DefaultLogProjectName"];
            }
            set
            {
                this["DefaultLogProjectName"] = value;
            }
        }

        /// <summary>
        /// 当isUseLog4net为true时，本属性指定log4net配置中用到的配置名
        /// </summary>
        [ConfigurationProperty("logConfigNameInLog4net", DefaultValue = "BitAutoSysLog", IsRequired = false)]
        public string LogConfigNameInLog4net
        {
            get
            { return (string)this["logConfigNameInLog4net"]; }
            set
            { this["logConfigNameInLog4net"] = value; }
        }

        /// <summary>
        /// 控制记录的日志级别，小于该级别的日志将不被记录。
        /// 有效的值如下，级别依次降低：Fatal、Error、Warn、Info、Debug。
        /// </summary>
        [ConfigurationProperty("logLevel", DefaultValue = "Error", IsRequired = false)]
        public SysLogLevelEnum LogLevel
        {
            get
            {
                return (SysLogLevelEnum)this["logLevel"];
            }
            set
            {
                this["logLevel"] = value;
            }
        }

        /// <summary>
        /// 当用于Web应用中时，是否记录发生异常时的ClientMessage、ServerMessage、SessionMessage，
        /// 由于这些信息比较大，正常情况下应该设为false
        /// </summary>
        [ConfigurationProperty("isLogWebContext", DefaultValue = "false", IsRequired = false)]
        public bool IsLogWebContext
        {
            get
            { return (bool)this["isLogWebContext"]; }
            set
            { this["isLogWebContext"] = value; }
        }

    }
}
