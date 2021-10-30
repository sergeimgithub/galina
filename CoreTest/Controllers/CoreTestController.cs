using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreTestCommon;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


namespace CoreTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoreTestController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] TestPayload data)
        {
            Console.WriteLine($"CoreTestController: PostAsync: {data}");
            await Task.Run(() => "dummy");
            return Ok("Just Hi");
        }
    }
}
