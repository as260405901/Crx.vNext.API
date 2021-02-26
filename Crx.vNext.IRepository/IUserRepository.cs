using Crx.vNext.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Crx.vNext.IRepository
{
    public interface IUserRepository
    {
        IEnumerable<UserInfo> GetList();
    }
}
