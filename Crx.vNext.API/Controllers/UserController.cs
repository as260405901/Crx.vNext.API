using Crx.vNext.Common.Base;
using Crx.vNext.IService;
using Crx.vNext.Model.InputModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
