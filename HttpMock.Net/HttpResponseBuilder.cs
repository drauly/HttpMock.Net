using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace HttpMock.Net
{
    public class HttpResponseBuilder
    {
        private readonly HttpHandlerBuilder _httpHandlerBuilder;
        private readonly Func<HttpContext, bool> _requestMatcher;

        internal HttpResponseBuilder(HttpHandlerBuilder httpHandlerBuilder, Func<HttpContext, bool> requestMatcher)
        {
            _httpHandlerBuilder = httpHandlerBuilder;
            _requestMatcher = requestMatcher;
        }

        public HttpHandlerBuilder Do(Action<HttpContext> handler)
        {
            _httpHandlerBuilder.RequestHandlers.Add(_requestMatcher, handler);
            return _httpHandlerBuilder;
        }
    }
}