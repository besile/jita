using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Jita.Common.Config
{
    public class CacheConfig : ConfigurationElement
    {
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection)base["providers"]; }
        }

        [StringValidator(MinLength = 1)]
        [ConfigurationProperty("defaultProvider", DefaultValue = "MemcachedCacheProvider")]
        public string DefaultProvider
        {
            get { return (string)base["defaultProvider"]; }
            set { base["defaultProvider"] = value; }
        }

        [StringValidator(MinLength = 1)]
        [ConfigurationProperty("backupProvider", DefaultValue = "MemcachedCacheProvider")]
        public string BackupProvider
        {
            get { return (string)base["backupProvider"]; }
            set { base["backupProvider"] = value; }
        }
    }
}
