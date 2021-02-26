using Dapper;
using System;

namespace Crx.vNext.Model.DataModel
{
    /// <summary>
    /// 基本数据实体，建表必备字段
    /// 规范（不这样干的，业务扩展时早晚后悔）
    /// </summary>
    public class BaseInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [IgnoreUpdate]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [IgnoreUpdate]
        public long CreateUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public long ModifiedUserId { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
