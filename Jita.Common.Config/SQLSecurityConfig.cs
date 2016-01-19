using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Jita.Common.Config
{
    /// <summary>
    /// SQL注入安全类配置
    /// </summary>
    public class SQLSecurityConfig : ConfigurationElement
    {
        /// <summary>
        /// 是否使用安全模块执行过滤
        /// </summary>
        [ConfigurationProperty("Enabled", DefaultValue = "False", IsRequired = true)]
        public bool Enabled
        {
            get
            {
                return (bool)this["Enabled"];
            }
            set
            {
                this["Enabled"] = value;
            }
        }

        /// <summary>
        /// 敏感词列表,用|分开。此类敏感词用分词方式过滤。
        /// </summary>
        [ConfigurationProperty("Keywords", DefaultValue = "'|;|(|)|select|insert|update|delete|drop|truncate|declare|xp_cmdshell|/add|net user|exec|execute|cast|xp_|sp_|0x", IsRequired = true)]
        public string Keywords
        {
            get
            {
                return (string)this["Keywords"];
            }
            set
            {
                this["Keywords"] = value;
            }
        }

        /// <summary>
        /// 敏感词列表,用|分开。此类敏感词直接过滤。
        /// </summary>
        [ConfigurationProperty("Keychars", DefaultValue = "'|;|(|)|0x")]
        public string Keychars
        {
            get
            {
                return (string)this["Keychars"];
            }
            set
            {
                this["Keychars"] = value;
            }
        }
        [ConfigurationProperty("IgnorePostInputs", DefaultValue = "")]
        public string IgnorePostInputs
        {
            get
            {
                return (string)this["IgnorePostInputs"];
            }
            set
            {
                this["IgnorePostInputs"] = value;
            }

        }
        /// <summary>
        /// 需要排除的url规则
        /// </summary>
        [ConfigurationProperty("SpecialPages", IsRequired = true)]
        public SpecialPages SpecialPages
        {
            get
            {
                return (SpecialPages)this["SpecialPages"];
            }

            set
            {
                this["SpecialPages"] = value;
            }
        }
    }

    /// <summary>
    /// 页面信息
    /// </summary>
    public class SpecialPage : ConfigurationElement
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SpecialPage()
        {
        }
        public SpecialPage(string pageUrl)
        {
            PageUrl = pageUrl;
        }

        /// <summary>
        /// 名称
        /// </summary>
        [ConfigurationProperty("PageUrl", IsRequired = true, IsKey = true)]
        [StringValidator(MinLength = 0, MaxLength = 600)]
        public string PageUrl
        {
            get
            {
                return (string)this["PageUrl"];
            }
            set
            {
                this["PageUrl"] = value;
            }
        }

        /// <summary>
        /// 该url是否检查
        /// </summary>
        [ConfigurationProperty("Enabled", IsRequired = true, DefaultValue = true)]
        public bool Enabled
        {
            get
            {
                return (bool)this["Enabled"];
            }
            set
            {
                this["Enabled"] = value;
            }
        }

        /// <summary>
        /// 敏感词列表，用|隔开
        /// </summary>
        [ConfigurationProperty("Keywords", IsRequired = false, IsKey = false)]
        [StringValidator(MinLength = 0, MaxLength = 600)]
        public string Keywords
        {
            get
            {
                return (string)this["Keywords"];
            }
            set
            {
                this["Keywords"] = value;
            }
        }

        /// <summary>
        /// 敏感词列表,用|分开
        /// </summary>
        [ConfigurationProperty("Keychars", IsRequired = false, IsKey = false)]
        public string Keychars
        {
            get
            {
                return (string)this["Keychars"];
            }
            set
            {
                this["Keychars"] = value;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SpecialPages : ConfigurationElementCollection
    {
        public SpecialPages()
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
            return new SpecialPage();
        }


        protected override ConfigurationElement CreateNewElement(
            string elementName)
        {
            return new SpecialPage(elementName);
        }


        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((SpecialPage)element).PageUrl;
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


        public SpecialPage this[int index]
        {
            get
            {
                return (SpecialPage)BaseGet(index);
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

        new public SpecialPage this[string Name]
        {
            get
            {
                return (SpecialPage)BaseGet(Name);
            }
        }

        public int IndexOf(SpecialPage assembly)
        {
            return BaseIndexOf(assembly);
        }

        public void Add(SpecialPage assembly)
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

        public void Remove(SpecialPage assembly)
        {
            if (BaseIndexOf(assembly) >= 0)
                BaseRemove(assembly.PageUrl);
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
