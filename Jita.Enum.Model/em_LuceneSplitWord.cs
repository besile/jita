using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jita.Enum.Model
{
    public enum em_LuceneSplitWord
    {
        /// <summary>
        /// Field.Index.NOT_ANALYZED:指定不按照分词后的结果保存--是否按分词后结果保存取决于是否对该列内容进行模糊查询
        /// </summary>
        NOT_ANALYZED,
        /// <summary>
        /// Field.Index.ANALYZED:指定文章内容按照分词后结果保存 否则无法实现后续的模糊查询 
        /// </summary>
        ANALYZED
    }
}
