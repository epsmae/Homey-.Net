using System.Windows;
using Homey.Net.Sample.ViewModel;
using MahApps.Metro.Controls;

namespace Homey.Net.Sample.Dialogs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class YesNoDialog : MetroWindow
    {
        private readonly YesNoDialogViewModel _viewModel;

        public YesNoDialog(YesNoDialogViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void NegativeButton_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.NegativeAction();
            Close();
        }

        private void PositiveButton_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.PositiveAction();
            Close();
        }
    }
}
