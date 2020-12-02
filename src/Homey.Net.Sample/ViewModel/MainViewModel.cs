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
        private string _selectedDeviceId;
        private string _selectedDeviceCaps;
        private string _selectedCap;
        private string _selectedValue;
        private bool _loginEnabled;
        private bool _requestEnabled;

        private readonly ObservableCollection<Info> _devices;
        private readonly ObservableCollection<Info> _flows;
        private readonly ObservableCollection<Info> _zones;
        private HomeyClient _client;

        public MainViewModel()
        {
            _accessToken = "c643-46bf-8e67-29f0d9d64255:36df75c90a909a27c0a5ae597f78c8829554796b";
            _ipAddress = "192.168.5.88:80";
            _requestEnabled = false;
            _loginEnabled = true;
            _devices = new ObservableCollection<Info>();
            _flows = new ObservableCollection<Info>();
            _zones = new ObservableCollection<Info>();
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
                return _requestEnabled;
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


        public void HandleSetup()
        {
            LoginEnabled = false;
            RequestEnabled = true;
            _client = new HomeyClient(_ipAddress, _accessToken);
        }

        public async Task RequestDevices()
        {
            _devices.Clear();

            IList<Device> devices = await _client.GetDevices();
            
            foreach (Device device in devices)
            {
                _devices.Add(new Info{Id = device.Id, Name = device.Name});
            }

            NotifyPropertyChanged(nameof(Devices));
        }

        public async Task RequestFlows()
        {
            _flows.Clear();

            IList<Flow> flows = await _client.GetFlows();

            foreach (Flow flow in flows)
            {
                _flows.Add(new Info { Id = flow.Id, Name = flow.Name });
            }

            NotifyPropertyChanged(nameof(Flows));
        }

        public async Task RequestZones()
        {
            _zones.Clear();

            IList<Zone> zones = await _client.GetZones();

            foreach (Zone zone in zones)
            {
                _zones.Add(new Info { Id = zone.Id, Name = zone.Name });
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
    }
}
