using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Jita.Common.Config
{
    public class PoliticsConfig : ConfigurationElement
    {
        [ConfigurationProperty("ConfigFile", DefaultValue = "")]
        public string ConfigFile
        {
            get
            {
                return (string)this["ConfigFile"];
            }
            set
            {
                this["ConfigFile"] = value;
            }
        }
    }
}
