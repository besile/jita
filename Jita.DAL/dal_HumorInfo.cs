using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Jita.Common;
using Jita.Common.Config;
using Jita.Config;

namespace Jita.DAL
{
    public class dal_HumorInfo
    {
        
        /// <summary>
        /// 通过帖子ID获取帖子信息
        /// </summary>
        /// <param name="humorId"></param>
        /// <returns></returns>
        public static DataTable GetHumorInfoById(int humorId)
        {
            DataTable dt = SqlHelper.ExecuteDataset(AppConfig.GetApp("Humor"), CommandType.StoredProcedure, "GetHumorInfoById",
                new SqlParameter[] { new SqlParameter("@HumorId", humorId) }).Tables[0];
            return dt;
        }
        /// <summary>
        /// 通过帖子IDS获取帖子信息
        /// </summary>
        /// <param name="humorIds"></param>
        /// <returns></returns>
        public static DataTable GetHumorInfoByIds(string humorIds)
        {
            DataTable dt = SqlHelper.ExecuteDataset(DBConfig.GetDb("Humor"), CommandType.StoredProcedure, "GetHumorInfoByIds",
                new SqlParameter[] { new SqlParameter("@HumorIds", humorIds) }).Tables[0];
            return dt;
        }
        /// <summary>
        /// 获取口碑信息测试
        /// </summary>
        /// <returns></returns>
        public static DataTable GetHumorInfo()
        {
            DataTable dt = SqlHelper.ExecuteDataset(DBConfig.GetDb("Humor"), CommandType.Text,
                "SELECT TOP 10 * FROM dbo.T_Humor_HumorInfo(NOLOCK) ", null).Tables[0];
            return dt;
        }
        /// <summary>
        /// 获取首页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="startTime"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetIndexHumor(int pageIndex, int pageSize, DateTime startTime, out int recordCount)
        {
            SqlParameter[] prams = {
                                      new SqlParameter("@PageIndex", pageIndex),
                                      new SqlParameter("@PageSize", pageSize),
                                      new SqlParameter("@StartTime", startTime),
                                      new SqlParameter("@RecordCount", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Default, null)
                                   };
            DataTable dt = SqlHelper.ExecuteDataset(DBConfig.GetDb("Humor")
                , CommandType.StoredProcedure
                , "GetIndexHumor"
                , prams).Tables[0];
            recordCount = Convert.ToInt32(prams[prams.Length - 1].Value);
            return dt;
        }
    }
}
