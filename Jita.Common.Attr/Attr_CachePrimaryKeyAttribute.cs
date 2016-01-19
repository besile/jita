using System;
namespace Jita.Common.Attr
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class Attr_CachePrimaryKeyAttribute : Attribute
    {
        public Attr_CachePrimaryKeyAttribute() { ;}
    }
}
