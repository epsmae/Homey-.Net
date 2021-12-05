using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Homey.Net.Auth;
using Homey.Net.Dtos;

namespace Homey.Net
{
    public class HomeyClient
    {
        private string _homeyIp;
        private string _token;
        private readonly IHomeyRestClient _client;

        private readonly ResponseParser _responseParser;
        private readonly Authentication _authentication;

        public HomeyClient()
            :this(new RestSharpClient())
        {
        }

        public HomeyClient(IHomeyRestClient client)
        {
            _homeyIp = string.Empty;
            _token = string.Empty;
            _client = client;
            _responseParser = new ResponseParser();
            _authentication = new Authentication();
        }

        private string BaseUrl
        {
            get
            {
                return $"http://{_homeyIp}/api/manager/";
            }
        }

        /// <summary>
        /// Bearer token to aceess the homey
        /// </summary>
        public string Token
        {
            get
            {
                return _token;
            }

            set
            {
                _token = value;
            }
        }

        /// <summary>
        /// Ip Address of the homey
        /// </summary>
        public string HomeyIp
        {
            get
            {
                return _homeyIp;
            }

            set
            {
                _homeyIp = value;
            }
        }

        /// <summary>
        /// Authenticates to get the access token
        /// This also interally sets the authentication token
        /// </summary>
        /// <param name="config"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> Authenticate(HomeyApiConfig config, string username, string password)
        {
            _token = await _authentication.Login(config, username, password);

            return _token;
        }


        /// <summary>
        /// Get all configured devices
        /// </summary>
        /// <exception cref="HomeyRequestException"></exception>
        /// <returns></returns>
        public async Task<List<Device>> GetDevices()
        {
            string endpoint = $"{BaseUrl}devices/device";
            
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
            string endpoint = $"{BaseUrl}devices/device/?zone={zoneId}";
            
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
            string endpoint = $"{BaseUrl}devices/device/{deviceId}";

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
            string endpoint = $"{BaseUrl}insights/log/homey:device:{deviceId}/{capability}/entry";

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
            string endpoint = $"{BaseUrl}zones/zone";

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
            string endpoint = $"{BaseUrl}flow/flow";

            return await RequestData(endpoint,
                _client.RequestAsyncGet(endpoint, _token),
                _responseParser.ParseFlows);
        }

        /// <summary>
        /// Get a specific flow
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public async Task<Flow> GetFlow(string flowId)
        {
            string endpoint = $"{BaseUrl}flow/flow/{flowId}";

            return await RequestData(endpoint,
                _client.RequestAsyncGet(endpoint, _token),
                _responseParser.ParseFlow);
        }


        /// <summary>
        /// Gets all the available alarms
        /// </summary>
        /// <returns></returns>
        public async Task<List<Alarm>> GetAlarms()
        {
            string endpoint = $"{BaseUrl}alarms/alarm";

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
            string endpoint = $"{BaseUrl}system";

            return await RequestData(endpoint,
                _client.RequestAsyncGet(endpoint, _token),
                _responseParser.ParseSystem);
        }

        /// <summary>
        /// Update an existing alarm
        /// </summary>
        /// <param name="alarmId"></param>
        /// <param name="enabled"></param>
        /// <param name="time"></param>
        /// <param name="repetition"></param>
        /// <returns>The updated alarm</returns>
        public async Task<Alarm> UpdateAlarm(string alarmId, bool enabled, DayTime time, Repetition repetition)
        {
            string endpoint = $"{BaseUrl}alarms/alarm/{alarmId}";
            
            AlarmUpdate update = new AlarmUpdate
            {
                Repetition = repetition,
                Enabled = enabled,
                Time = time.ToString()
            };

            return await RequestData(endpoint,
                _client.RequestAsyncPut(endpoint, update, _token),
                _responseParser.ParseAlarm);
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
            string endpoint = $"{BaseUrl}devices/device/{deviceId}/capability/{capability}";

            var body = new
            {
                value = value
            };

            return await RequestData(endpoint,
                _client.RequestAsyncPut(endpoint, body, _token),
                _responseParser.ParseTransactionResponse);
        }


        /// <summary>
        /// Enabled or disable a flow
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="enabled"></param>
        /// <returns>The updated flow</returns>
        public async Task<Flow> EnableFlow(string flowId, bool enabled)
        {
            string endpoint = $"{BaseUrl}flow/flow/{flowId}";

            var body = new
            {
                enabled = enabled
            };

            return await RequestData(endpoint,
                _client.RequestAsyncPut(endpoint, body, _token),
                _responseParser.ParseFlow);
        }

        /// <summary>
        /// trigger a specific flow
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public async Task<bool> TriggerFlow(string flowId)
        {
            string endpoint = $"{BaseUrl}flow/flow/{flowId}/trigger";

            return await RequestData(endpoint,
                _client.RequestAsyncPost(endpoint, null, _token),
                _responseParser.ParseTriggerFlowResponse);
        }

        private async Task<T> RequestData<T>(string endpoint, Task<RestResponseResult> request, Func<string, T> parse)
        {
            try
            {
                EnsureAccessInformationSet();

                RestResponseResult result = await request;

                EnsureStatusCodeOk(endpoint, result);

                return parse(result.Content);
            }
            catch (HomeyAccessException)
            {
                throw;
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

        private void EnsureAccessInformationSet()
        {

            if (string.IsNullOrEmpty(_token))
            {
                throw new HomeyAccessException("Access token not set either set the AccessToken or use Authenticate to fetch it");
            }

            if (string.IsNullOrEmpty(_homeyIp))
            {
                throw new HomeyAccessException("IP Address of the homey must be set");
            }

        } 
    }
}
