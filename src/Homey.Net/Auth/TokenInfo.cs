using Newtonsoft.Json;

namespace Homey.Net.Auth.Auth
{
    public class TokenInfo
    {
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken
        {
            get; set;
        }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken
        {
            get; set;
        }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType
        {
            get; set;
        }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn
        {
            get; set;
        }
    }
}
