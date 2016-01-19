using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Jita.Common.Config
{
    public class CommonPlatformConfiurationSectionHandler : ConfigurationSection
    {
        /// <summary>
        /// Gets the providers.
        /// </summary>
        /// <value>The providers.</value>
        [ConfigurationProperty("CommonConfig")]
        public CommonConfigBase CommonConfig
        {
            get { return (CommonConfigBase)base["CommonConfig"]; }
            set { base["CommonConfig"] = value; }
        }

        /// <summary>
        /// Gets the providers.
        /// </summary>
        /// <value>The providers.</value>
        [ConfigurationProperty("CommonConfigs")]
        public CommonConfigCollection CommonConfigs
        {
            get { return (CommonConfigCollection)base["CommonConfigs"]; }
            set { base["CommonConfigs"] = value; }
        }
    }
}
