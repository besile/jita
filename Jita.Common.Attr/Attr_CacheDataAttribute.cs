using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jita.Common.Attr
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class Attr_CacheDataAttribute : Attribute
    {
        private string _whatCase;

        public string WhatCase
        {
            get { return _whatCase; }
            set { _whatCase = value; }
        }

        private Attr_CacheDataAttribute() { ;}

        public Attr_CacheDataAttribute(string whatCase)
            : this()
        {
            this._whatCase = whatCase;
        }
    }
}
