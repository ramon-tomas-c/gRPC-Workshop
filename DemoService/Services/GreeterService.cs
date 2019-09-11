using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greet;
using Grpc.Core;

namespace DemoService
{
    public class GreeterService : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task SayHelloStream(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {            
            for(int i=0; i<=2;i++)
            {
                await responseStream.WriteAsync(new HelloReply()
                {
                    Message = "Hello " + request.Name + $"  Time Sent: {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}"
                });
                await Task.Delay(5000);
            }
        }
    }
}
