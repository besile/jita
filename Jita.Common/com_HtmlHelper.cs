using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KouBei.Common
{
    public class com_HtmlHelper
    {
        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="htmlString">包括HTML的源码   </param>
        /// <returns>已经去除后的文字</returns>
        public static string RemoveHtmlTag(string htmlString)
        {
            if (string.IsNullOrWhiteSpace(htmlString))
            {
                return string.Empty;
            }
            //删除脚本
            htmlString = Regex.Replace(htmlString, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            htmlString = Regex.Replace(htmlString, @"<img[^>]*?>.*?(>| />|</img>)", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"-->", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"<!--.*", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(nbsp|#160);", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            htmlString.Replace("<", "");
            htmlString.Replace(">", "");
            htmlString.Replace("\r\n", "");

            return htmlString;
        }
        /// <summary>
        /// 替换标签
        /// </summary>
        /// <param name="content"></param>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public static string RemoveTags(string content, string tagname)
        {
            string regexstr = "<" + tagname + ">.+?</" + tagname + ">";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

    }
}
