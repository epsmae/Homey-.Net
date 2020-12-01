using System.Threading.Tasks;
using RestSharp;

namespace Homey.Net
{
    public class RestSharpClient : IHomeyRestClient
    {
        public Task<RestResponseResult> RequestAsyncGet(string endpoint, string bearerToken = null)
        {
            return RequestAsync(endpoint, null, Method.GET, bearerToken);
        }

        public Task<RestResponseResult> RequestAsyncPut(string endpoint, object body, string bearerToken = null)
        {
            return RequestAsync(endpoint, body, Method.PUT, bearerToken);
        }


        public async Task<RestResponseResult> RequestAsync(string endpoint, object body, Method method, string bearerToken = null)
        {

            IRestClient client = new RestClient(endpoint);

            if (!string.IsNullOrEmpty(bearerToken))
            {
                client.AddDefaultHeader("Authorization", $"Bearer {bearerToken}");
            }

            RestRequest restRequest = new RestRequest(method);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("Accept", "application/json");

            if (body != null)
            {
                restRequest.AddJsonBody(body);
            }

            IRestResponse response = await client.ExecuteAsync(restRequest);

            return new RestResponseResult
            {
                Code = response.StatusCode,
                Content = response.Content
            };
        }
    }
}
