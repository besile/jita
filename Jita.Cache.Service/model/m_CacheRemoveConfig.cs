using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jita.Cache.Service.model
{
    public class m_CacheRemoveConfig
    {
        public string ID { get; set; }
        public string Pre { get; set; }
        public string AssemblyPath { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public bool GuidCacheHandel { get; set; }
        public string GuidPre { get; set; }
    }
}
