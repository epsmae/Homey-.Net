using Newtonsoft.Json;

namespace Homey.Net.Auth
{
    internal class AuthResponse
    {

        [JsonProperty(PropertyName = "token")]
        public string Token
        {
            get; set;
        }
    }
}
