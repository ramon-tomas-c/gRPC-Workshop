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
            var result = new List<string>();
            try
            {
                var metadata = new Metadata()
                {
                    { "Authorization", $"Bearer {JwtToken.Get()}"}
                };
                
                var channel = GrpcChannel.ForAddress(_serverUri);                
                var client = new Greeter.GreeterClient(channel);
                var call = client.SayHelloStream(new Greet.HelloRequest() { Name = name}, metadata);
                var responseStream = call.ResponseStream;
                while (await responseStream.MoveNext())
                {
                    result.Add(responseStream.Current.Message);
                }
            }
            catch (RpcException e)
            {
                Console.WriteLine("RPC failed " + e);
                throw;
            }

            return Ok(result);
        }
    }
}
