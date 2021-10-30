using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreTestCommon;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;


namespace CoreTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoreTestController : ControllerBase
    {
        private PositionOptions positionOptions;
        public CoreTestController(IOptions<PositionOptions> options)
        {
            // Get configuration way 3
            var positionOptions = options.Value;
            Console.WriteLine($"Title: {positionOptions.Title}");
            Console.WriteLine($"Name: {positionOptions.Name}");
            this.positionOptions = positionOptions;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] TestPayload data)
        {
            Console.WriteLine($"CoreTestController: PostAsync: {data}");
            await Task.Run(() => "dummy");
            return Ok("Just Hi");
        }
    }
}
