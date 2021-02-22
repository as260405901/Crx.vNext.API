using System.Collections.Generic;
using Crx.vNext.Model.DataModel;

namespace Crx.vNext.IRepository.Base
{
    // 仓储
    public interface IBaseRepository<TEntity> where TEntity : BaseInfo
    {

        int? Insert(TEntity model);

        int Delete(int id);

        int Delete(TEntity model);

        TEntity Get(object id);

        IEnumerable<TEntity> GetList(string conditions = null, object param = null);

        int Update(TEntity model);

        int Execute(string sql, object param = null);

        IEnumerable<TEntity> GetListPaged(int pageNumber, int rowsPerPage, string conditions = null, string orderby = null, object param = null);

        int RecordCount(string conditions = null, object param = null);

        IEnumerable<TEntity> Query(string sql, object param = null);

        IEnumerable<T> Query<T>(string sql, object param = null);

        TEntity QuerySingleOrDefault(string sql, object param = null);

        T QuerySingleOrDefault<T>(string sql, object param = null);

        /*
        TEntity QueryFirstOrDefault(string sql, object param = null);

        IEnumerable<TEntity> GetList(string conditions = null, object parameters = null);

        int? Insert(TEntity model);

        IEnumerable<TEntity> Query(string sql, object parameters = null);

        TReturn QuerySingleOrDefault<TReturn>(string sql, object parameters = null);
        */
    }
}
