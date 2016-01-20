using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace Jita.Config
{
    public sealed class AppConfig
    {
        private AppConfig() { ;}

        public static string GetApp(string appKey)
        {
            return ConfigurationManager.AppSettings[appKey].ToString();
        }

        public static string GetConfigFilePath(string filePath)
        {
            if (filePath == null || filePath.Length == 0) return null;
            return  string.Concat(AppDomain.CurrentDomain.BaseDirectory ,filePath);
            //return HttpContext.Current.Server.MapPath(filePath);
        }

        public static bool OpenCache
        {
            get
            {
                return GetApp("IsCache") == "1";
            }
        }

        public static bool DevelopMode
        {
            get
            {
                return GetApp("DevelopMode") == "1";
            }
        }
    }
}
