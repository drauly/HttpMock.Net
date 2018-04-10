using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HttpMock.Net
{
    public class Server
    {
        public static HttpHandlerBuilder Start(int port, Action<ILoggingBuilder> loggingBuilder = null)
        {
            var handlerBuilder = new HttpHandlerBuilder();

            var webHostBuilder = new WebHostBuilder()
                .UseKestrel(k => k.Listen(IPAddress.Any, port))
                .Configure(app => Configure(app, handlerBuilder));

            if (loggingBuilder != null)
            {
                webHostBuilder.ConfigureLogging(loggingBuilder);
            }

            var webHost = webHostBuilder.Build();

            handlerBuilder.SetServer(webHost);
            webHost.Start();

            return handlerBuilder;
        }

        private static void Configure(IApplicationBuilder app, HttpHandlerBuilder requestHandlerBuilder)
        {
            app.Run(async context =>
            {
                requestHandlerBuilder.AddReceivedRequest(context);

                var handler = requestHandlerBuilder.RequestHandlers.FirstOrDefault(rm => rm.Key(context));

                if (!handler.Equals(default(KeyValuePair<Func<HttpContext, bool>, Action<HttpContext>>)))
                {
                    handler.Value(context);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("no handler found for this request");
                }
            });
        }
    }
}
