using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace HttpMock.Net
{
    public static class BasicHttpHandlerBuilderExtensions
    {
        public static HttpResponseBuilder WhenGet(this HttpHandlerBuilder builder, string path = null)
        {
            bool Match(HttpContext context)
            {
                if (path != null &&
                    !context.Request.Path.Equals(path, StringComparison.InvariantCultureIgnoreCase))
                {
                     return false;
                }
                return context.Request.Method.Equals(HttpMethod.Get.ToString());
            }

            return builder.When(Match);
        }

        public static HttpResponseBuilder WhenPost(this HttpHandlerBuilder builder, Func<HttpContext, bool> matcher)
        {
            return builder.When(context => context.Request.Method.Equals(HttpMethod.Post.ToString()) && matcher(context));
        }
    }
}