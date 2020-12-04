using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Homey.Net.Dtos;

namespace Homey.Net
{
    public class HomeyClient
    {
        private readonly string _homeyIp;
        private readonly string _token;
        private readonly IHomeyRestClient _client;

        private readonly ResponseParser _responseParser;

        public HomeyClient(string homeyIp, string token)
            :this(new RestSharpClient(), homeyIp, token)
        {
        }

        public HomeyClient(IHomeyRestClient client, string homeyIp, string token)
        {
            _homeyIp = homeyIp;
            _token = token;
            _client = client;
            _responseParser = new ResponseParser();
        }

        /// <summary>
        /// Get all configured devices
        /// </summary>
        /// <exception cref="HomeyRequestException"></exception>
        /// <returns></returns>
        public async Task<List<Device>> GetDevices()
        {
            string endpoint = $"http://{_homeyIp}/api/manager/devices/device";
            
            return await RequestData(endpoint, 
                _client.RequestAsyncGet(endpoint, _token),
                _responseParser.ParseDevices);
        }

        /// <summary>
        /// Get all configured devices of a zone
        /// </summary>
        /// <param name="zoneId"></param>
        /// <exception cref="HomeyRequestException"></exception>
        /// <returns></returns>
        public async Task<List<Device>> GetDevices(string zoneId)
        {
            string endpoint = $"http://{_homeyIp}/api/manager/devices/device/?zone={zoneId}";
            
            return await RequestData(endpoint,
                _client.RequestAsyncGet(endpoint, _token),
                _responseParser.ParseDevices);
        }

        /// <summary>
        /// Get a specific device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <exception cref="HomeyRequestException"></exception>
        /// <returns></returns>
        public async Task<Device> GetDevice(string deviceId)
        {
            string endpoint = $"http://{_homeyIp}/api/manager/devices/device/{deviceId}";

            return await RequestData(endpoint,
                _client.RequestAsyncGet(endpoint, _token),
                _responseParser.ParseDevice);
        }

        /// <summary>
        /// Get a capability of a device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="capability"></param>
        /// <exception cref="HomeyRequestException"></exception>
        /// <returns></returns>
        public async Task<CapatibilityReport> GetCapability(string deviceId, string capability)
        {
            string endpoint = $"http://{_homeyIp}/api/manager/insights/log/homey:device:{deviceId}/{capability}/entry";

            return await RequestData(endpoint,
                _client.RequestAsyncGet(endpoint, _token),
                _responseParser.ParseCapatibilityReport);
        }

        /// <summary>
        /// Get all available zones
        /// </summary>
        /// <exception cref="HomeyRequestException"></exception>
        /// <returns></returns>
        public async Task<List<Zone>> GetZones()
        {
            string endpoint = $"http://{_homeyIp}/api/manager/zones/zone";

            return await RequestData(endpoint,
                _client.RequestAsyncGet(endpoint, _token),
                _responseParser.ParseZones);
        }

        /// <summary>
        /// Get all available flows
        /// </summary>
        /// <exception cref="HomeyRequestException"></exception>
        /// <returns></returns>
        public async Task<List<Flow>> GetFlows()
        {
            string endpoint = $"http://{_homeyIp}/api/manager/flow/flow";

            return await RequestData(endpoint,
                _client.RequestAsyncGet(endpoint, _token),
                _responseParser.ParseFlows);
        }

        /// <summary>
        /// Gets all the available alarms
        /// </summary>
        /// <returns></returns>
        public async Task<List<Alarm>> GetAlarms()
        {
            string endpoint = $"http://{_homeyIp}/api/manager/alarms/alarm";

            return await RequestData(endpoint,
                _client.RequestAsyncGet(endpoint, _token),
                _responseParser.ParseAlarms);
        }

        /// <summary>
        /// Gets the current system 
        /// </summary>
        /// <returns></returns>
        public async Task<HomeySystem> GetSystem()
        {
            string endpoint = $"http://{_homeyIp}/api/manager/system";

            return await RequestData(endpoint,
                _client.RequestAsyncGet(endpoint, _token),
                _responseParser.ParseSystem);
        }

        /// <summary>
        /// Set a boolean capability of a device 
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="capability"></param>
        /// <param name="value"></param>
        /// <exception cref="HomeyRequestException"></exception>
        /// <returns></returns>
        public async Task<TransactionResponse> SetBooleanCapability(string deviceId, string capability, bool value)
        {
            string endpoint = $"http://{_homeyIp}/api/manager/devices/device/{deviceId}/capability/{capability}";

            var body = new
            {
                value = value
            };

            return await RequestData(endpoint,
                _client.RequestAsyncPut(endpoint, body, _token),
                _responseParser.ParseTransactionResponse);
        }

        private async Task<T> RequestData<T>(string endpoint, Task<RestResponseResult> request, Func<string, T> parse)
        {
            try
            {
                RestResponseResult result = await request;

                EnsureStatusCodeOk(endpoint, result);

                return parse(result.Content);
            }
            catch (HomeyRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new HomeyRequestException("Failed to execute request", ex)
                {
                    Endpoint = endpoint
                };
            }
        }

        private static void EnsureStatusCodeOk(string endpoint, RestResponseResult result)
        {
            if (result.Code != HttpStatusCode.OK)
            {
                throw new HomeyRequestException("Unexpected result code")
                {
                    Endpoint = endpoint,
                    ResponseCode = result.Code
                };
            }
        }
    }
}
