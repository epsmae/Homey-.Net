using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Homey.Net.Auth.Auth;
using Newtonsoft.Json;

namespace Homey.Net.Auth
{
    /// <summary>
    /// Adaption from https://homey.solweb.no/advanced-api-usage/bearertoken
    /// </summary>
    public class Authentication
    {
        private const string RedirectUrl = "http://localhost";
        private const string LoginUrl = "https://accounts.athom.com/login";
        private const string OAuthTokenEndpoint = "https://api.athom.com/oauth2/token";
        private string TokenEndpoint = "https://api.athom.com/delegation/token?audience=homey";

        public async Task<string> Login(HomeyApiConfig config, string userName, string password)
        {
            AuthResponse responseStep1 = await AuthenticateStep1(userName, password);

            CsrfResponse csrfResponse = await AuthenticateStep2(config, responseStep1.Token);

            string code = await AuthenticateStep3(config, responseStep1.Token, csrfResponse);

            TokenInfo tokenInfo = await AuthenticateStep4(config, code);

            // Get JWT Token
            string accessToken = await AuthenticateStep5(config, tokenInfo);

            // get bearer Token
            return await AuthenticateStep6(config, accessToken);
        }


        private string GetCsrf(string src, string[] strf, string strt)
        {
            string[] array = src.Split(strf, StringSplitOptions.RemoveEmptyEntries);

            return array[array.Length - 1].Split(strt.ToCharArray())[0].Trim();

        }

        internal string EncodeUserNamePAssword(string mail, string password)
        {
            return $"email={Encode(mail)}&password={Encode(password)}&otptoken=";
        }

        private string Encode(string str)
        {
            return WebUtility.UrlEncode(str);
        }


        private async Task<AuthResponse> AuthenticateStep1(string userName, string password)
        {
            string encodedContent = EncodeUserNamePAssword(userName, password);
            HttpContent content = new StringContent(encodedContent, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(HttpRequestHeader.Accept.ToString(), "application/json, text/javascript, */*; q=0.01");
                client.DefaultRequestHeaders.Add(HttpRequestHeader.ContentType.ToString(), "application/x-www-form-urlencoded; charset=UTF-8");
                HttpResponseMessage response = await client.PostAsync(LoginUrl, content);
                EnsureStatusCodeOk("Step 1", response);
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AuthResponse>(responseBody);
            }
        }

        private async Task<CsrfResponse> AuthenticateStep2(HomeyApiConfig config, string token)
        {
            string authUrl = $"https://accounts.athom.com/oauth2/authorise?client_id={config.ClientId}&redirect_uri={Encode(RedirectUrl)}&response_type=code&user_token={token}";

            CsrfResponse csrfResponse = new CsrfResponse();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(authUrl);

                EnsureStatusCodeOk("Step 2", response);
                string responseBody = await response.Content.ReadAsStringAsync();

                csrfResponse.Csrf = GetCsrf(responseBody, new string[] { "name=\"_csrf\" value=\"" }, "\">");
                IEnumerable<string> cockieContent = response.Headers.GetValues("set-cookie");


                foreach (string entry in cockieContent)
                {
                    foreach (string cookie in entry.Split(';'))
                    {
                        string[] dc = cookie.Split('=');
                        if (dc[0] == "_csrf")
                        {
                            csrfResponse.Cookie = dc[1];
                        }
                    }
                }

                return csrfResponse;
            }
        }

        private async Task<string> AuthenticateStep3(HomeyApiConfig config, string token, CsrfResponse csrf)
        {
            string authorizeUrl = $"https://accounts.athom.com/authorise?client_id={config.ClientId}&redirect_uri={Encode(RedirectUrl)}&response_type=code&user_token={token}";

            string contentString = $"resource=resource.homey.{config.CloudId}&_csrf={csrf.Csrf}&allow=Allow";
            string cookie = $"_csrf={csrf.Cookie}";
            HttpContent content = new StringContent(contentString, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(HttpRequestHeader.ContentType.ToString(), "application/x-www-form-urlencoded");
                client.DefaultRequestHeaders.Add("Cookie", cookie);
                HttpResponseMessage response = await client.PostAsync(authorizeUrl, content);
                EnsureStatusCodeOk("Step 3", response, HttpStatusCode.Found);

                return response.Headers.GetValues("Location").First().Split('=')[1];
            }
        }

        private async Task<TokenInfo> AuthenticateStep4(HomeyApiConfig config, string code)
        {
            string url = $"client_id={Encode(config.ClientId)}&client_secret={Encode(config.ClientSecret)}&grant_type=authorization_code&code={Encode(code)}";
            HttpContent content = new StringContent(url, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(HttpRequestHeader.ContentType.ToString(), "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.PostAsync(OAuthTokenEndpoint, content);
                EnsureStatusCodeOk("Step 4", response);
                string responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TokenInfo>(responseString);
            }
        }

        private async Task<string> AuthenticateStep5(HomeyApiConfig config, TokenInfo token)
        {
            string stringContent = $"client_id={config.ClientId}&client_secret={config.ClientSecret}&grant_type=refresh_token&refresh_token={token.RefreshToken}&access_token={token.AccessToken}";
            HttpContent content = new StringContent(stringContent, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(HttpRequestHeader.ContentType.ToString(), "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.PostAsync(TokenEndpoint, content);
                EnsureStatusCodeOk("Step 5", response);
                return (await response.Content.ReadAsStringAsync()).Replace("\"", string.Empty);
            }
        }

        private async Task<string> AuthenticateStep6(HomeyApiConfig config, string token)
        {
            string endpoint = $"https://{config.CloudId}.connect.athom.com/api/manager/users/login";
            string serializedToken = JsonConvert.SerializeObject(new AuthResponse() { Token = token });
            HttpContent content = new StringContent(serializedToken, System.Text.Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(HttpRequestHeader.Accept.ToString(), "application/json");
                HttpResponseMessage response = await client.PostAsync(endpoint, content);
                EnsureStatusCodeOk("Step 5", response);
                return (await response.Content.ReadAsStringAsync()).Replace("\"", string.Empty);
            }
        }

        private void EnsureStatusCodeOk(string message, HttpResponseMessage res, HttpStatusCode code = HttpStatusCode.OK)
        {
            if (res.StatusCode != code)
            {
                throw new AuthenticationException(message);
            }
        }
    }
}
