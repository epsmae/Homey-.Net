using System.Net;

namespace Homey.Net
{
    public class RestResponseResult
    {
        public HttpStatusCode Code { get; set; }
        public string Content { get; set; }
    }
}
