using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Crx.vNext.Model.Enum
{
    /// <summary>
    /// 审核状态
    /// </summary>
    public enum ApprovalStatusEnum
    {
        /// <summary>
        /// 未审核
        /// </summary>
        [Description("未审核")]
        None = 0,
        /// <summary>
        /// 通过
        /// </summary>
        [Description("通过")]
        Success,
        /// <summary>
        /// 未通过
        /// </summary>
        [Description("未通过")]
        Failurem,
        /// <summary>
        /// 审核中
        /// </summary>
        [Description("审核中")]
        Approvaling
    }
}
