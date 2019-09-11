using Greet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DemoClient.Controllers
{
    [Route("hello")]
    [ApiController]
    public class HelloController : Controller
    {

        private readonly string _proxyUri;
        private readonly ILogger<HelloGrpcController> _logger;

        public HelloController(
            IOptions<Settings> settings,
            ILogger<HelloGrpcController> logger)
        {
            _proxyUri = settings.Value.ProxyUri;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> CallHello(string name = "test")
        {
            _logger.LogInformation("*************** Calling from Http Client **********************");
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken.Get());

            var body = new StringContent(JsonConvert.SerializeObject(new HelloRequest { Name = "Ramon" }), System.Text.Encoding.UTF8, "application/json");
            var reply = await httpClient.PostAsync(_proxyUri, body);
            var content = await reply.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<HelloReply>(content);

            _logger.LogInformation("*************** Received messge from json-grpc transcoder proxy **********************");
            return Ok(response);
        }
    }
}
