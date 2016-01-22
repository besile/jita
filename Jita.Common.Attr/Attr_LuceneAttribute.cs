using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jita.Common.Attr
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class Attr_LuceneAttribute : Attribute
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
        /// <summary>
        /// 分词
        /// </summary>
        public int SplitWord { get; set; }

        public Attr_LuceneAttribute(string requestKey, object defaultValue,int splitWord)
        {
            this._key = requestKey;
            this._defaultValue = defaultValue;
            this.SplitWord = splitWord;
        }
    }
}
