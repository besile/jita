using System;
namespace Jita.Common.Attr
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class Attr_AddLuceneAttribute : Attribute
    {
        /// <summary>
        /// 参数位置
        /// </summary>
        public int PramIndex { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Key { get; set; }

        public Attr_AddLuceneAttribute(int pramIndex, string key)
        {
            this.PramIndex = pramIndex;
            this.Key = key;
        }
    }
}
