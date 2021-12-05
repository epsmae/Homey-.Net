using Homey.Net.Test.Infrastructure;
using NUnit.Framework;

namespace Homey.Net.Test
{
    [TestFixture(Ignore = "Only for live testing")]
    public class IntegrationRequestTestTest : RequestTestBase
    {
        private const string BearerToken = "<TOKEN>";
        private const string HomeyIp = "192.168.99.99:8080";

        public override HomeyClient SetupClient()
        {
            HomeyClient client = new HomeyClient
            {
                Token = BearerToken,
                HomeyIp = HomeyIp
            };
            return client;
        }
    }
}
