using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jita.Controller.model
{
    public sealed class m_Cache2Config
    {
        public m_Cache2Config() { ;}

        public string CacheKey { get; set; }
        public int CacheTime { get; set; }
    }
}
