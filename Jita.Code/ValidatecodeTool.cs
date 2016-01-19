using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web;
using Jita.Common.Config;

namespace Jita.Code
{
    /// <summary>
    /// 验证码工具的使用接口
    /// </summary>
    public class ValidatecodeTool
    {
        #region Fields

        //生成的验证码存储在Session中的变量名
        private const string ValidatecodeVaryName = "CurrentValidatecode_zxcekqlockde";

        #endregion

        #region 输出验证码图片

        #region Recommended OutputImage Methods

        /// <summary>
        /// 输出验证码图片 
        /// 参看OutputImage(string validateCodeName)方法
        /// </summary>
        public static void OutputImage()
        {
            ValidatecodeConfig validatecodeConfig = GetValidateCodeConfig();
            if (validatecodeConfig == null)
            {
                OutputImage(0, 0, null, null, 0, true, 0, false, true);
            }
            else
            {
                OutputImage(validatecodeConfig.Width,
                    validatecodeConfig.Height,
                    null, validatecodeConfig.FontName,
                    validatecodeConfig.FontSize,
                    validatecodeConfig.IsDrawNoise,
                    validatecodeConfig.CharCount,
                    validatecodeConfig.IsUseNumber,
                    validatecodeConfig.IsDistorted);
            }
        }

        /// <summary>
        /// 输出验证码图片并且返回验证码数值
        /// </summary>
        /// <returns></returns>
        public static string OutputImageAndReturnCode()
        {
            ValidatecodeConfig validatecodeConfig = GetValidateCodeConfig();
            if (validatecodeConfig == null)
            {
                return OutputImageAndReturnCode(0, 0, null, null, 0, true, 0, false, true);
            }
            else
            {
                return OutputImageAndReturnCode(validatecodeConfig.Width,
                    validatecodeConfig.Height,
                    null, validatecodeConfig.FontName,
                    validatecodeConfig.FontSize,
                    validatecodeConfig.IsDrawNoise,
                    validatecodeConfig.CharCount,
                    validatecodeConfig.IsUseNumber,
                    validatecodeConfig.IsDistorted);
            }
        }
        /// <summary>
        /// 输出验证码图片并且返回验证码数值
        /// </summary>
        /// <param name="validatecodeName">验证码名字，用于区分验证码</param>
        /// <returns></returns>
        public static string OutputImageAndReturnCode(string validatecodeName)
        {
            ValidatecodeConfig validatecodeConfig = GetValidateCodeConfig();
            if (validatecodeConfig == null)
            {
                return OutputImageAndReturnCode(0, 0, validatecodeName, null, 0, true, 0, false, true);
            }
            else
            {
                return OutputImageAndReturnCode(validatecodeConfig.Width,
                    validatecodeConfig.Height,
                    validatecodeName,
                    validatecodeConfig.FontName,
                    validatecodeConfig.FontSize,
                    validatecodeConfig.IsDrawNoise,
                    validatecodeConfig.CharCount,
                    validatecodeConfig.IsUseNumber,
                    validatecodeConfig.IsDistorted);
            }
        }

