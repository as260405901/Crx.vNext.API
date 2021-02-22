using Crx.vNext.IRepository.Base;
using Crx.vNext.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;

namespace Crx.vNext.Repository.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseInfo
    {
        private readonly IUnitOfWork _unitOfWork;
        private IDbConnection conn { get => _unitOfWork.GetConnection(); }

        private IDbTransaction tran { get => _unitOfWork.GetTransaction(); }

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int? Insert(TEntity model)
        {
            return conn.Insert(model, tran);
        }

        public int Delete(int id)
        {
            return conn.Delete<TEntity>(id, tran);
        }

        public int Delete(TEntity model)
        {
            return conn.Delete(model, tran);
        }

        public TEntity Get(object id)
        {
            return conn.Get<TEntity>(id, tran);
        }

        public IEnumerable<TEntity> GetList(string conditions = null, object param = null)
        {
            return conn.GetList<TEntity>(conditions, param, tran);
        }

        public int Update(TEntity model)
        {
            return conn.Update(model, tran);
        }

        public IEnumerable<TEntity> GetListPaged(int pageNumber, int rowsPerPage, string conditions = null, string orderby = null, object param = null)
        {
            return conn.GetListPaged<TEntity>(pageNumber, rowsPerPage, conditions, orderby, param, tran);
        }

        public int RecordCount(string conditions = null, object param = null)
        {
            return conn.RecordCount<TEntity>(conditions, param, tran);
        }

        public IEnumerable<TEntity> Query(string sql, object param = null)
        {
            return conn.Query<TEntity>(sql, param, tran);
        }

        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            return conn.Query<T>(sql, param, tran);
        }

        public TEntity QuerySingleOrDefault(string sql, object param = null)
        {
            return conn.QuerySingleOrDefault<TEntity>(sql, param, tran);
        }

        public T QuerySingleOrDefault<T>(string sql, object param = null)
        {
            return conn.QuerySingleOrDefault<T>(sql, param, tran);
        }

        public int Execute(string sql, object param = null)
        {
            return conn.Execute(sql, param, tran);
        }

        /*

        public TEntity QueryFirstOrDefault(string sql, object param = null)
        {
            return conn.QueryFirstOrDefault<TEntity>(sql, param);
        }

        public IEnumerable<TEntity> GetList(string conditions = null, object parameters = null)
        {
            return conn.GetList<TEntity>(conditions, parameters);
        }

        public int? Insert(TEntity model)
        {
            return conn.Insert(model);
        }

        public IEnumerable<TEntity> Query(string sql, object parameters = null)
        {
            return conn.Query<TEntity>(sql, parameters);
        }

        public TReturn QuerySingleOrDefault<TReturn>(string sql, object parameters = null)
        {
            return conn.QuerySingleOrDefault<TReturn>(sql, parameters);
        }
        */
    }
}
