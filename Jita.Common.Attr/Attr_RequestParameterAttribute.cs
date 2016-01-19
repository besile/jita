using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jita.Common.Attr
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class Attr_RequestParameterAttribute : Attribute
    {
        private string _key;

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }
        private object _defaultValue;

        public object DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        public Attr_RequestParameterAttribute(string requestKey, object defaultValue)
        {
            this._key = requestKey;
            this._defaultValue = defaultValue;
        }
    }
}
