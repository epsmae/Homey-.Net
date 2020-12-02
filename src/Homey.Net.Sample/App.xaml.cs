using System;
using System.Windows;
using Homey.Net.Sample.ViewModel;
using Homey.Net.Sample.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Homey.Net.Sample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(@"/MahApps.Metro;component/Styles/Controls.xaml", UriKind.Relative) });
            Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(@"/MahApps.Metro;component/Styles/Fonts.xaml", UriKind.Relative) });
            Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(@"/MahApps.Metro;component/Styles/Themes/Light.Red.xaml", UriKind.Relative) });

            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
        }
    }
}
