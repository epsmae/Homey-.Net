using System;

namespace Homey.Net.Auth.Auth
{
    internal class AuthenticationException : Exception
    {
        public AuthenticationException(string message)
            : base($"{message}: Unexpected Status Code")
        {

        }
    }
}
