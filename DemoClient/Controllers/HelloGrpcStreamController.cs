//using Greet;
using Greet;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoClient
{
    [Route("helloGrpcStream")]
    [ApiController]
    public class HelloGrpcStreamController : Controller
    {

        private readonly string _serverUri;
        public HelloGrpcStreamController(IOptions<Settings> settings)
        {
            _serverUri = settings.Value.ServerUri;
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> CallHello(string name = "GreeterClient")
        {
            

            return Ok();
        }
    }
}
