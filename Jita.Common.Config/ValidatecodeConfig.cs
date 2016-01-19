using System.Configuration;
namespace Jita.Common.Config
{
    public class ValidatecodeConfig : ConfigurationElement
    {

        public ValidatecodeConfig()
        {

        }

        public ValidatecodeConfig(string elementName)
        {
            Name = elementName;
        }

        [ConfigurationProperty("ValidatecodeName", IsRequired = true, IsKey = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{};'\"|\\", MinLength = 0, MaxLength = 600)]
        public string Name
        {
            get
            {
                return (string)this["ValidatecodeName"];
            }
            set
            {
                this["ValidatecodeName"] = value;
            }
        }

        [ConfigurationProperty("Width")]
        //[IntegerValidator(MinValue=20)]
        public int Width
        {
            get
            {
                return (int)this["Width"];
            }
            set
            {
                this["Width"] = value;
            }
        }

        [ConfigurationProperty("Height")]
        //[IntegerValidator(MinValue = 15)]
        public int Height
        {
            get
            {
                return (int)this["Height"];
            }
            set
            {
                this["Height"] = value;
            }
        }

        [ConfigurationProperty("FontName", IsRequired = true, IsKey = true)]
        //[StringValidator(InvalidCharacters = "~!@#$%^&*()[]{};'\"|\\", MinLength = 3, MaxLength = 60)]
        public string FontName
        {
            get
            {
                return (string)this["FontName"];
            }
            set
            {
                this["FontName"] = value;
            }
        }

        [ConfigurationProperty("FontSize")]
        //[IntegerValidator(MinValue = 5)]
        public int FontSize
        {
            get
            {
                return (int)this["FontSize"];
            }
            set
            {
                this["FontSize"] = value;
            }
        }

        [ConfigurationProperty("IsDrawNoise", DefaultValue = "false")]
        public bool IsDrawNoise
        {
            get
            {
                return (bool)this["IsDrawNoise"];
            }
            set
            {
                this["IsDrawNoise"] = value;
            }
        }

        [ConfigurationProperty("CharCount")]
        //[IntegerValidator(MinValue = 4)]
        public int CharCount
        {
            get
            {
                return (int)this["CharCount"];
            }
            set
            {
                this["CharCount"] = value;
            }
        }

        [ConfigurationProperty("IsUseNumber")]
        public bool IsUseNumber
        {
            get
            {
                return (bool)this["IsUseNumber"];
            }
            set
            {
                this["IsUseNumber"] = value;
            }
        }

        [ConfigurationProperty("IsDistorted", DefaultValue = true)]
        public bool IsDistorted
        {
            get
            {
                return (bool)this["IsDistorted"];
            }
            set
            {
                this["IsDistorted"] = value;
            }
        }

    }
}