        /// <summary>
        /// 输出验证码图片并且触发回调函数返回验证码数值
        /// </summary>
        /// <param name="ac">回调函数</param>
        public static void OutputImageByAction(Action<string> ac)
        {
            ValidatecodeConfig validatecodeConfig = GetValidateCodeConfig();
            if (validatecodeConfig == null)
            {
                ac(OutputImageAndReturnCode(0, 0, null, null, 0, true, 0, false, true));
            }
            else
            {
                ac(OutputImageAndReturnCode(validatecodeConfig.Width,
                    validatecodeConfig.Height,
                    null, validatecodeConfig.FontName,
                    validatecodeConfig.FontSize,
                    validatecodeConfig.IsDrawNoise,
                    validatecodeConfig.CharCount,
                    validatecodeConfig.IsUseNumber,
                    validatecodeConfig.IsDistorted));
            }
        }
        /// <summary>
        /// 输出验证码图片并且触发回调函数返回验证码数值
        /// </summary>
        /// <param name="validatecodeName">验证码名字，用于区分验证码</param>
        /// <param name="ac">回调函数</param>
        public static void OutputImageByAction(string validatecodeName, Action<string> ac)
        {
            ValidatecodeConfig validatecodeConfig = GetValidateCodeConfig();
            if (validatecodeConfig == null)
            {
                ac(OutputImageAndReturnCode(0, 0, validatecodeName, null, 0, true, 0, false, true));
            }
            else
            {
                ac(OutputImageAndReturnCode(validatecodeConfig.Width,
                    validatecodeConfig.Height,
                    validatecodeName,
                    validatecodeConfig.FontName,
                    validatecodeConfig.FontSize,
                    validatecodeConfig.IsDrawNoise,
                    validatecodeConfig.CharCount,
                    validatecodeConfig.IsUseNumber,
                    validatecodeConfig.IsDistorted));
            }
        }

        /// <summary>
        /// 输出指定验证码名称的验证码图片,
        /// Web.Config中需要进行类似如下的配置
        /// <![CDATA[
        ///      <configSections>
        ///          <!--验证码的配置Section-->
        ///          <section name="BitAuto.Utils.ValidatecodeConfig" type="BitAuto.Utils.ValidatecodeConfigurationSectionHandler, BitAuto.Utils"/>
        ///      </configSections>
        ///      <!--DefaultValidatecodeName指定的名字在Validatecodes中必须存在-->
        ///      <BitAuto.Utils.ValidatecodeConfig DefaultValidatecodeName = "default">
        ///          <Validatecodes>
        ///              <!--
        ///                  Width验证图片的宽度，
        ///                  Height验证图片的高度，
        ///                  FontName输出的字符的字体,服务器存在的字体名可以在"控制面板—>字体"中查到
        ///                  FontSize字体大小,
        ///                  IsDrawNoise是否画出干扰线，
        ///                  CharCount指定画出的字符个数如果是0，则画出4——8的随机个数
        ///              -->
        ///              <add ValidatecodeName="default" Width="150" Height="40" FontName="" FontSize="30" IsDrawNoise="true" CharCount="4"></add>
        ///              <add ValidatecodeName="A" Width="200" Height="50" FontName="华文隶书" FontSize="50" IsDrawNoise="true" CharCount="0"></add>
        ///              <add ValidatecodeName="B" Width="250" Height="60" FontName="Times New Roman" FontSize="35" IsDrawNoise="true" CharCount="8"></add>
        ///          </Validatecodes>
        ///      </BitAuto.Utils.ValidatecodeConfig>
        /// ]]>
        /// </summary>
        /// 
        /// <param name="validateCodeName">验证码名字，用于区分验证码</param>
        public static void OutputImage(string validateCodeName)
        {
            ValidatecodeConfig validatecodeConfig = GetValidateCodeConfig(validateCodeName);
            if (validatecodeConfig == null)
            {
                OutputImage(0, 0, validateCodeName, null, 0, true, 0, false, true);
            }
            else
            {
                OutputImage(validatecodeConfig.Width,
                    validatecodeConfig.Height,
                    validateCodeName,
                    validatecodeConfig.FontName,
                    validatecodeConfig.FontSize,
                    validatecodeConfig.IsDrawNoise,
                    validatecodeConfig.CharCount,
                    validatecodeConfig.IsUseNumber,
                    validatecodeConfig.IsDistorted);
            }
        }

