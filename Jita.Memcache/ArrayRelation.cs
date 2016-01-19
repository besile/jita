
namespace Jita.Memcache
{
    using System;
    using System.Collections;

    [Serializable]
    internal class ArrayRelation
    {
        private ArrayList _value;
        internal static string idPropertyName = "Key";
        private Type itemType;

        public Type IListType
        {
            get
            {
                return this.itemType;
            }
            set
            {
                this.itemType = value;
            }
        }

        public ArrayList Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }
    }
}
