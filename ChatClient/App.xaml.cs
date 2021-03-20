using ChatClient.Factories;
using ChatClient.Services.AuthServices;
using ChatClient.Services.DataServices;
using ChatClient.States.Authenticators;
using ChatClient.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            Window window = new MainWindow();
            window.DataContext = MainWindowViewModel.GetInstance();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<ChatClientDbContextFactory>();
            services.AddSingleton<IAccountDataService, AccountDataService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IAuthenticator, Authenticator>();
            

            return services.BuildServiceProvider();
        }
    }
}
