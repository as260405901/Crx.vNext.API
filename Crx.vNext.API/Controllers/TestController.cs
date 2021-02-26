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
using StackExchange.Redis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crx.vNext.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TestController> _logger;
        private readonly IDatabase _redis;

        public TestController(ILogger<TestController> logger, IMapper mapper, IDatabase redis)
        {
            _logger = logger;
            _mapper = mapper;
            _redis = redis;
        }
        // GET: api/Test
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var key = "aaa";
            string aaa;            
            if (await _redis.KeyExistsAsync(key))
            {
                aaa = await _redis.StringGetAsync("aaa");
            }
            else
            {
                aaa = SnowflakeID.NextId().ToString();
                await _redis.StringSetAsync("aaa", aaa);
            }
            return Ok(aaa);
        }

        // GET api/Test/5
        [HttpGet("{id}")]
        public async Task<MessageResponse> Get(int id)
        {
            _logger.LogWarning(DateTime.Now + "中文测试");
            _logger.LogWarning(JsonHelper.Serialize(new { a = DateTime.Now, b = "中文测试" }));
            _logger.LogWarning(JsonHelper.Serialize(new { a = '啊', b = new[] { '是', '的' } }));
            _logger.LogWarning(JsonSerializer.Serialize(new { a = '啊', b = new[] { '是', '的' } }));
            _logger.LogWarning(JsonHelper.Serialize(MessageResponse.Ok(new { a = DateTime.Now, b = "中文测试" })));
            return MessageResponse.Ok(MessageResponse.Ok(new { a = DateTime.Now, b = "中文测试",id = SnowflakeID.NextId() }));
        }

        // POST api/Test
        [HttpPost]
        public async Task<string> Post(InputModel<int> model)
        {
            return _mapper.Map<InputModel<ApprovalStatusEnum>>(model).Data.GetDescription();
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
    }
}
