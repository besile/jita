using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jita.Common.Attr
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class Attr_RemoveCacheAttribute : Attribute
    {
        public string WhatCase { get; set; }
        public int ArgsIndex { get; set; }
        public string Key { get; set; }

        public Attr_RemoveCacheAttribute(string whatCase, int argsIndex, string key = null)
        {
            this.WhatCase = whatCase;
            this.ArgsIndex = argsIndex;
            this.Key = key;
        }
    }
}
