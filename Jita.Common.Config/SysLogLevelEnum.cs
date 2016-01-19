using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jita.Common.Config
{
    /// <summary>
    /// 日志级别枚举类型
    /// </summary>
    public enum SysLogLevelEnum
    {
        /// <summary> 
        /// 致命错误
        /// </summary>
        Fatal = 1,

        /// <summary> 
        /// 一般错误
        /// </summary>
        Error = 2,

        /// <summary> 
        /// 警告
        /// </summary>
        Warn = 3,

        /// <summary> 
        /// 一般信息
        /// </summary>
        Info = 4,

        /// <summary> 
        /// 调试信息
        /// </summary>
        Debug = 5
    }
}
