using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace HttpMock.Net.ElasticSearch
{
    public static class ElastiSearchResponseBuilderExtensions
    {
        public static HttpHandlerBuilder ReturnDocs(this HttpResponseBuilder builder, IEnumerable<object> documents)
        {
            var response = new
            {
                hits = new { hits = documents.Select(doc => new {_source = doc} ).ToList(),
                total = documents.Count()}
            };
            return builder.Respond(response);
        }
    }
}