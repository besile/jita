using System.Configuration;
namespace Jita.Common.Config
{
    public sealed class DBConfig
    {
        private DBConfig() { ;}

        public static string GetDb(string dbKey)
        {
            return ConfigurationManager.ConnectionStrings[dbKey].ConnectionString;
        }
    }
}
