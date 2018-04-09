using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace HttpMock.Net
{
    public class HttpHandlerBuilder : IDisposable
    {
        private IDisposable _server;

        public IDictionary<Func<HttpContext, bool>, Action<HttpContext>> RequestHandlers { get; } = new Dictionary<Func<HttpContext, bool>, Action<HttpContext>>();

        public HttpResponseBuilder When(Func<HttpContext, bool> matcher)
        {
            return new HttpResponseBuilder(this, matcher);
        }

        internal void SetServer(IDisposable server)
        {
            _server = server;
        }

        public void Clear()
        {
            RequestHandlers.Clear();
        }

        public void Dispose()
        {
            _server?.Dispose();
        }
    }
}