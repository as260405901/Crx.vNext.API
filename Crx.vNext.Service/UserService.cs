using Crx.vNext.IRepository;
using Crx.vNext.IService;
using Crx.vNext.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crx.vNext.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IUserSpeakRepository _userSpeakRepository;
        public UserService(IUserRepository userRepository, IUserSpeakRepository userSpeakRepository)
        {
            _userRepository = userRepository;
            _userSpeakRepository = userSpeakRepository;
        }

        public IEnumerable<UserInfo> GetList()
        {
            _userSpeakRepository.GetList();
            return _userRepository.GetList();
        }

        public IEnumerable<UserSpeakInfo> GetSpeakList()
        {
            return _userSpeakRepository.GetList();
        }
    }
}
