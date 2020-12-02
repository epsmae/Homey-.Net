using System;
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

        private void Setup_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.HandleSetup();
        }

        private async void RequestDevices_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                await _viewModel.RequestDevices();
            }
            catch (Exception ex)
            {
                ShowErrorDialog(ex);
            }
        }

        private async void RequestFlows_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                await _viewModel.RequestFlows();
            }
            catch (Exception ex)
            {
                ShowErrorDialog(ex);
            }
        }

        private async void RequestZones_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _viewModel.RequestZones();
            }
            catch (Exception ex)
            {
                ShowErrorDialog(ex);
            }
        }
        
        private async void Devices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                if (e.AddedItems[0] is Info info)
                {
                    _viewModel.SelectedDeviceId = info.Id;
                    
                    try
                    {
                        await _viewModel.RequestDeviceInfo();
                    }
                    catch (Exception ex)
                    {
                        ShowErrorDialog(ex);
                    }
                }
            }
        }

        private async void SetValue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _viewModel.SetValue();
            }
            catch (Exception ex)
            {
                ShowErrorDialog(ex);
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
