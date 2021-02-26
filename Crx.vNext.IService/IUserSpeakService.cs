using Crx.vNext.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crx.vNext.IService
{
    public interface IUserSpeakService
    {
        IEnumerable<UserSpeakInfo> GetList();
    }
}
