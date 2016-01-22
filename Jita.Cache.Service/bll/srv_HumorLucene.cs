using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Jita.Common;
using Jita.DAL;
using Jita.Data.Model;
using Jita.LuceneManger;

namespace Jita.Cache.Service.bll
{
    public class srv_HumorLucene
    {
        /// <summary>
        /// 添加多个帖子索引
        /// </summary>
        /// <param name="humorIds"></param>
        public static void AddLuceneHumorList(string humorIds)
        {
            DataTable dt = dal_HumorInfo.GetHumorInfoByIds(humorIds);
            List<T_Humor_HumorInfo> humors = com_ModelFillHelper.FillModelList<T_Humor_HumorInfo>(dt);
            HumorLucene.MultiInsertToIndex(humors);
        }
        /// <summary>
        /// 添加单个帖子索引
        /// </summary>
        /// <param name="id"></param>
        public static void AddLuceneHumor(int id)
        {
            DataTable dt = dal_HumorInfo.GetHumorInfoById(id);
            T_Humor_HumorInfo humor = com_ModelFillHelper.FillModel<T_Humor_HumorInfo>(dt);
            HumorLucene.InsertToIndex(humor);
        }
    }
}
