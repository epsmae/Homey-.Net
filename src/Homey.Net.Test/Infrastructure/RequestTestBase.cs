using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homey.Net.Dtos;
using NUnit.Framework;

namespace Homey.Net.Test.Infrastructure
{
    public abstract class RequestTestBase
    {
        private HomeyClient _client;

        [SetUp]
        public void Setup()
        {
            _client = SetupClient();
        }

        public abstract HomeyClient SetupClient();

        [Test]
        public async Task TestRequestDevices()
        {
            IList<Device> devices = await _client.GetDevices();
            Assert.NotNull(devices);
            Assert.Greater(devices.Count, 3);
            AssertDevice(devices.First());
        }

        [Test]
        public async Task TestRequestZoneDevices()
        {
            IList<Device> devices = await _client.GetDevices("9919ee1e-ffbc-480b-bc4b-77fb047e9e68");
            Assert.NotNull(devices);
            Assert.Greater(devices.Count, 3);
            AssertDevice(devices.First());
        }


        [Test]
        public async Task TestRequestDevice()
        {
            Device device = await _client.GetDevice("447f00f7-64e8-45b4-93c5-346455b2346b");
            AssertDevice(device);
        }
        
        [Test]
        public async Task TestRequestCapability()
        {
            CapatibilityReport report = await _client.GetCapability("447f00f7-64e8-45b4-93c5-346455b2346b", "measure_humidity");
            AssertReport(report);
        }

        [Test]
        public async Task TestRequestZones()
        {
            IList<Zone> zones = await _client.GetZones();
            Assert.NotNull(zones);
            Assert.Greater(zones.Count, 3);
            AssertZone(zones.First());
        }
        
        [Test]
        public async Task TestRequestFlows()
        {
            IList<Flow> flows = await _client.GetFlows();
            Assert.NotNull(flows);
            Assert.Greater(flows.Count, 3);
            AssertFlow(flows.First());
        }

        [Test]
        public async Task TestRequestFlow()
        {
            string flowId = "d812af08-e413-4b64-991c-7d81e3f35cb7";
            Flow flow = await _client.GetFlow(flowId);
            AssertFlow(flow);
        }

        [Test]
        public async Task TestRequestAlarms()
        {
            IList<Alarm> alarms = await _client.GetAlarms();
            Assert.NotNull(alarms);
            Assert.AreEqual(alarms.Count, 1);
            AssertAlarm(alarms.First());
        }

        [Test]
        public async Task TestRequestSystem()
        {
            HomeySystem system = await _client.GetSystem();
            Assert.NotNull(system);
            AssertSystem(system);
        }
        
        [Test]
        public async Task TestSetSwitchOn()
        {
            string id = "0e87a17f-5995-45ba-810d-37b1710acf46";

            TransactionResponse result = await _client.SetBooleanCapability(id, "onoff", true);
            
            Assert.NotNull(result);
            Assert.False(string.IsNullOrEmpty(result.TransactionId));
            Assert.False(string.IsNullOrEmpty(result.TransactionTime));
        }

        [Test]
        public async Task TestUpdateAlarm()
        {
            string alarmId = "db8f391f-38a6-4e42-81b5-1bf042c87254";
            DayTime time = new DayTime(10, 15);
            Repetition repetition = new Repetition {Friday = true, Monday = true};
            Alarm alarm = await _client.UpdateAlarm(alarmId, true, time, repetition);
            AssertAlarm(alarm);
        }

        [Test]
        public async Task TestEnableFlow()
        {
            string flowId = "d812af08-e413-4b64-991c-7d81e3f35cb7";
            Flow flow = await _client.EnableFlow(flowId, false);
            AssertFlow(flow);
        }


        [Test]
        public async Task TestTriggerFlow()
        {
            string flowId = "d812af08-e413-4b64-991c-7d81e3f35cb7";
            bool result = await _client.TriggerFlow(flowId);
            Assert.True(result);
        }

        private static void AssertDevice(Device device)
        {
            Assert.NotNull(device);
            Assert.False(string.IsNullOrEmpty(device.Id));
            Assert.False(string.IsNullOrEmpty(device.Name));
            Assert.NotNull(device.Capabilities);
            Assert.NotNull(device.CapabilitiesObj);
            Assert.NotNull(device.Data);
        }

        private void AssertReport(CapatibilityReport report)
        {
            Assert.NotNull(report);
            Assert.False(string.IsNullOrEmpty(report.Id));
            Assert.False(string.IsNullOrEmpty(report.Uri));
            Assert.Greater(report.Values.Count, 3);
        }

        private void AssertZone(Zone zone)
        {
            Assert.NotNull(zone);
            Assert.False(string.IsNullOrEmpty(zone.Name));
            Assert.False(string.IsNullOrEmpty(zone.Id));
        }

        private void AssertFlow(Flow flow)
        {
            Assert.NotNull(flow);
            Assert.False(string.IsNullOrEmpty(flow.Name));
            Assert.False(string.IsNullOrEmpty(flow.Id));
            Assert.NotNull(flow.Actions);
            Assert.NotNull(flow.Conditions);
        }

        private void AssertAlarm(Alarm alarm)
        {
            Assert.NotNull(alarm);
            Assert.False(string.IsNullOrEmpty(alarm.Id));
            Assert.False(string.IsNullOrEmpty(alarm.Name));
            Assert.False(string.IsNullOrEmpty(alarm.Time));
            Assert.True(alarm.NextOccurance > DateTime.MinValue);
            Assert.NotNull(alarm.Repetition);
        }

        private void AssertSystem(HomeySystem system)
        {
            Assert.False(string.IsNullOrEmpty(system.HomeyVersion));
            Assert.False(string.IsNullOrEmpty(system.HomeyModelId));
            Assert.False(string.IsNullOrEmpty(system.Hostname));
            Assert.False(string.IsNullOrEmpty(system.NodeVersion));
            Assert.False(string.IsNullOrEmpty(system.WifiAddress));
            Assert.False(string.IsNullOrEmpty(system.WifiSsid));
        }
    }
}