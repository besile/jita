using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jita.Common.Attr
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class Attr_MatchFieldAttribute : Attribute
    {
        /// <summary>
        /// The _key
        /// </summary>
        private string _key;

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Attr_MatchFieldAttribute"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public Attr_MatchFieldAttribute(string key)
        {
            this._key = key;
        }
    }
}
