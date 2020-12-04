using System.Collections.Generic;
using System.Linq;
using Homey.Net.Dtos;
using Newtonsoft.Json;

namespace Homey.Net
{
    public class ResponseParser
    {
        public CapatibilityReport ParseCapatibilityReport(string source)
        {
            return JsonConvert.DeserializeObject<CapatibilityReport>(source);
        }

        public Device ParseDevice(string source)
        {
            return JsonConvert.DeserializeObject<Device>(source);
        }

        public List<Device> ParseDevices(string source)
        {
            Dictionary<string, Device> elements = JsonConvert.DeserializeObject<Dictionary<string, Device>>(source);
            return elements.Values.ToList();
        }

        public List<Zone> ParseZones(string source)
        {
            Dictionary<string, Zone> elements = JsonConvert.DeserializeObject<Dictionary<string, Zone>>(source);
            return elements.Values.ToList();
        }

        public List<Flow> ParseFlows(string source)
        {
            Dictionary<string, Flow> elements = JsonConvert.DeserializeObject<Dictionary<string, Flow>>(source);
            return elements.Values.ToList();
        }

        public List<Alarm> ParseAlarms(string source)
        {
            Dictionary<string, Alarm> elements = JsonConvert.DeserializeObject<Dictionary<string, Alarm>>(source);
            return elements.Values.ToList();
        }

        public HomeySystem ParseSystem(string source)
        {
            return JsonConvert.DeserializeObject<HomeySystem>(source);
        }

        public TransactionResponse ParseTransactionResponse(string source)
        {
            return JsonConvert.DeserializeObject<TransactionResponse>(source);
        }
    }
}
