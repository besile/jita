using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Jita.Common.Config
{
    public class ValidatecodesConfig : ConfigurationElementCollection
    {
        public ValidatecodesConfig()
        {
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ValidatecodeConfig();
        }


        protected override ConfigurationElement CreateNewElement(
            string elementName)
        {
            return new ValidatecodeConfig(elementName);
        }


        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((ValidatecodeConfig)element).Name;
        }


        public new string AddElementName
        {
            get
            { return base.AddElementName; }

            set
            { base.AddElementName = value; }

        }

        public new string ClearElementName
        {
            get
            { return base.ClearElementName; }

            set
            { base.AddElementName = value; }

        }

        public new string RemoveElementName
        {
            get
            { return base.RemoveElementName; }


        }

        public new int Count
        {

            get { return base.Count; }

        }


        public ValidatecodeConfig this[int index]
        {
            get
            {
                return (ValidatecodeConfig)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public ValidatecodeConfig this[string Name]
        {
            get
            {
                return (ValidatecodeConfig)BaseGet(Name);
            }
        }

        public int IndexOf(ValidatecodeConfig assembly)
        {
            return BaseIndexOf(assembly);
        }

        public void Add(ValidatecodeConfig assembly)
        {
            BaseAdd(assembly);

            // Add custom code here.
        }

        protected override void
            BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
            // Add custom code here.
        }

        public void Remove(ValidatecodeConfig assembly)
        {
            if (BaseIndexOf(assembly) >= 0)
                BaseRemove(assembly.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
            // Add custom code here.
        }

    }
}
