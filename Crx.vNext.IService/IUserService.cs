using Crx.vNext.Model.DataModel;
using Crx.vNext.Model.InputModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crx.vNext.IService
{
    public interface IUserService
    {
        IEnumerable<UserInfo> GetList();
        UserInfo GetModel(long id);
        UserInfo GetModel(InputModel<long> model);
        IEnumerable<UserSpeakInfo> GetSpeakList();
    }
}
