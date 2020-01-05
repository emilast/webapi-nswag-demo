using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiClientTestApp.Client.ApiHelpers
{
    /// <summary>
    /// Base class for auto-generated API clients. Responsible for making configuration and
    /// providing authentication for outgoing requests.
    /// </summary>
    // Credits: https://stackoverflow.com/a/49801655/736684
    public abstract class ApiClientBase
    {
        protected readonly ApiConfiguration Configuration;

        protected ApiClientBase(ApiConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Overridden by api client partial classes to set the base api url. 
        protected abstract Uri GetBaseAddress();

        // Used if "/UseHttpClientCreationMethod:true /InjectHttpClient:false" when running nswag to generate api clients.
        protected async Task<HttpClient> CreateHttpClientAsync(CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient {BaseAddress = GetBaseAddress()};
            return await Task.FromResult(httpClient);
        }

        // Used if "/UseHttpRequestMessageCreationMethod:true"
        // Called by implementing swagger client classes
        //protected Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        //{
        //    var msg = new HttpRequestMessage();

        //    // Add authentication to outgoing request
        //    var token = ApiClientContext.GetToken();
        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        msg.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //    }

        //    return Task.FromResult(msg);
        //}
    }
}
