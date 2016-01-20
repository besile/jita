using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jita.Common.Attr;

namespace Jita.Data.Model
{
    [Serializable]
    public class T_Humor_HumorInfo
    {
        [Attr_CachePrimaryKey]
        [Attr_MatchField("Id")]
        public int Id { get; set; }
        /// <summary>
        /// 帖子标题
        /// </summary>
        [Attr_MatchField("HumorTitle")]
        public string HumorTitle { get; set; }
        /// <summary>
        /// 帖子类型
        /// </summary>
        [Attr_MatchField("HumorType")]
        public int HumorType { get; set; }
        /// <summary>
        /// ubb内容
        /// </summary>
        [Attr_MatchField("HumorUbbContent")]
        public string HumorUbbContent { get; set; }
        /// <summary>
        /// 帖子内容
        /// </summary>
        [Attr_MatchField("HumorContent")]
        public string HumorContent { get; set; }
        /// <summary>
        /// 帖子创建人Id
        /// </summary>
        [Attr_MatchField("CreateUserId")]
        public int CreateUserId { get; set; }
        /// <summary>
        /// 帖子评论数
        /// </summary>
        [Attr_MatchField("CommentNum")]
        public int CommentNum { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Attr_MatchField("CreateUserName")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 帖子点击数
        /// </summary>
        [Attr_MatchField("HitNum")]
        public int HitNum { get; set; }
        /// <summary>
        /// 帖子支持数
        /// </summary>
        [Attr_MatchField("SupportNum")]
        public int SupportNum { get; set; }
        /// <summary>
        /// 反对数
        /// </summary>
        [Attr_MatchField("OpposeNum")]
        public int OpposeNum { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Attr_MatchField("IsDelete")]
        public int IsDelete { get; set; }
        /// <summary>
        /// IsTop是否是置顶
        /// </summary>
        [Attr_MatchField("IsTop")]
        public int IsTop { get; set; }
        /// <summary>
        /// ip地址
        /// </summary>
        [Attr_MatchField("IpAddress")]
        public string IpAddress { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [Attr_MatchField("DeleteTime")]
        public DateTime DeleteTime { get; set; }
        /// <summary>
        /// 帖子标签
        /// </summary>
        [Attr_MatchField("TagIds")]
        public string TagIds { get; set; }
        /// <summary>
        /// 帖子支持数
        /// </summary>
        [Attr_MatchField("ShowTime")]
        public DateTime ShowTime { get; set; }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        [Attr_MatchField("IsOuterUser")]
        public bool IsOuterUser { get; set; }
        /// <summary>
        /// 权重值
        /// </summary>
        [Attr_MatchField("Factor")]
        public float Factor { get; set; }
        /// <summary>
        /// 帖子是否显示
        /// </summary>
        [Attr_MatchField("IsShow")]
        public int IsShow { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Attr_MatchField("UpdateTime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 帖子是否审核
        /// </summary>
        [Attr_MatchField("IsAudit")]
        public int IsAudit { get; set; }
    }
}
