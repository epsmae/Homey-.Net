using System.Collections.Generic;
using System.IO;
using Homey.Net.Dtos;
using Homey.Net.Test.Infrastructure;
using NUnit.Framework;

namespace Homey.Net.Test
{
    public class ParserTest
    {
        private ResponseParser _parser;
        private TestDataContext _testData;

        [SetUp]
        public void Setup()
        {
            _testData = new TestDataContext();
            _parser = new ResponseParser();
        }

        [Test]
        public void TestParseZones()
        {
            string data = File.ReadAllText(_testData.GetZonesResponseFile);
            List<Zone> response = _parser.ParseZones(data);
            Assert.NotNull(response);
        }


        [Test]
        public void TestParseDevices()
        {
            string data = File.ReadAllText(_testData.GetDevicesResponseFile);
            List<Device> response = _parser.ParseDevices(data);
            Assert.NotNull(response);
        }

        [Test]
        public void TestParseFlows()
        {
            string data = File.ReadAllText(_testData.GetFlowsResponseFile);
            List<Flow> response = _parser.ParseFlows(data);
            Assert.NotNull(response);
        }

        [Test]
        public void TestParseCapatibilityReport()
        {
            string data = File.ReadAllText(_testData.GetCapatibilityReport);
            CapatibilityReport response = _parser.ParseCapatibilityReport(data);
            Assert.NotNull(response);
        }

        [Test]
        public void TestParseAlarms()
        {
            string data = File.ReadAllText(_testData.GetAlarmsResponse);
            IList<Alarm> response = _parser.ParseAlarms(data);
            Assert.NotNull(response);
        }

        [Test]
        public void TestParseSetOnOff()
        {
            string data = File.ReadAllText(_testData.SetOnOff);
            TransactionResponse response = _parser.ParseTransactionResponse(data);
            Assert.NotNull(response);
        }

        [Test]
        public void TestParseSystem()
        {
            string data = File.ReadAllText(_testData.GetSystemResponse);
            HomeySystem response = _parser.ParseSystem(data);
            Assert.NotNull(response);
        }
    }
}