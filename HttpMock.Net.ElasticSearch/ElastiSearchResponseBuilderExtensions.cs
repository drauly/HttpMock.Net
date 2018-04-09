using System.Collections.Generic;
using System.Linq;

namespace HttpMock.Net.ElasticSearch
{
    public static class ElastiSearchResponseBuilderExtensions
    {
        public static HttpHandlerBuilder ReturnDocs(this HttpResponseBuilder builder, IEnumerable<object> documents)
        {
            var docs = documents.ToList();
            var response = new
            {
                hits = new
                {
                    hits = docs.Select(doc => new {_source = doc} ).ToList(),
                    total = docs.Count 
                }
            };
            return builder.Respond(response);
        }
    }
}