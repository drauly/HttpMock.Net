using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HttpMock.Net
{
    public static class BasicResponseBuilderExtensions
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = 
            new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};

        public static HttpHandlerBuilder Respond(this HttpResponseBuilder builder, object response)
        {
            var strResponse = JsonConvert.SerializeObject(response, JsonSerializerSettings);
            return builder.Respond(strResponse);
        }

        public static HttpHandlerBuilder Respond(this HttpResponseBuilder builder, string response)
        {
            return builder.Do(c => c.Response.WriteAsync(response).Wait());
        }
    }
}