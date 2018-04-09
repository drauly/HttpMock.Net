using System;
using System.IO;
using Microsoft.AspNetCore.Http;

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
                using (var streamReader = new StreamReader(context.Request.Body))
                {
                    body = streamReader.ReadToEndAsync().Result;
                }


                return body.ToLower().Contains(queryContains.ToLower());
            }

            return builder.WhenPost(Match);
        }
             
    }
}
