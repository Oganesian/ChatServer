using ChatClient.Factories.WindowFactories;
using ChatClient.States.Authenticators;
using ChatClient.ViewModels;
using ChatClient.Views;
using CryptographyServices.DecryptionServices;
using CryptographyServices.EncryptionServices;
using CryptographyServices.KeyExchangeServices;
using Microsoft.Extensions.DependencyInjection;
using Services.AuthServices;
using Services.DataServices;
using Services.Factories;
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

            Window window = serviceProvider.GetRequiredService<LoginWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<ChatClientDbContextFactory>();
            services.AddSingleton<IDiffieHellmanKeyExchangeService, DiffieHellmanKeyExchangeService>();
            services.AddSingleton<IDiffieHellmanDecryptionService, DiffieHellmanDecryptionService>();
            services.AddSingleton<IDiffieHellmanEncryptionService, DiffieHellmanEncryptionService>();
            services.AddSingleton<IAccountDataService, AccountDataService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IAuthenticator, Authenticator>();

            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<RegisterViewModel>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<IWindowFactory, WindowFactory>();

            services.AddSingleton(s => new LoginWindow(s.GetRequiredService<LoginViewModel>()));
            services.AddSingleton(s => new RegisterWindow(s.GetRequiredService<RegisterViewModel>()));
            services.AddSingleton(s => new MainWindow(s.GetRequiredService<MainWindowViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}
