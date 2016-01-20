using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Jita.Common.Attr;
using Jita.DAL;
using Jita.Data.Model;

namespace Jita.BLL
{
    public class bll_HumorInfo
    {
        /// <summary>
        /// 添加帖子
        /// </summary>
        /// <param name="humorInfo"></param>
        /// <returns></returns>
        //[Attr_AddSolr(0, "Id")]
        //public static int AddHumorInfo(T_Humor_HumorInfo humorInfo)
        //{
        //    if (string.IsNullOrWhiteSpace(humorInfo.HumorContent))
        //    {
        //        return 0;
        //    }
        //    int res = dal_HumorInfo.AddHumorInfo(humorInfo);
        //    humorInfo.Id = res;
        //    return res;
        //}
        [Attr_CacheData("CacheHumorList")]
        public static List<int> GetHumorInfos()
        {
            DataTable dt = dal_HumorInfo.GetHumorInfo();
            if (dt == null || dt.Rows == null || dt.Rows.Count == 0) return null;
            List<int> list = new List<int>(dt.Rows.Count);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(Convert.ToInt32(dr["Id"]));
            }
            return list;
        }
        /// <summary>
        /// 获取首页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="startTime"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        [Attr_CacheData("CacheHumorList")]
        public static List<int> GetIndexHumorInfos(int pageIndex, int pageSize, DateTime startTime, out int recordCount)
        {
            DataTable dt = dal_HumorInfo.GetIndexHumor(pageIndex, pageSize, startTime, out recordCount);
            if (dt == null || dt.Rows == null || dt.Rows.Count == 0) return null;
            List<int> ids = new List<int>(dt.Rows.Count);
            foreach (DataRow dr in dt.Rows)
            {
                ids.Add(Convert.ToInt32(dr["Id"]));
            }
            return ids;
        }
    }
}
