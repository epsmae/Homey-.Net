using System.Threading.Tasks;

namespace Homey.Net
{
    public interface IHomeyRestClient
    {
        Task<RestResponseResult> RequestAsyncGet(string endpoint, string bearerToken = null);

        Task<RestResponseResult> RequestAsyncPut(string endpoint, object body, string bearerToken = null);
    }
}
