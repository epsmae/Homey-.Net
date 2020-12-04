using Homey.Net.Test.Infrastructure;

namespace Homey.Net.Test
{
    public class MockedClientRequestTest : RequestTestBase
    {
        public override HomeyClient SetupClient()
        {
            TestDataContext testData = new TestDataContext();
            MockedRestTestClient mock = new MockedRestTestClient();
            mock.AddResponseFile("/capability/", testData.SetOnOff);
            mock.AddResponseFile("/api/manager/flow/flow", testData.GetFlowsResponseFile);
            mock.AddResponseFile("/api/manager/zones/zone", testData.GetZonesResponseFile);
            mock.AddResponseFile("/api/manager/insights/log/homey:device:", testData.GetCapatibilityReport);
            mock.AddResponseFile("/api/manager/devices/device/?zone=", testData.GetDevicesResponseFile);
            mock.AddResponseFile("/api/manager/devices/device/", testData.GetDeviceResponseFile);
            mock.AddResponseFile("/api/manager/devices/device", testData.GetDevicesResponseFile);
            mock.AddResponseFile("/api/manager/alarms/alarm", testData.GetAlarmsResponse);

            return new HomeyClient(mock.Object, "192.168.99.99:80", "1234");
        }
    }
}
