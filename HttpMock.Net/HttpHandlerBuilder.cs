using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace HttpMock.Net
{
    public class HttpHandlerBuilder : IDisposable
    {
        private IDisposable _server;

        
        private readonly ICollection<HttpContext> _receivedRequests = new List<HttpContext>();

        public IDictionary<Func<HttpContext, bool>, Action<HttpContext>> RequestHandlers { get; } = new Dictionary<Func<HttpContext, bool>, Action<HttpContext>>();

        public HttpResponseBuilder When(Func<HttpContext, bool> matcher)
        {
            return new HttpResponseBuilder(this, matcher);
        }

        internal void SetServer(IDisposable server)
        {
            _server = server;
        }

        internal void AddReceivedRequest(HttpContext context)
        {
            _receivedRequests.Add(context);
        }

        public void Clear()
        {
            RequestHandlers.Clear();
            _receivedRequests.Clear();
        }

        public void Received(int nb, Func<HttpContext, bool> matcher)
        {
            var count = _receivedRequests.Count(matcher);
            if (count != nb)
            {
                var nonMatchingCount = _receivedRequests.Count(ctx => !matcher(ctx));
                throw new AssertionException($"expected to received {nb} matching requests, " +
                                             $"actually received {count} matching requests " +
                                             $"and {nonMatchingCount} non matching requests");
            }
        }

        public void Dispose()
        {
            _server?.Dispose();
        }
    }
}