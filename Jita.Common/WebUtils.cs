using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace Jita.Common
{
    public class WebUtils
    {
        ///  <summary> 
        ///  取得客户端真实IP。如果有代理则取第一个非内网地址
        ///  </summary> 
        public static string GetRequestIP()
        {
            IPAddress tempIP;

            string result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_CDN_SRC_IP"];
            if (!string.IsNullOrEmpty(result))
            {
                if (IPAddress.TryParse(result, out tempIP) && result.Substring(0, 3) != "10."
                                    && result.Substring(0, 7) != "192.168"
                                    && result.Substring(0, 7) != "172.16.")  //代理即是IP格式  
                {
                    return result;
                }
            }


            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (result != null && result != String.Empty)
            {

                //可能有代理
                if (result.IndexOf(".") == -1)        //没有“.”肯定是非IPv4格式
                    result = null;
                else
                {
                    if (result.IndexOf(",") != -1)
                    {
                        //有“,”，估计多个代理。取第一个不是内网的IP。
                        result = result.Replace(" ", "").Replace("'", "").Replace("\"", "");
                        string[] temparyip = result.Split(",;".ToCharArray());
                        for (int i = 0; i < temparyip.Length; i++)
                        {

                            if (IPAddress.TryParse(temparyip[i], out tempIP)
                                    && temparyip[i].Substring(0, 3) != "10."
                                    && temparyip[i].Substring(0, 7) != "192.168"
                                    && temparyip[i].Substring(0, 7) != "172.16.")
                            {
                                return temparyip[i];        //找到不是内网的地址
                            }
                        }
                    }
                    else if (IPAddress.TryParse(result, out tempIP))  //代理即是IP格式
                        return result;
                    else
                        result = null;        //代理中的内容 非IP，取IP 
                }
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (result == null || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            return result;
        }

        /// <summary>
        /// 获取用户友好的日期格式
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetHumanFriendDate(DateTime date)
        {

            string result;
            TimeSpan ts = date.Date.Subtract(DateTime.Today);
            int xday = ts.Days;


            switch (xday)
            {
                case 0:
                    result = date.ToString("今天 HH : mm");
                    break;
                case 1:
                    result = date.ToString("昨天 HH : mm");
                    break;
                case 2:
                    result = date.ToString("前天 HH : mm");
                    break;
                default:
                    result = date.ToString("yyyy-MM-dd");
                    break;
            }

            return result;
        }

        /// <summary>
        /// 通过URL获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static DataSet GetDataSetByUrl(string url)
        {
            DataSet ds = new DataSet();
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(url);
                stream = new StringReader(xmlDoc.InnerXml);
                reader = new XmlTextReader(stream);
                ds.ReadXml(reader);
                return ds;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        public static string GetSubString(string str, int l)
        {
            string temp = str;
            if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= l)
            {
                return temp;
            }
            for (int i = temp.Length; i >= 0; i--)
            {
                temp = temp.Substring(0, i);
                if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= l - 3)
                {
                    return temp + "...";
                }
            }
            return "...";
        }


        public static void PostData(string url, System.Collections.Specialized.NameValueCollection formData)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                client.UploadValues(url, formData);
            }
        }

        public static string GetData(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                return client.DownloadString(url);
            }
        }

        static string[] shareUrlFormats ={ 
            ConfigurationManager.AppSettings["ShareUrl.Format.Kaixin001"]??"http://www.kaixin001.com/!repaste/repaste.php?rtitle={0}&rurl={1}&rcontent={2}",
            ConfigurationManager.AppSettings["ShareUrl.Format.Xiaonei"]??"http://share.renren.com/share/buttonshare.do?title={0}&link={1}#nogo",
            ConfigurationManager.AppSettings["ShareUrl.Format.QQ"]??"http://shuqian.qq.com/post?jumpback=1&title={0}&uri={1}",
            ConfigurationManager.AppSettings["ShareUrl.Format.Delicious"]??"http://delicious.com/post?title={0}&url={1}",
        };

        /// <summary>
        /// 301跳转
        /// </summary>
        /// <param name="url"></param>
        public static void Redirect301(string url)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.StatusCode = 301;
            response.Status = "301 MovedPermanently";
            response.AddHeader("Location", url);
        }

        

        /// <summary>
        /// 以指定的ContentType输出指定文件文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="filename">输出的文件名</param>
        /// <param name="filetype">将文件输出时设置的ContentType</param>
        public static void ResponseFile(string filepath, string filename, string filetype)
        {
            Stream iStream = null;

            // 缓冲区为10k
            byte[] buffer = new Byte[10000];

            // 文件长度
            int length;

            // 需要读的数据长度
            long dataToRead;

            try
            {
                // 打开文件
                iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);


                // 需要读的数据长度
                dataToRead = iStream.Length;

                HttpContext.Current.Response.ContentType = filetype;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename.Trim()).Replace("+", " "));

                while (dataToRead > 0)
                {
                    // 检查客户端是否还处于连接状态
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        length = iStream.Read(buffer, 0, 10000);
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        buffer = new Byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        // 如果不再连接则跳出死循环
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    // 关闭文件
                    iStream.Close();
                }
            }
            HttpContext.Current.Response.End();
        }
    }
}
