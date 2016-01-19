
namespace Jita.Memcache
{
    using System;
    using System.Configuration.Provider;
    using System.Reflection;

    public class CacheProviderCollection : ProviderCollection
    {
        public override void Add(ProviderBase provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            if (!(provider is CacheProvider))
            {
                throw new ArgumentException("Invalid provider type", "Jita.Services.Cache");
            }
            base.Add(provider);
        }

        public CacheProvider this[string name]
        {
            get
            {
                return (CacheProvider)base[name];
            }
        }
    }
}