        /// <summary>
        /// 输出验证码图片
        /// </summary>
        /// <param name="width">验证图片的宽度</param>
        /// <param name="height">验证图片的高度</param>
        /// <param name="validateCodeName">验证码名称，用于同一页面支持多验证码的情况，不要重名</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="isDrawNoise">是否生成干扰线</param>
        /// <param name="charCount">验证码字符位数</param>
        /// <param name="isUserNumber">是否使用数字</param>
        /// <param name="isDistorted">是否变形</param>
        public static void OutputImage(int width, int height, string validateCodeName,
            string fontName, int fontSize, bool isDrawNoise,
            int charCount, bool isUserNumber, bool isDistorted)
        {
            ValidatecodeImage ci = new ValidatecodeImage(width, height,
                fontName, fontSize, isDrawNoise, charCount, isUserNumber, isDistorted);
            HttpContext.Current.Session[ValidatecodeVaryName + validateCodeName] = ci.Text;
            // Change the response headers to output a gif image.
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "image/gif";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Gif);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();
        }

        public static string OutputImageAndReturnCode(int width, int height, string validateCodeName, string fontName, int fontSize, bool isDrawNoise, int charCount, bool isUserNumber, bool isDistorted)
        {
            ValidatecodeImage ci = new ValidatecodeImage(width, height, fontName, fontSize, isDrawNoise, charCount, isUserNumber, isDistorted);
            string rtn = ci.Text;
            //HttpContext.Current.Session[ValidatecodeVaryName + validateCodeName] = ci.Text;
            // Change the response headers to output a gif image.
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "image/gif";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Gif);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();
            return rtn;
        }

        #endregion

        #region This Methods is just for compatibility

        /// <summary>
        /// 输出验证码图片 
        /// </summary>
        /// <param name="isDrawNoise">是否生成干扰线</param>
        [Obsolete("请使用HttpHandler生成验证图片", false)]
        public static void OutputImage(bool isDrawNoise)
        {
            OutputImage(null, isDrawNoise);
        }

        /// <summary>
        /// 输出验证码图片 
        /// </summary>
        /// <param name="fontName">字体名称</param>
        /// <param name="isDrawNoise">是否生成干扰线</param>
        [Obsolete("请使用HttpHandler生成验证图片", false)]
        public static void OutputImage(string fontName, bool isDrawNoise)
        {
            OutputImage(null, fontName, isDrawNoise);
        }

        /// <summary>
        /// 输出验证码图片 
        /// </summary>
        /// <param name="validateCodeName">验证码名称，用于同一页面支持多验证码的情况，不要重名</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="isDrawNoise">是否生成干扰线</param>
        [Obsolete("请使用HttpHandler生成验证图片", false)]
        public static void OutputImage(string validateCodeName, string fontName, bool isDrawNoise)
        {
            OutputImage(0, 0, validateCodeName, fontName, 0, isDrawNoise, 0, false, true);
        }

        #endregion

        #region GetValidatecodeConfig Methods
        /// <summary>
        /// 新配置上线后调用这个函数，取代旧的GetValidatecodeConfig()
        /// </summary>
        /// <returns></returns>
        private static ValidatecodeConfig GetValidateCodeConfig()
        {
            return GetValidateCodeConfig(null);
        }

        private static ValidatecodeConfig GetValidateCodeConfig(string ValidatecodeName)
        {
            ValidatecodeConfigRoot vConfig = CommonPlatformConfiguration.GetValidatecodeConfigRoot();
            if (vConfig == null)
            {
                return null;
            }
            ValidatecodeConfig validatecodeConfig = null;
            if (!string.IsNullOrEmpty(ValidatecodeName))
            {
                validatecodeConfig = vConfig.ValidateCodes[ValidatecodeName];
            }
            if (validatecodeConfig == null)
            {
                validatecodeConfig = vConfig.ValidateCodes[vConfig.DefaultValidateCodeName];
            }
            return validatecodeConfig;
        }
        #endregion

        #endregion

        #region 验证输入的验证码是否相符
        /// <summary>
        /// 验证输入的验证码是否相符，不区分大小写,
        /// 参看ValidateInputcode(string validateCodeName, string code)方法
        /// </summary>
        /// <param name="code">用户输入的验证码，不区分大小写</param>
        /// <returns></returns>
        public static bool ValidateInputcode(string code)
        {
            return ValidateInputcode(null, code);
        }

        /// <summary>
        /// 验证输入的验证码是否相符，不区分大小写,
        /// 参看ValidateInputcode(string validateCodeName, string code)方法
        /// </summary>
        /// <param name="code">用户输入的验证码，不区分大小写</param>
        /// <param name="isClearCodeInSession">
        /// 是否清除Session中的验证码，此参数最好传true，否则会引入安全漏洞；
        /// 使用场景为：客户端验证时传false，服务器端验证时再传true。
        /// </param>
        /// <returns></returns>
        public static bool ValidateInputcode(string code, bool isClearCodeInSession)
        {
            return ValidateInputcode(null, code, isClearCodeInSession);
        }

        /// <summary>
        /// 验证输入的验证码是否相符，不区分大小写
        /// Web.Config中需要进行类似如下的配置
        /// <![CDATA[
        ///      <configSections>
        ///          <!--验证码的配置Section-->
        ///          <section name="BitAuto.Utils.ValidatecodeConfig" type="BitAuto.Utils.ValidatecodeConfigurationSectionHandler, BitAuto.Utils"/>
        ///      </configSections>
        ///      <!--DefaultValidatecodeName指定的名字在Validatecodes中必须存在-->
        ///      <BitAuto.Utils.ValidatecodeConfig DefaultValidatecodeName = "default">
        ///          <Validatecodes>
        ///              <!--
        ///                  Width验证图片的宽度，
        ///                  Height验证图片的高度，
        ///                  FontName输出的字符的字体,服务器存在的字体名可以在"控制面板—>字体"中查到
        ///                  FontSize字体大小,
        ///                  IsDrawNoise是否画出干扰线，
        ///                  CharCount指定画出的字符个数如果是0，则画出4——8的随机个数
        ///              -->
        ///              <add ValidatecodeName="default" Width="150" Height="40" FontName="" FontSize="30" IsDrawNoise="true" CharCount="4"></add>
        ///              <add ValidatecodeName="A" Width="200" Height="50" FontName="华文隶书" FontSize="50" IsDrawNoise="true" CharCount="0"></add>
        ///              <add ValidatecodeName="B" Width="250" Height="60" FontName="Times New Roman" FontSize="35" IsDrawNoise="true" CharCount="8"></add>
        ///          </Validatecodes>
        ///      </BitAuto.Utils.ValidatecodeConfig>
        /// ]]>
        /// </summary>
        /// <param name="validateCodeName">验证码名称，用于同一页面支持多验证码的情况</param>
        /// <param name="code">用户输入的验证码，不区分大小写</param>
        /// <returns>校验结果，true——通过，false——不正确</returns>
        public static bool ValidateInputcode(string validateCodeName, string code)
        {
            return ValidateInputcode(validateCodeName, code, true);
        }

        /// <summary>
        /// 验证输入的验证码是否相符，不区分大小写
        /// Web.Config中需要进行类似如下的配置
        /// <![CDATA[
        ///      <configSections>
        ///          <!--验证码的配置Section-->
        ///          <section name="BitAuto.Utils.ValidatecodeConfig" type="BitAuto.Utils.ValidatecodeConfigurationSectionHandler, BitAuto.Utils"/>
        ///      </configSections>
        ///      <!--DefaultValidatecodeName指定的名字在Validatecodes中必须存在-->
        ///      <BitAuto.Utils.ValidatecodeConfig DefaultValidatecodeName = "default">
        ///          <Validatecodes>
        ///              <!--
        ///                  Width验证图片的宽度，
        ///                  Height验证图片的高度，
        ///                  FontName输出的字符的字体,服务器存在的字体名可以在"控制面板—>字体"中查到
        ///                  FontSize字体大小,
        ///                  IsDrawNoise是否画出干扰线，
        ///                  CharCount指定画出的字符个数如果是0，则画出4——8的随机个数
        ///              -->
        ///              <add ValidatecodeName="default" Width="150" Height="40" FontName="" FontSize="30" IsDrawNoise="true" CharCount="4"></add>
        ///              <add ValidatecodeName="A" Width="200" Height="50" FontName="华文隶书" FontSize="50" IsDrawNoise="true" CharCount="0"></add>
        ///              <add ValidatecodeName="B" Width="250" Height="60" FontName="Times New Roman" FontSize="35" IsDrawNoise="true" CharCount="8"></add>
        ///          </Validatecodes>
        ///      </BitAuto.Utils.ValidatecodeConfig>
        /// ]]>
        /// </summary>
        /// <param name="validateCodeName">验证码名称，用于同一页面支持多验证码的情况</param>
        /// <param name="code">用户输入的验证码，不区分大小写</param>
        /// <param name="isClearCodeInSession">
        /// 是否清除Session中的验证码，此参数最好传true，否则可能会引入安全漏洞；
        /// 使用场景为：客户端验证时传false，服务器端验证时再传true。
        /// </param>
        /// <returns>校验结果，true——通过，false——不正确</returns>
        public static bool ValidateInputcode(string validateCodeName, string code, bool isClearCodeInSession)
        {
            if (validateCodeName == null)
            {
                validateCodeName = "";
            }
            string storedCode = (string)HttpContext.Current.Session[ValidatecodeVaryName + validateCodeName];
            if (code != null && storedCode != null
                && code.ToUpper().Equals(storedCode.ToUpper()))
            {
                if (isClearCodeInSession)
                {
                    HttpContext.Current.Session[ValidatecodeVaryName + validateCodeName] = null;
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region 验证图片生成类

        /// <summary>
        /// 本类封装了验证码的生成算法.
        /// </summary>
        class ValidatecodeImage
        {
            #region Fields

            //缺省宽度
            private const int DefaultWidth = 200;
            //缺省高度
            private const int DefaultHeight = 50;
            //缺省的字体
            private const string DefaultFontfamilyName = "Century Schoolbook";
            //缺省字体大小
            private const int DefaultFontSize = 50;

            // Internal properties.
            private string m_text;
            private int m_width;
            private int m_height;
            private string m_fontName;
            private Bitmap m_image;
            private bool m_isDrawNoise;
            private int m_charCount;
            private int m_fontSize;
            private bool m_isDistorted;

            // For generating random numbers.
            private Random random = new Random();

            #endregion

            #region Properties

            // Public properties (all read-only).
            /// <summary>
            /// 
            /// </summary>
            public string Text
            {
                get { return this.m_text; }
            }
            /// <summary>
            /// 
            /// </summary>
            public Bitmap Image
            {
                get { return this.m_image; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int Width
            {
                get { return this.m_width; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int Height
            {
                get { return this.m_height; }
            }

            #endregion

            #region Constructs

            // ====================================================================
            // Initializes a new instance of the CaptchaImage class using the
            // specified m_text, width, height and font family.
            // ====================================================================
            /// <summary>
            /// 
            /// </summary>
            /// <param name="width">宽度</param>
            /// <param name="height">高度</param>
            /// <param name="fontName">字体名称</param>
            /// <param name="fontSize">字体大小</param>
            /// <param name="isDrawNoise">是否画干扰线</param>
            /// <param name="charCount">字符个数</param>
            /// <param name="isUserNumber">是否使用数字</param>
            /// <param name="isDistorted">是否变形</param>
            public ValidatecodeImage(int width, int height, string fontName,
                int fontSize, bool isDrawNoise, int charCount,
                bool isUserNumber, bool isDistorted)
            {
                this.m_charCount = charCount;
                this.m_text = GenerateRandomString(isUserNumber);
                this.SetDimensions(
                    width == 0 ? DefaultWidth : width,
                    height == 0 ? DefaultHeight : height);
                this.SetFamilyName(string.IsNullOrEmpty(fontName) ? DefaultFontfamilyName : fontName);
                this.m_fontSize = fontSize == 0 ? DefaultFontSize : fontSize;
                this.m_isDrawNoise = isDrawNoise;
                this.m_isDistorted = isDistorted;
                this.GenerateImage();
            }

            //
            // Returns a string of six random digits.
            //
            private string GenerateRandomString(bool isUserNumber)
            {
                int charCount = m_charCount > 1 ? m_charCount : this.random.Next(4, 8);

                string s = "";
                for (int i = 0; i < charCount; i++)
                {
                    char rchar = 'A';
                    if (isUserNumber)
                    {
                        rchar = (char)('0' + this.random.Next(10));
                    }
                    else
                    {
                        rchar = (char)('A' + this.random.Next(26));
                    }
                    s = String.Concat(s, rchar.ToString());
                }
                return s;
            }

            #endregion

            #region Dispose Methods

            // ====================================================================
            // This member overrides Object.Finalize.
            // ====================================================================
            /// <summary>
            /// 
            /// </summary>
            ~ValidatecodeImage()
            {
                Dispose(false);
            }

            // ====================================================================
            // Releases all resources used by this object.
            // ====================================================================
            /// <summary>
            /// 
            /// </summary>
            public void Dispose()
            {
                GC.SuppressFinalize(this);
                this.Dispose(true);
            }

            // ====================================================================
            // Custom Dispose method to clean up unmanaged resources.
            // ====================================================================
            /// <summary>
            /// 
            /// </summary>
            /// <param name="disposing"></param>
            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                    // Dispose of the bitmap.
                    this.m_image.Dispose();
            }

            #endregion

            #region Private Methods

            // ====================================================================
            // Sets the image width and height.
            // ====================================================================
            private void SetDimensions(int width, int height)
            {
                // Check the width and height.
                if (width <= 0)
                    throw new ArgumentOutOfRangeException("width", width, "Argument out of range, must be greater than zero.");
                if (height <= 0)
                    throw new ArgumentOutOfRangeException("height", height, "Argument out of range, must be greater than zero.");
                this.m_width = width;
                this.m_height = height;
            }

            // ====================================================================
            // Sets the font used for the image m_text.
            // ====================================================================
            private void SetFamilyName(string familyName)
            {
                // If the named font is not installed, default to a system font.
                try
                {
                    Font font = new Font(this.m_fontName, GetFontSize());
                    this.m_fontName = familyName;
                    font.Dispose();
                }
                catch (Exception)
                {
                    this.m_fontName = System.Drawing.FontFamily.GenericSerif.Name;
                }
            }

            // ====================================================================
            // Creates the bitmap image.
            // ====================================================================
            private void GenerateImage()
            {
                // Create a new 32-bit bitmap image.
                Bitmap bitmap = new Bitmap(this.m_width, this.m_height, PixelFormat.Format32bppArgb);

                // Create a graphics object for drawing.
                Graphics g = Graphics.FromImage(bitmap);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                //Rectangle rect = new Rectangle(0, 0, this.width, this.height);

                // Fill in the background.
                //FillBackGround(g, rect);
                g.Clear(Color.White);

                Font font = new Font(this.m_fontName, GetFontSize(), FontStyle.Bold);
                HatchBrush brush = new HatchBrush(HatchStyle.Percent80, Color.Black, Color.White);

                int avgWidth = this.m_width / this.m_text.Length;
                for (int i = 0; i < this.m_text.Length; i++)
                {
                    int xIdx = 0;
                    if (m_isDistorted)
                    {
                        xIdx = random.Next(((i - 1) * avgWidth) * 2, ((i + 1) * avgWidth) * 2);
                    }
                    else
                    {
                        xIdx = i * avgWidth;
                    }
                    Rectangle rectOfOneChar = new Rectangle(xIdx, 0, this.m_width / this.m_text.Length, this.m_height);
                    DrawOneChar(g, this.m_text.Substring(i, 1), ref rectOfOneChar, i, font, brush);
                }

                // Add some random noise.
                if (this.m_isDrawNoise)
                {
                    AddRandomNoise(g, brush);
                }

                // Clean up.
                g.Dispose();
                font.Dispose();
                brush.Dispose();

                // Set the image.
                this.m_image = bitmap;
            }

            private int GetFontSize()
            {
                return m_fontSize < 5 ? DefaultFontSize : m_fontSize;
            }

            private Font GetFont(Graphics g, ref Rectangle rect, string oneChar)
            {
                SizeF size;
                float fontSize = rect.Height + 1;
                Font font;
                // Adjust the font size until the m_text fits within the image.
                do
                {
                    fontSize--;
                    font = new Font(this.m_fontName, fontSize, FontStyle.Bold);
                    size = g.MeasureString(oneChar, font);
                } while (size.Width > rect.Width);
                //return font;
                return new Font(this.m_fontName, GetFontSize(), FontStyle.Bold);
            }

            private void AddRandomNoise(Graphics g, HatchBrush brush)
            {
                Point thirdPoint = new Point(this.random.Next(this.m_width), this.random.Next(this.Height));
                g.DrawBezier(new Pen(brush, 5 * GetFontSize() / DefaultFontSize),
                    new Point(this.random.Next(this.m_width), this.random.Next(this.Height)),
                    new Point(this.random.Next(this.m_width), this.random.Next(this.Height)),
                    thirdPoint,
                    thirdPoint);
            }

            private void DrawOneChar(Graphics g, string oneChar, ref Rectangle rect, int charIdx, Font font, HatchBrush brush)
            {
                // Set up the m_text font.
                //Font font = GetFont(g, ref rect, oneChar);

                // Create a path using the m_text and warp it randomly.
                GraphicsPath path = new GraphicsPath();
                path.AddString(oneChar, font.FontFamily, (int)font.Style, font.Size, rect, getStringFormat());
                if (m_isDistorted)
                {
                    float v = 3;
                    PointF[] points =
			        {
				        new PointF(this.random.Next(rect.Width) / v, this.random.Next(rect.Height) / v),
				        new PointF(rect.Width - this.random.Next(rect.Width) / v, this.random.Next(rect.Height) / v),
				        new PointF(this.random.Next(rect.Width) / v, rect.Height - this.random.Next(rect.Height) / v),
				        new PointF(rect.Width - this.random.Next(rect.Width) / v, rect.Height - this.random.Next(rect.Height) / v)
			        };
                    Matrix matrix = new Matrix();
                    matrix.Translate(charIdx * this.m_width / this.m_text.Length, 0F);
                    path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);
                }
                // Draw the m_text.
                g.FillPath(brush, path);
            }

            private StringFormat getStringFormat()
            {
                StringFormat format = new StringFormat();
                int rInt = this.random.Next(0, 2);
                if (rInt == 0)
                {
                    format.Alignment = StringAlignment.Near;
                }
                else if (rInt == 1)
                {
                    format.Alignment = StringAlignment.Center;
                }
                else
                {
                    format.Alignment = StringAlignment.Far;
                }
                format.Alignment = StringAlignment.Center;

                rInt = this.random.Next(0, 2);
                if (rInt == 0)
                {
                    format.LineAlignment = StringAlignment.Near;
                }
                else if (rInt == 1)
                {
                    format.LineAlignment = StringAlignment.Center;
                }
                else
                {
                    format.LineAlignment = StringAlignment.Far;
                }

                return format;
            }

            #endregion
        }

        #endregion
    }
}
