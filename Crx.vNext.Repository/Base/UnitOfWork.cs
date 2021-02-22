using Crx.vNext.IRepository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NCR.Repository.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _conn;

        private IDbTransaction _tran;

        public UnitOfWork(IDbConnection connection)
        {
            _conn = connection;
        }

        public IDbConnection GetConnection()
        {
            return _conn;
        }

        public IDbTransaction GetTransaction()
        {
            return _tran;
        }

        public void BeginTransaction()
        {
            if (_conn.State == ConnectionState.Closed)
                _conn.Open();
            _tran = _conn.BeginTransaction();
        }

        public void CommitTran()
        {
            _tran?.Commit();
            _tran?.Dispose();
            _tran = null;
        }

        public void RollbackTran()
        {
            _tran?.Rollback();
            _tran?.Dispose();
            _tran = null;
        }

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}
