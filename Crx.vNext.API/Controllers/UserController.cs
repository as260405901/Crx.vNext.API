using Crx.vNext.Common.Base;
using Crx.vNext.Common.Helper;
using Crx.vNext.IService;
using Crx.vNext.Model.InputModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crx.vNext.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("GetList")]
        [HttpGet]
        public async Task<MessageResponse> GetList()
        {
            return MessageResponse.Ok(_userService.GetList());
        }

        [Route("GetModel")]
        [HttpPost]
        public async Task<MessageResponse> GetModel(InputModel<long> model)
        {
            return MessageResponse.Ok(_userService.GetModel(model.Data));
        }

        [Route("Redis")]
        [HttpGet]
        public async Task<MessageResponse> Redis()
        {
            var key = "aaa";
            string aaa = await RedisHelper.GetDatabase().StringGetAsync("aaa");
            if (aaa == null)
            {
                aaa = SnowflakeID.NextId().ToString();
                await RedisHelper.GetDatabase().StringSetAsync("aaa", aaa);
            }
            return MessageResponse.Ok(aaa);
        }
    }
}
