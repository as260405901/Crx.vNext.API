using AutoMapper;
using Crx.vNext.Framework;
using Crx.vNext.Model.Enum;
using Crx.vNext.Common.Helper;
using Crx.vNext.Model.InputModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crx.vNext.Common.Base;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Crx.vNext.IService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crx.vNext.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TestController> _logger;
        private readonly IUserService _userService;

        public TestController(ILogger<TestController> logger, IMapper mapper, IUserService userService)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
        }

        // Get api/Test/GetSnowflakeID
        [Route("GetSnowflakeID")]
        [HttpGet]
        public async Task<MessageResponse> GetSnowflakeID()
        {
            return MessageResponse.Ok(SnowflakeID.NextId());
        }


        // PUT api/Test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Test/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Route("Encoder")]
        [HttpGet()]
        public async Task<MessageResponse> Encoder()
        {
            _logger.LogWarning(DateTime.Now + "中文测试");
            _logger.LogWarning(JsonHelper.Serialize(new { a = DateTime.Now, b = "中文测试" }));
            _logger.LogWarning(JsonHelper.Serialize(new { a = '啊', b = new[] { '是', '的' } }));
            _logger.LogWarning(JsonSerializer.Serialize(new { a = '啊', b = new[] { '是', '的' } }));
            _logger.LogWarning(JsonHelper.Serialize(MessageResponse.Ok(new { a = DateTime.Now, b = "中文测试" })));
            return MessageResponse.Ok(MessageResponse.Ok(new { a = DateTime.Now, b = "中文测试", id = SnowflakeID.NextId() }));
        }

        [Route("AutoMapper")]
        [HttpPost]
        public async Task<MessageResponse> AutoMapper(InputModel<int> model)
        {
            return MessageResponse.Ok(_mapper.Map<InputModel<ApprovalStatusEnum>>(model).Data.GetDescription());
        }
    }
}
