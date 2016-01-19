using System;
using System.Configuration;
namespace Jita.Common.Config
{
    /// <summary>
    /// FckEditor配置类
    /// </summary>
    public class FckEditorConfig : ConfigurationElement
    {
        /// <summary>
        /// 上传文件用绝对路径的配置
        /// </summary>
        [ConfigurationProperty("AbsoluteFileUrlConfig")]
        public AbsoluteFileUrlConfig AbsoluteFileUrlConfig
        {
            get
            {
                return (AbsoluteFileUrlConfig)this["AbsoluteFileUrlConfig"];
            }
            set
            {
                this["AbsoluteFileUrlConfig"] = value;
            }
        }

        /// <summary>
        /// 上传文件的目录和文件名配置
        /// </summary>
        [ConfigurationProperty("UploadConfig")]
        public UploadConfig UploadConfig
        {
            get
            {
                return (UploadConfig)this["UploadConfig"];
            }
            set
            {
                this["UploadConfig"] = value;
            }
        }

        /// <summary>
        /// 编辑器根目录
        /// </summary>
        [ConfigurationProperty("BaseFilePath", DefaultValue = "/fckeditor/", IsRequired = true)]
        public string BaseFilePath
        {
            get
            {
                return (string)this["BaseFilePath"];
            }
            set
            {
                this["BaseFilePath"] = value;
            }
        }

        /// <summary>
        /// 上传文件路径根目录
        /// </summary>
        [ConfigurationProperty("UserFilesPath", DefaultValue = "/UploadFile/", IsRequired = true)]
        public string UserFilesPath
        {
            get
            {
                return (string)this["UserFilesPath"];
            }
            set
            {
                this["UserFilesPath"] = value;
            }
        }

        /// <summary>
        /// FckEditor 配置信息类：记录上传信息的类
        /// </summary>
        [ConfigurationProperty("RecorderConfig")]
        public RecorderConfig RecorderConfig
        {
            get
            {
                return (RecorderConfig)this["RecorderConfig"];
            }
            set
            {
                this["RecorderConfig"] = value;
            }
        }

    }
    /// <summary>
    /// FckEditor 文件Host类
    /// </summary>
    public class Host : ConfigurationElement
    {
        public Host()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>       
        public Host(string elementName)
        {
            Name = elementName;
        }

        [ConfigurationProperty("Host", IsRequired = true, IsKey = true)]
        [StringValidator(MinLength = 0, MaxLength = 600)]
        public string Name
        {
            get
            {
                return (string)this["Host"];
            }
            set
            {
                this["Host"] = value;
            }
        }

    }
    /// <summary>
    /// FckEditor 上传的文件用的多个随机Hosts类
    /// </summary>
    public class Hosts : ConfigurationElementCollection
    {
        public Hosts()
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
            return new Host();
        }


        protected override ConfigurationElement CreateNewElement(
            string elementName)
        {
            return new Host(elementName);
        }


        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((Host)element).Name;
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


        public Host this[int index]
        {
            get
            {
                return (Host)BaseGet(index);
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

        new public Host this[string Name]
        {
            get
            {
                return (Host)BaseGet(Name);
            }
        }

        public int IndexOf(Host assembly)
        {
            return BaseIndexOf(assembly);
        }

        public void Add(Host assembly)
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

        public void Remove(Host assembly)
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
    /// <summary>
    /// FckEditor 配置信息类：上传文件用绝对路径的信息类
    /// </summary>
    public class AbsoluteFileUrlConfig : ConfigurationElement
    {
        /// <summary>
        /// 是否用绝对路径
        /// </summary>
        [ConfigurationProperty("Enabled", IsRequired = true)]
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
        /// 默认的host
        /// </summary>
        [ConfigurationProperty("DefaultHostName", IsRequired = true)]
        public string DefaultHostName
        {
            get
            {
                return (string)this["DefaultHostName"];
            }
            set
            {
                this["DefaultHostName"] = value;
            }
        }

        /// <summary>
        /// 绝对路径是否用多个随机host
        /// </summary>
        [ConfigurationProperty("AllowMultiHostName", IsRequired = true)]
        public bool AllowMultiHostName
        {
            get
            {
                return (bool)this["AllowMultiHostName"];
            }
            set
            {
                this["AllowMultiHostName"] = value;
            }
        }


        /// <summary>
        /// 多个随机的host
        /// </summary>
        [ConfigurationProperty("Hosts", IsRequired = true)]
        public Hosts Hosts
        {
            get
            {
                return (Hosts)this["Hosts"];
            }
            set
            {
                this["Hosts"] = value;
            }

        }
    }

    /// <summary>
    /// FckEditor 配置信息类：上传文件的目录和文件命名配置
    /// </summary>
    public class UploadConfig : ConfigurationElement
    {
        /// <summary>
        /// 使用年度、月份自动创建目录 如 2008/04/
        /// </summary>
        [ConfigurationProperty("AutoDirectory", IsRequired = true, IsKey = true)]
        public bool AutoDirectory
        {
            get
            {
                return (bool)this["AutoDirectory"];
            }
            set
            {
                this["AutoDirectory"] = value;
            }
        }
        /// <summary>
        /// 使用时间自动文件命名，如 000103211
        /// </summary>
        [ConfigurationProperty("AutoFileName", IsRequired = true, IsKey = true)]
        public bool AutoFileName
        {
            get
            {
                return (bool)this["AutoFileName"];
            }
            set
            {
                this["AutoFileName"] = value;
            }
        }

    }
    /// <summary>
    /// FckEditor 配置信息类：记录上传信息的类
    /// </summary>
    public class RecorderConfig : ConfigurationElement
    {
        /// <summary>
        /// 是否开启记录
        /// </summary>
        [ConfigurationProperty("Enabled", IsRequired = true)]
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
        /// 类名，例如 FredCK.FCKeditorV2.FileBrowser.FCKUploadRecorder,FredCK.FCKUploadRecorder
        /// </summary>
        [ConfigurationProperty("ClassName", IsRequired = true)]
        public string ClassName
        {
            get
            {
                return (string)this["ClassName"];
            }
            set
            {
                this["ClassName"] = value;
            }
        }

    } 
}
