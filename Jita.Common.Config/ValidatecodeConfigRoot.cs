using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Jita.Common.Config
{
    //验证码配置类
    public class ValidatecodeConfigRoot : ConfigurationElement
    {
        [ConfigurationProperty("Validatecodes")]
        public ValidatecodesConfig ValidateCodes
        {
            get
            { return (ValidatecodesConfig)this["Validatecodes"]; }
            set
            { this["Validatecodes"] = value; }
        }

        [ConfigurationProperty("DefaultValidatecodeName")]
        public string DefaultValidateCodeName
        {
            get
            { return (string)this["DefaultValidatecodeName"]; }
            set
            { this["DefaultValidatecodeName"] = value; }
        }
    }
}
