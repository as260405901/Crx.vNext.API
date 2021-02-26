using Crx.vNext.IRepository;
using Crx.vNext.IService;
using Crx.vNext.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crx.vNext.Service
{
    public class UserSpeakService1 : IUserSpeakService
    {
        private IUserSpeakRepository _userSpeakRepository;
        public UserSpeakService1(IUserSpeakRepository userSpeakRepository)
        {
            _userSpeakRepository = userSpeakRepository;
        }
        public IEnumerable<UserSpeakInfo> GetList()
        {
            return _userSpeakRepository.GetList();
        }
    }
}
