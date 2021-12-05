using System.Threading.Tasks;
using Homey.Net.Auth;
using Homey.Net.Auth.Auth;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Homey.Net.Test
{
    internal class AuthTest
    {
        // Homey Login credentials
        private const string UserName = "<USER_NAME>";
        private const string Password = "<PASSWORD>";

        // client id and client secret from: https://tools.developer.homey.app/api/projects
        // cloud id from: https://tools.developer.homey.app/tools/system 
        private static HomeyApiConfig _apiConfig = new HomeyApiConfig
        {
            ClientId = "<CLIENT_ID>",
            ClientSecret = "<CLIENT_SECRET>",
            CloudId = "<CLOUD_ID>"
        };


        [Test]
        [Ignore("Only for live testing")]
        public async Task TestLogin()
        {
            Authentication a = new Authentication();
            await a.Login(_apiConfig, UserName, Password);
        }

        [Test]
        public void TestEncode()
        {
            string expected = "email=test%40gmail.com&password=test!&otptoken=";
            Authentication auth = new Authentication();
            string encoded = auth.EncodeUserNamePAssword("test@gmail.com", "test!");
            Assert.AreEqual(expected, encoded);
        }

        [Test]
        public void TestJsonParse()
        {
            string path = System.IO.Path.Combine(TestContext.CurrentContext.WorkDirectory, "token.json");

            string content = System.IO.File.ReadAllText(path);

            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(content);
            Assert.AreEqual("1234", tokenInfo.AccessToken);
            Assert.AreEqual("5678", tokenInfo.RefreshToken);
            Assert.AreEqual(3660, tokenInfo.ExpiresIn);
            Assert.AreEqual("bearer", tokenInfo.TokenType);
        }
    }
}
