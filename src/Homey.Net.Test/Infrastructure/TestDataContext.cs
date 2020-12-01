using System.IO;
using NUnit.Framework;

namespace Homey.Net.Test.Infrastructure
{
    public class TestDataContext
    {
        public string TestDirectory
        {
            get
            {
                return Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData");
            }
        }


        public string GetDevicesResponseFile
        {
            get
            {
                return Path.Combine(TestDirectory, "GetDevicesResponse.json");
            }
        }

        public string GetDeviceResponseFile
        {
            get
            {
                return Path.Combine(TestDirectory, "GetDeviceResponse.json");
            }
        }

        public string GetZonesResponseFile
        {
            get
            {
                return Path.Combine(TestDirectory, "GetZonesResponse.json");
            }
        }

        public string GetFlowsResponseFile
        {
            get
            {
                return Path.Combine(TestDirectory, "GetFlowsResponse.json");
            }
        }

        public string GetCapatibilityReport
        {
            get
            {
                return Path.Combine(TestDirectory, "GetCapabilityReportLog.json");
            }
        }

        public string SetOnOff
        {
            get
            {
                return Path.Combine(TestDirectory, "SetOnOffResponse.json");
            }
        }
    }
}
