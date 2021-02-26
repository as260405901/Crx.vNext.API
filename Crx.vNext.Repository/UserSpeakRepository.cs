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
    public class UserSpeakRepository : IUserSpeakRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private IDbConnection _conn;
        public UserSpeakRepository(IUnitOfWork unitOfWork, IDbConnection dbConnection)
        {
            _unitOfWork = unitOfWork;
            _conn = dbConnection;
        }

        public IEnumerable<UserSpeakInfo> GetList()
        {
            return _unitOfWork.DbConnection.GetList<UserSpeakInfo>();
        }
    }
}
