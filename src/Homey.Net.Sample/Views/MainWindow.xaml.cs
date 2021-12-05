using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Homey.Net.Sample.Dialogs;
using Homey.Net.Sample.ViewModel;
using MahApps.Metro.Controls;

namespace Homey.Net.Sample.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly MainViewModel _viewModel;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private async void Login_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await TryExecute(_viewModel.ObtainBearerToken());
        }

        private async void Setup_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await TryExecute(Init());
        }

        private async Task Init()
        {
            try
            {
                _viewModel.LoginEnabled = false;
                _viewModel.UpdateHomeyInfo();

                await _viewModel.RequestSystem();

                _viewModel.RequestEnabled = true;
            }
            catch (Exception ex)
            {
                _viewModel.LoginEnabled = true;
                throw;
            }
        }


        private async void RequestDevices_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await TryExecute(_viewModel.RequestDevices());
        }

        private async void RequestAlarms_Click(object sender, RoutedEventArgs e)
        {
            await TryExecute(_viewModel.RequestAlarms());
        }

        private async void EnableAlarm_Click(object sender, RoutedEventArgs e)
        {
            await TryExecute(_viewModel.EnableAlarm(true));
        }

        private async void DisableAlarm_Click(object sender, RoutedEventArgs e)
        {
            await TryExecute(_viewModel.EnableAlarm(false));
        }

        private async void RequestFlows_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await TryExecute(_viewModel.RequestFlows());
        }

        private async void EnableFlow_Click(object sender, RoutedEventArgs e)
        {
            await TryExecute(_viewModel.EnableFlow(true));
        }

        private async void DisableFlow_Click(object sender, RoutedEventArgs e)
        {
            await TryExecute(_viewModel.EnableFlow(false));
        }

        private async void TriggerFlow_Click(object sender, RoutedEventArgs e)
        {
            await TryExecute(_viewModel.TriggerFlow());
        }

        private async void RequestZones_Click(object sender, RoutedEventArgs e)
        {
            await TryExecute(_viewModel.RequestZones());
        }

        private async void Devices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                if (e.AddedItems[0] is Info info)
                {
                    _viewModel.SelectedDeviceId = info.Key;

                    await TryExecute(_viewModel.RequestDeviceInfo());
                }
            }
        }

        private void Flows_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                if (e.AddedItems[0] is Info info)
                {
                    _viewModel.SelectedFlowId = info.Key;
                }
            }
        }

        private void Alarms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                if (e.AddedItems[0] is Info info)
                {
                    _viewModel.SelectedAlarmId = info.Key;
                }
            }
        }


        private async void SetValue_Click(object sender, RoutedEventArgs e)
        {
            await TryExecute(_viewModel.SetValue());
        }

        private async void GetValue_Click(object sender, RoutedEventArgs e)
        {
            await TryExecute(_viewModel.RequestValue());
        }

        private async void GetReport_Click(object sender, RoutedEventArgs e)
        {
            await TryExecute(_viewModel.RequestReport());
        }

        private async Task TryExecute(Task task)
        {
            try
            {
                _viewModel.RequestStatus = "Requesting...";
                _viewModel.RequestInProgress = true;
                await task;
                _viewModel.RequestStatus = "Ok!";
            }
            catch (Exception ex)
            {
                _viewModel.RequestStatus = "Error!";
                ShowErrorDialog(ex);
            }
            finally
            {
                _viewModel.RequestInProgress = false;
            }
        }


        private static void ShowErrorDialog(Exception ex)
        {
            YesNoDialogViewModel dialogViewModel = new YesNoDialogViewModel("Error",
                ex.ToString(), "OK",
                string.Empty, null, null);
            YesNoDialog dialog = new YesNoDialog(dialogViewModel);
            dialog.ShowDialog();
        }
    }
}
