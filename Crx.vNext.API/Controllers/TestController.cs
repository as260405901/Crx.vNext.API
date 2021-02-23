using Crx.vNext.Framework;
using Crx.vNext.Model.InputModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crx.vNext.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET: api/Test
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var a = 1;
            return new BadRequestResult();
        }

        // GET api/Test/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return MsgResponse.Ok(DateTime.Now);
        }

        // POST api/Test
        [HttpPost]
        public async Task<string> Post(InputModel<string> model)
        {
            return model.Data;
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
