using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace HttpMock.Net.ElasticSearch
{
    public static class ElastiSearchRequestHandlerBuilderExtensions
    {
        public static HttpResponseBuilder WhenSearch(this HttpHandlerBuilder builder, string indexPath = null, string queryContains = null)
        {
            bool Match(HttpContext context)
            {
                var path = (indexPath + "/_search").Replace("//", "/");
                if (!context.Request.Path.Equals(path, StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }

                if (queryContains == null)
                {
                    return true;
                }

                string body;
                context.Request.EnableRewind();
                using (var streamReader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    body = streamReader.ReadToEndAsync().Result;
                    context.Request.Body.Seek(0, SeekOrigin.Begin);
                }


                return body.ToLower().Contains(queryContains.ToLower());
            }

            return builder.WhenPost(Match);
        }
             
    }
}
