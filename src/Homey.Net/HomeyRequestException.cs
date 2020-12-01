using System;
using System.Net;

namespace Homey.Net
{
    public class HomeyRequestException : Exception
    {
        public HomeyRequestException(string message, Exception ex)
            :base(message, ex)
        {

        }

        public HomeyRequestException(string message)
            : base(message)
        {

        }

        public HttpStatusCode? ResponseCode { get; set; }

        public string Endpoint { get; set; }
    }
}
