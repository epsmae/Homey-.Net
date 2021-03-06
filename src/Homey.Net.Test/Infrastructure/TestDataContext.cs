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

        public string TriggerFlowResponse
        {
            get
            {
                return Path.Combine(TestDirectory, "TriggerFlowResponse.json");
            }
        }

        public string GetFlowResponseFile
        {
            get
            {
                return Path.Combine(TestDirectory, "GetFlowResponse.json");
            }
        }

        public string GetCapatibilityReport
        {
            get
            {
                return Path.Combine(TestDirectory, "GetCapabilityReportLog.json");
            }
        }

        public string GetAlarmsResponse
        {
            get
            {
                return Path.Combine(TestDirectory, "GetAlarmsResponse.json");
            }
        }

        public string GetAlarmResponse
        {
            get
            {
                return Path.Combine(TestDirectory, "GetAlarmResponse.json");
            }
        }

        public string SetOnOff
        {
            get
            {
                return Path.Combine(TestDirectory, "SetOnOffResponse.json");
            }
        }

        public string GetSystemResponse
        {
            get
            {
                return Path.Combine(TestDirectory, "GetSystemResponse.json");
            }
        }
    }
}
