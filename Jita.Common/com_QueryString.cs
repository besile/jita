using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Web;

namespace Jita.Common
{
    public class com_QueryString : NameValueCollection
    {
        public com_QueryString()
        {

        }

        public com_QueryString(NameValueCollection query)
        {
            this.Add(query);
        }

        public com_QueryString(string query)
        {
            var nameValues = HttpUtility.ParseQueryString(query);
            this.Add(nameValues);
        }

        public static com_QueryString Current
        {
            get
            {
                return new com_QueryString(HttpContext.Current.Request.Url.Query);
            }
        }

        public com_QueryString Parse(string query)
        {
            base.Clear();
            var nameValues = HttpUtility.ParseQueryString(query);
            this.Add(nameValues);

            return this;
        }

        public com_QueryString ParseCurrent()
        {
            if (HttpContext.Current != null)
            {
                return Parse(HttpContext.Current.Request.Url.Query);
            }
            base.Clear();
            return this;
        }

        public new com_QueryString Add(string name, string value)
        {
            return Add(name, value, true);
        }

        public com_QueryString Add(string name, string value, bool isUnique)
        {
            string existingValue = base[name];
            if (string.IsNullOrEmpty(existingValue))
                base.Add(name, HttpUtility.UrlEncodeUnicode(value));
            else if (isUnique)
                base[name] = HttpUtility.UrlEncodeUnicode(value);
            else
                base[name] += "," + HttpUtility.UrlEncodeUnicode(value);
            return this;
        }

        public new com_QueryString Set(string name, string value)
        {
            Set(name, value, true);
            return this;
        }

        public com_QueryString Set(string name, string value, bool encoded)
        {
            if (value == null)
            {
                base.Remove(name);
                return this;
            }

            string existingValue = base[name];
            if (string.IsNullOrEmpty(existingValue))
                base.Add(name, encoded ? HttpUtility.UrlEncodeUnicode(value) : value);
            else
                base[name] = encoded ? HttpUtility.UrlEncodeUnicode(value) : value;
            return this;
        }

        public new com_QueryString Remove(string name)
        {
            string existingValue = base[name];
            if (!string.IsNullOrEmpty(existingValue))
                base.Remove(name);
            return this;
        }

        public new string this[int index]
        {
            get
            {
                return HttpUtility.UrlDecode(base[index]);
            }
        }

        public bool Contains(string name)
        {
            string existingValue = base[name];
            return !string.IsNullOrEmpty(existingValue);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (var i = 0; i < base.Keys.Count; i++)
            {
                if (!string.IsNullOrEmpty(base.Keys[i]))
                {
                    foreach (string val in base[base.Keys[i]].Split(','))
                        builder.Append((builder.Length == 0) ? "?" : "&").Append(HttpUtility.UrlEncodeUnicode(base.Keys[i])).Append("=").Append(val);
                }
            }
            return builder.ToString();
        }
    }
}
