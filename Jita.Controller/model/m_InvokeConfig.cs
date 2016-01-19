using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jita.Controller.model
{
    public sealed class m_InvokeConfig
    {
        public string ID { get; set; }
        public string AssemblyPath { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }

        public bool IsCache2On { get; set; }

        public m_Cache2Config Cache2Config { get; set; }
    }
}
