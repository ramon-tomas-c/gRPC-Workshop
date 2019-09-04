//using Greet;
using Greet;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
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
        public HelloGrpcController(IOptions<Settings> settings)
        {
            _serverUri = settings.Value.ServerUri;
        }

        [HttpGet]
        public async Task<IActionResult> CallHello(string name = "GreeterClient")
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_serverUri);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken.Get());

            var client = GrpcClient.Create<Greeter.GreeterClient>(httpClient);

            var reply = await client.SayHelloAsync(
                      new HelloRequest { Name = name });

            return Ok(reply);
        }        
    }
}
