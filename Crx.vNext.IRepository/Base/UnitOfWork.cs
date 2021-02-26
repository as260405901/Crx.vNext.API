using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Crx.vNext.Common.Helper;

namespace Crx.vNext.IRepository.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbConnection DbConnection { get; private set; }

        public IDbTransaction DbTransaction { get; private set; }

        public UnitOfWork(IDbConnection connection)
        {
            DbConnection = connection;
        }

        public void BeginTransaction()
        {
            if (DbConnection.State == ConnectionState.Closed)
            {
                DbConnection.Open();
            }
            DbTransaction = DbConnection.BeginTransaction();
        }

        public void CommitTran()
        {
            DbTransaction.InvokeDispose(() => DbTransaction.Commit());
        }

        public void RollbackTran()
        {
            DbTransaction.InvokeDispose(() => DbTransaction.Rollback());
        }

        public void Dispose()
        {
            DbTransaction.InvokeDispose();
            DbConnection.InvokeDispose();
        }
    }
}
