using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homey.Net.Dtos;

namespace Homey.Net.Sample.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _accessToken;
        private string _ipAddress;
        private string _userName;
        private string _password;
        private string _selectedDeviceId;
        private string _selectedFlowId;
        private string _selectedDeviceCaps;
        private string _selectedAlarmId;
        private string _selectedCap;
        private string _selectedValue;
        private string _requestStatus;
        private string _homeyVersion;
        private string _homeyModel;

        private bool _loginEnabled;
        private bool _requestEnabled;
        private bool _requestInProgress;

        private readonly ObservableCollection<Info> _devices;
        private readonly ObservableCollection<Info> _alarms;
        private readonly ObservableCollection<Info> _flows;
        private readonly ObservableCollection<Info> _zones;
        private readonly ObservableCollection<Info> _report;

        private HomeyClient _client;

        // client id and client secret from: https://tools.developer.homey.app/api/projects
        // cloud id from: https://tools.developer.homey.app/tools/system 
        private static HomeyApiConfig _apiConfig = new HomeyApiConfig
        {
            ClientId = "<CLIENT_ID>",
            ClientSecret = "<CLIENT_SECRET>",
            CloudId = "<CLOUD_ID>"
        };

        public MainViewModel()
        {
            _accessToken = string.Empty;
            _ipAddress = string.Empty;
            _userName = string.Empty;
            _password = string.Empty;

            _requestEnabled = false;
            _loginEnabled = true;
            _devices = new ObservableCollection<Info>();
            _alarms = new ObservableCollection<Info>();
            _flows = new ObservableCollection<Info>();
            _zones = new ObservableCollection<Info>();
            _report = new ObservableCollection<Info>();

            _client = new HomeyClient();
        }

        public string AccessToken
        {
            get
            {
                return _accessToken;
            }
            set
            {
                _accessToken = value;
                NotifyPropertyChanged(nameof(AccessToken));
            }
        }

        public string IpAddress
        {
            get
            {
                return _ipAddress;
            }
            set
            {
                _ipAddress = value;
                NotifyPropertyChanged(nameof(IpAddress));
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                NotifyPropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                NotifyPropertyChanged(nameof(Password));
            }
        }

        public bool LoginEnabled
        {
            get
            {
                return _loginEnabled;
            }
            set
            {
                _loginEnabled = value;
                NotifyPropertyChanged(nameof(LoginEnabled));
            }
        }

        public bool RequestEnabled
        {
            get
            {
                return _requestEnabled && !_requestInProgress;
            }
            set
            {
                _requestEnabled = value;
                NotifyPropertyChanged(nameof(RequestEnabled));
            }
        }

        public ObservableCollection<Info> Devices
        {
            get
            {
                return _devices;
            }
        }

        public ObservableCollection<Info> Alarms
        {
            get
            {
                return _alarms;
            }
        }

        public ObservableCollection<Info> Flows
        {
            get
            {
                return _flows;
            }
        }

        public ObservableCollection<Info> Zones
        {
            get
            {
                return _zones;
            }
        }

        public ObservableCollection<Info> Report
        {
            get
            {
                return _report;
            }
        }

        public string SelectedDeviceId
        {
            get
            {
                return _selectedDeviceId;
            }
            set
            {
                _selectedDeviceId = value;
                NotifyPropertyChanged(nameof(SelectedDeviceId));
            }
        }


        public string SelectedFlowId
        {
            get
            {
                return _selectedFlowId;
            }
            set
            {
                _selectedFlowId = value;
                NotifyPropertyChanged(nameof(SelectedFlowId));
            }
        }

        public string SelectedAlarmId
        {
            get
            {
                return _selectedAlarmId;
            }
            set
            {
                _selectedAlarmId = value;
                NotifyPropertyChanged(nameof(SelectedAlarmId));
            }
        }

        public string SelectedDeviceCaps
        {
            get
            {
                return _selectedDeviceCaps;
            }
            set
            {
                _selectedDeviceCaps = value;
                NotifyPropertyChanged(nameof(SelectedDeviceCaps));
            }
        }

        public string SelectedCap
        {
            get
            {
                return _selectedCap;
            }
            set
            {
                _selectedCap = value;
                NotifyPropertyChanged(nameof(SelectedCap));
            }
        }

        public string SelectedValue
        {
            get
            {
                return _selectedValue;
            }
            set
            {
                _selectedValue = value;
                NotifyPropertyChanged(nameof(SelectedValue));
            }
        }

        public string RequestStatus
        {
            get
            {
                return _requestStatus;
            }
            set
            {
                _requestStatus = value;
                NotifyPropertyChanged(nameof(RequestStatus));
            }
        }

        public bool RequestInProgress
        {
            get
            {
                return _requestInProgress;
            }
            set
            {
                _requestInProgress = value;
                NotifyPropertyChanged(nameof(RequestEnabled));
            }
        }

        public string HomeyVersion
        {
            get
            {
                return _homeyVersion;
            }
            set
            {
                _homeyVersion = value;
                NotifyPropertyChanged(nameof(HomeyVersion));
            }
        }

        public string HomeyModel
        {
            get
            {
                return _homeyModel;
            }
            set
            {
                _homeyModel = value;
                NotifyPropertyChanged(nameof(HomeyModel));
            }
        }

        public void UpdateHomeyInfo()
        {
            _client.HomeyIp = _ipAddress;
            _client.Token = _accessToken;
        }

        public async Task ObtainBearerToken()
        {
            AccessToken = await _client.Authenticate(_apiConfig, _userName, _password);
        }

        public async Task RequestDevices()
        {
            _devices.Clear();

            IList<Device> devices = await _client.GetDevices();
            
            foreach (Device device in devices)
            {
                string status = device.Available ? "available" : "unavailable";
                _devices.Add(new Info { Key = device.Id, Value = device.Name, Status = status});
            }

            NotifyPropertyChanged(nameof(Devices));
        }

        public async Task RequestFlows()
        {
            _flows.Clear();

            IList<Flow> flows = await _client.GetFlows();

            foreach (Flow flow in flows)
            {
                string status = flow.Enabled ? "Enabled" : "Disabled";
                _flows.Add(new Info { Key = flow.Id, Value = flow.Name, Status = status });
            }

            NotifyPropertyChanged(nameof(Flows));
        }


        public async Task RequestAlarms()
        {
            _alarms.Clear();

            IList<Alarm> alarms = await _client.GetAlarms();

            foreach (Alarm alarm in alarms)
            {
                string status = alarm.Enabled ? "Enabled" : "Disabled";
                _alarms.Add(new Info { Key = alarm.Id, Value = alarm.Name, Status = status});
            }

            NotifyPropertyChanged(nameof(Alarms));
        }

        public async Task RequestZones()
        {
            _zones.Clear();

            IList<Zone> zones = await _client.GetZones();

            foreach (Zone zone in zones)
            {
                string status = zone.Active ? "Active" : "Not Active";
                _zones.Add(new Info { Key = zone.Id, Value = zone.Name, Status = status});
            }

            NotifyPropertyChanged(nameof(Zones));
        }

        public async Task RequestDeviceInfo()
        {
            Device device = await _client.GetDevice(_selectedDeviceId);

            SelectedDeviceCaps = ConcatCaps(device.Capabilities);

        }

        private string ConcatCaps(IList<string> deviceCapabilities)
        {
            if (!deviceCapabilities.Any())
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(deviceCapabilities[0]);
            
            for (int i = 1; i < deviceCapabilities.Count; i++)
            {
                builder.Append($", {deviceCapabilities[i]}");
            }

            return builder.ToString();
        }

        public async Task SetValue()
        {
            if (_selectedCap != "onoff")
            {
                throw new NotImplementedException("only onoff is supported");
            }

            if (bool.TryParse(_selectedValue, out bool value))
            {
                await _client.SetBooleanCapability(_selectedDeviceId, _selectedCap, value);
            }
            else
            {
                throw new ArgumentException("Value is not a boolean");
            }
        }

        public async Task RequestValue()
        {
            Device device = await _client.GetDevice(_selectedDeviceId);
            
            CapabilitiesObj capObj = device.CapabilitiesObj[_selectedCap];
            
            SelectedValue = $"{capObj.LastUpdated:s} {capObj.Title}: {capObj.Value}{capObj.Units}";
        }

        public async Task RequestReport()
        {
            Report.Clear();

            CapatibilityReport report = await _client.GetCapability(_selectedDeviceId, _selectedCap);

            foreach (TimeValue value in report.Values)
            {
                _report.Add(new Info { Key = value.T.ToString("s"), Value = value.V});
            }

            NotifyPropertyChanged(nameof(Report));
        }

        public async Task EnableFlow(bool enable)
        {
            await _client.EnableFlow(_selectedFlowId, enable);
        }

        public async Task EnableAlarm(bool enable)
        {
            IList<Alarm> alarms = await _client.GetAlarms();

            Alarm alarm = alarms.First(a => a.Id == _selectedAlarmId);

            await _client.UpdateAlarm(_selectedAlarmId, enable, new DayTime(alarm.Time), alarm.Repetition);
        }

        public async Task TriggerFlow()
        {
            await _client.TriggerFlow(_selectedFlowId);
        }

        public async Task RequestSystem()
        {
            HomeySystem system = await _client.GetSystem();
            HomeyVersion = system.HomeyVersion;
            HomeyModel = system.HomeyModelName;
        }
    }
}
