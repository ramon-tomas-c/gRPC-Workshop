//using Greet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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

            return Ok();
        }        
    }
}
