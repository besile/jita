using System.Reflection;
using Jita.Common.Attr;
namespace KouBei.Cache.Service
{
    internal static class srv_CacheKey
    {
        public static string ToPrimaryKey(this object self)
        {
            PropertyInfo[] pis = self.GetType().GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                object[] pk = pi.GetCustomAttributes(typeof(Attr_CachePrimaryKeyAttribute), false);
                if (pk == null || pk.Length == 0) continue;

                foreach (Attr_CachePrimaryKeyAttribute item in pk)
                {
                    if (item != null)
                    {
                        var key = (pi.GetValue(self, null));
                        if (key == null) return string.Empty;
                        return key.ToString().ToUpper();
                    }
                }
            }

            return self.ToString();
        }
    }
}
