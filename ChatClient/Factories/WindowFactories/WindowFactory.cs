using ChatClient.Factories.ViewModelFactories;
using ChatClient.States.Authenticators;
using ChatClient.Views;
using CryptographyServices.KeyExchangeServices;
using System;
using System.Windows;

namespace ChatClient.Factories.WindowFactories
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IAuthenticator _authenticator;
        private readonly IDiffieHellmanKeyExchangeService _keyExchangeService;

        public WindowFactory(IAuthenticator authenticator, IDiffieHellmanKeyExchangeService keyExchangeService)
        {
            _authenticator = authenticator;
            _keyExchangeService = keyExchangeService;
        }

        public Window CreateWindow(ViewType type)
        {
            return type switch
            {
                ViewType.MainWindow => new MainWindow(new MainWindowViewModelFactory(_authenticator, _keyExchangeService).CreateViewModel()),
                ViewType.LoginWindow => new LoginWindow(new LoginViewModelFactory(_authenticator, this).CreateViewModel()),
                ViewType.RegisterWindow => new RegisterWindow(new RegisterViewModelFactory(_authenticator, this).CreateViewModel()),
                _ => throw new Exception(),// TODO: Custom exception
            };
        }
    }
}
