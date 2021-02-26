using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Crx.vNext.IRepository.Base
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork: IDisposable
    {
        IDbConnection DbConnection { get; }

        IDbTransaction DbTransaction { get; }

        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTran();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTran();
    }
}
