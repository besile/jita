using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jita.Common.Attr
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class Attr_WriteLogAttribute : Attribute
    {
        public string WhatCase { get; set; }


        public Attr_WriteLogAttribute(string whatCase)
        {
            this.WhatCase = whatCase;

        }
    }
}
