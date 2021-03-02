using Crx.vNext.IRepository;
using Crx.vNext.IRepository.Base;
using Crx.vNext.Model.DataModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Crx.vNext.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbConnection _conn;

        public UserRepository(IUnitOfWork unitOfWork, IDbConnection dbConnection)
        {
            _unitOfWork = unitOfWork;
            _conn = dbConnection;
        }

        public IEnumerable<UserInfo> GetList()
        {
            return _unitOfWork.DbConnection.GetList<UserInfo>();
        }

        public UserInfo GetModel(long id)
        {
            return _unitOfWork.DbConnection.Get<UserInfo>(id);
        }
    }
}
