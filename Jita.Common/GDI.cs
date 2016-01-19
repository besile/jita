using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Jita.Common
{
    public class GDI
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoUrl"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="saveType">0:default;1:fill,2:max</param>
        /// <returns></returns>
        //static string GetImgHandlerUrl(string photoUrl, int? height, int width, int saveType, int? quality, bool isUseRealUrlResize)
        //{
        //    if (isUseRealUrlResize)
        //    {
        //        photoUrl = GetImgRealUrl(photoUrl);
        //    }
        //    string oldUrl = photoUrl;
        //    photoUrl = photoUrl.Replace("http://", "");
        //    int subfixIndex = photoUrl.LastIndexOf('.');
        //    string subfix = "jpg";
        //    if (subfixIndex > 0)
        //    {
        //        subfix = photoUrl.Remove(0, photoUrl.LastIndexOf('.') + 1);
        //        photoUrl = photoUrl.Replace("http://", "").Remove(photoUrl.LastIndexOf('.'));
        //    }
        //    switch (saveType)
        //    {
        //        case 0://限制宽高
        //            photoUrl = string.Format("V2{0}_{1}_{2}_{3}", photoUrl, width, height ?? 0, subfix.Trim());
        //            break;
        //        case 1://填充
        //            photoUrl = string.Format("V2{0}_{1}_{2}_fill_{3}", photoUrl, width, height ?? 0, subfix.Trim());
        //            break;
        //        case 2://最大宽
        //            photoUrl = string.Format("V2{0}_{1}_{2}_max_{3}", photoUrl, width, height ?? 0, subfix.Trim());
        //            break;
        //        case 3://最大高
        //            photoUrl = string.Format("V2{0}_{1}_{2}_maxh_{3}", photoUrl, width, height ?? 0, subfix.Trim());
        //            break;
        //    }
        //    if (quality.HasValue)
        //    {
        //        photoUrl = string.Format("{0}_{1}", photoUrl, quality.Value);
        //    }
        //    return GetImgAPP(oldUrl) + photoUrl + ".jpg";
        //}
        //static string GetImgHandlerUrl(string photoUrl, int? height, int width, int saveType, int? quality)
        //{
        //    return GetImgHandlerUrl(photoUrl, height, width, saveType, quality, true);
        //}
        public static string GetImgRealUrl(string photoUrl)
        {
            if (string.IsNullOrEmpty(photoUrl))
            {
                return "";
            }
            Regex reg = new Regex(@".+?V2(?<url>.*)_(?<width>\d+)_(?<height>\d+)(_(?<isfill>fill|max|maxh))?_(?<suffix>[a-zA-Z]+)(_(?<QualityValue>\d+))?");
            int subfixIndex = photoUrl.LastIndexOf(".");
            if (subfixIndex > 0)
            {
                if (reg.IsMatch(photoUrl.Remove(subfixIndex)))
                {
                    GroupCollection gc = reg.Match(photoUrl).Groups;
                    List<string> arr = new List<string>();
                    photoUrl = "http://" + gc["url"].Value + "." + gc["suffix"].Value;
                }
            }
            else { photoUrl = ""; }
            return photoUrl;
        }
        /// <summary>
        /// 重新设置图片大小
        /// </summary>
        /// <param name="photoUrl">原有图片路径</param>
        /// <param name="height">期望高</param>
        /// <param name="width">期望宽</param>
        /// <returns>重新设置图片后路径</returns>
        //public static string ResizeImage(string photoUrl, int height, int width)
        //{
        //    if (!photoUrl.StartsWith("http:"))
        //    {
        //        if (photoUrl.StartsWith("/"))
        //        {
        //            photoUrl = "http://my.baa.bitauto.com" + photoUrl;
        //        }
        //        else
        //        {
        //            photoUrl = "http://my.baa.bitauto.com/" + photoUrl;
        //        }
        //    }
        //    return GetImgHandlerUrl(photoUrl, height, width, 0, null);//GetImgAPP(photoUrl) + Convert.ToBase64String(Encoding.UTF8.GetBytes(photoUrl + "_=_" + width + "_=_" + height)) + ".jpg";
        //}

        /// <summary>
        /// 帖子正文内图片最大宽度
        /// </summary>
        public static int ImageMaxWidth
        {
            get
            {
                int width = 530;
                int.TryParse(ConfigurationManager.AppSettings["ImageMaxWidth"], out width);
                return width;
            }
        }

        /// <summary>
        /// 帖子正文内图片质量
        /// </summary>
        public static int ImageQuality
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["ImageQuality"]);
            }
        }

        /// <summary>
        /// 重新设置图片大小
        /// </summary>
        /// <param name="photoUrl">原有图片路径</param>
        /// <param name="maxWidth">图片最大宽度</param>
        /// <returns></returns>
        //public static string ResizeImage(string photoUrl, int maxWidth)
        //{
        //    return GetImgHandlerUrl(photoUrl, null, maxWidth, 2, null);
        //}
        /// <summary>
        /// 重新设置图片大小
        /// </summary>
        /// <param name="photoUrl">原有图片路径</param>
        /// <param name="maxWidth">图片最大宽度</param>
        /// <param name="quality">图片质量</param>
        /// <returns></returns>
        //public static string ResizeImageWithQuality(string photoUrl, int maxWidth, int quality)
        //{
        //    return GetImgHandlerUrl(photoUrl, null, maxWidth, 2, quality);
        //}
        //public static string ResizeMaxHeightImage(string photoUrl, int maxHeight)
        //{
        //    return GetImgHandlerUrl(photoUrl, null, maxHeight, 3, null, false);
        //}
        /// <summary>
        /// 重新设置图片大小
        /// </summary>
        /// <param name="photoUrl">原有图片路径</param>
        /// <param name="longestBorder">最长边</param>
        /// <param name="isFill">是否填充</param>
        /// <returns>重新设置图片后路径</returns>
        //public static string ResizeImage(string photoUrl, int longestBorder, bool isFill)
        //{
        //    if (!photoUrl.StartsWith("http:"))
        //    {
        //        if (photoUrl.StartsWith("/"))
        //        {
        //            photoUrl = "http://my.baa.bitauto.com" + photoUrl;
        //        }
        //        else
        //        {
        //            photoUrl = "http://my.baa.bitauto.com/" + photoUrl;
        //        }
        //    }
        //    //string src = photoUrl + "_=_" + longestBorder;
        //    //if (isFill)
        //    //{
        //    //    src += "fill";
        //    //}

        //    if (!photoUrl.StartsWith("http:"))
        //    {
        //        photoUrl = "http://my.baa.bitauto.com/" + photoUrl;
        //    }

        //    return GetImgHandlerUrl(photoUrl, null, longestBorder, isFill ? 1 : 0, null); //GetImgAPP(photoUrl) + Convert.ToBase64String(Encoding.UTF8.GetBytes(src)) + ".jpg";
        //}

        public static String GetFullSpaceImageUrl(string urlData)
        {
            if (string.IsNullOrEmpty(urlData) || urlData.Contains(@"avatars\common\0.gif"))
            {
                return "http://pic.baa.bitautotech.com/webpic/images/defaultuser.gif";
            }
            if (urlData.StartsWith("http:"))
            {
                return urlData;
            }
            urlData = urlData.Trim().Replace("\\", "/");
            if (urlData.StartsWith("upload/day_"))
            {
                urlData = urlData.Replace("upload/", "http://pic.baa.bitautotech.com/DzUsergroupFiles/");
            }
            else if (!urlData.StartsWith("http:") && !urlData.StartsWith("//192."))
            {
                urlData = System.Configuration.ConfigurationManager.AppSettings["SpacePicURL"] + urlData;
            }
            return urlData;
        }

        [Obsolete("此方法已停用,请使用Common.UrlManager.GetAttachmentUrl方法", true)]
        public static String GetFullAttachmentImageUrl(string urlData)
        {
            urlData = urlData.Trim().Replace("\\", "/");
            if (urlData.StartsWith("upload/day_"))
            {
                urlData = urlData.Replace("upload/", "http://pic.baa.bitautotech.com/DzUsergroupFiles/");
            }
            else if (!urlData.StartsWith("http:") && !urlData.StartsWith("//192."))
            {
                urlData = System.Configuration.ConfigurationManager.AppSettings["AttachRootUrl"] + urlData;
            }
            return urlData;
        }

        # region 将数据库中的头像地扯转换成页面上能直接使用的地扯
        public static String GetSpaceAvatarUrl(string urlData)
        {
            urlData = urlData.Trim();
            if (urlData == "")
            {
                return "http://pic.baa.bitautotech.com/webpic/images/user.gif";
            }
            urlData = urlData.Trim().Replace("\\", "/");
            if (urlData.StartsWith("customavatars/"))
            {
                urlData = "http://my.baa.bitauto.com/" + urlData;
            }
            else if (urlData.Contains(@"avatars/common/0.gif"))
            {
                return "http://pic.baa.bitautotech.com/webpic/images/defaultuser.gif";
            }
            else if (!urlData.StartsWith("http:"))
            {
                urlData = ConfigurationManager.AppSettings["AvatarRootUrl"] + urlData;
            }

            return urlData;
        }

        public static String GetSpaceAvatarUrl(string urlData, string gender)
        {
            urlData = urlData.Trim();
            if (string.IsNullOrEmpty(urlData))
            {
                //return "http://pic.baa.bitautotech.com/webpic/images/user.gif";
                return gender == "2" ? "http://baa.bitauto.com/images/user1.gif" : "http://baa.bitauto.com/images/user.gif";
            }
            urlData = urlData.Trim().Replace("\\", "/");
            if (urlData.StartsWith("customavatars/"))
            {
                urlData = "http://my.baa.bitauto.com/" + urlData;
            }
            else if (!urlData.StartsWith("http:"))
            {
                urlData = ConfigurationManager.AppSettings["AvatarRootUrl"] + urlData;
            }

            return urlData;
        }
        # endregion

        //private static string GetImgAPP(string url)
        //{
        //    url = url.Trim();
        //    int lastNum = GetRightFirstNum(url);
        //    int index = lastNum % UrlManager.NASServerHostArray.Length;
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(UrlManager.NASServerHostArray[index]);
        //    sb.Append("img/");
        //    return sb.ToString();
        //}
        /// <summary>
        /// 获取字符串从右至左的第一个数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static int GetRightFirstNum(string str)
        {
            int lastNum = 0;
            Regex regex = new Regex(@".+(\d).+?", RegexOptions.IgnoreCase);
            Match match = regex.Match(str);
            if (match.Success)
            {
                lastNum = int.Parse(match.Groups[1].Value);
            }
            return lastNum;
        }
        static GDI()
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {

                if (codec.FormatID == ImageFormat.Bmp.Guid)
                {
                    codecDict.Add(ImageFormat.Bmp, codec);
                    continue;
                }
                if (codec.FormatID == ImageFormat.Emf.Guid)
                {
                    codecDict.Add(ImageFormat.Emf, codec);
                    continue;
                }
                if (codec.FormatID == ImageFormat.Exif.Guid)
                {
                    codecDict.Add(ImageFormat.Exif, codec);
                    continue;
                }
                if (codec.FormatID == ImageFormat.Gif.Guid)
                {
                    codecDict.Add(ImageFormat.Gif, codec);
                    continue;
                }
                if (codec.FormatID == ImageFormat.Icon.Guid)
                {
                    codecDict.Add(ImageFormat.Icon, codec);
                    continue;
                }
                if (codec.FormatID == ImageFormat.Jpeg.Guid)
                {
                    codecDict.Add(ImageFormat.Jpeg, codec);
                    continue;
                }
                if (codec.FormatID == ImageFormat.MemoryBmp.Guid)
                {
                    codecDict.Add(ImageFormat.MemoryBmp, codec);
                    continue;
                }
                if (codec.FormatID == ImageFormat.Png.Guid)
                {
                    codecDict.Add(ImageFormat.Png, codec);
                    continue;
                }
                if (codec.FormatID == ImageFormat.Tiff.Guid)
                {
                    codecDict.Add(ImageFormat.Tiff, codec);
                    continue;
                }
                if (codec.FormatID == ImageFormat.Wmf.Guid)
                {
                    codecDict.Add(ImageFormat.Wmf, codec);
                    continue;
                }

            }
            LosslessParam = new EncoderParameters(3);
            LosslessParam.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.RenderMethod, (int)System.Drawing.Imaging.EncoderValue.RenderProgressive);
            LosslessParam.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            LosslessParam.Param[2] = new EncoderParameter(System.Drawing.Imaging.Encoder.RenderMethod, (int)System.Drawing.Imaging.EncoderValue.ScanMethodInterlaced);
        }

        static Dictionary<ImageFormat, ImageCodecInfo> codecDict = new Dictionary<ImageFormat, ImageCodecInfo>();
        /// <summary>
        /// 获取无损压缩参数
        /// </summary>
        public static EncoderParameters LosslessParam { get; private set; }
        /// <summary>
        /// 获取图形编码器信息
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            if (codecDict.ContainsKey(format)) return codecDict[format];
            else return codecDict[ImageFormat.Png];
        }

        public static string ResizeImage(string p, string p_2)
        {
            throw new NotImplementedException();
        }
    }
}