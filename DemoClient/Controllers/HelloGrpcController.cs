//using Greet;
using Greet;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DemoClient
{
    [Route("helloGrpc")]
    [ApiController]
    public class HelloGrpcController : Controller
    {

        private readonly string _serverUri;
        private readonly ILogger<HelloGrpcController> _logger;

        public HelloGrpcController(
            IOptions<Settings> settings,
            ILogger<HelloGrpcController> logger)
        {
            _serverUri = settings.Value.ServerUri;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> CallHello(string name = "GreeterClient")
        {
            _logger.LogInformation("*************** Calling from Grpc Client **********************");
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_serverUri);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken.Get());

            var client = GrpcClient.Create<Greeter.GreeterClient>(httpClient);

            var reply = await client.SayHelloAsync(
                      new HelloRequest { Name = name });

            _logger.LogInformation("*************** Received message from Grpc Server **********************");

            return Ok(reply);
        }        
    }
}
