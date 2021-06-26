using ChatClient.Factories.ViewModelFactories;
using ChatClient.States.Authenticators;
using ChatClient.Views;
using CryptographyServices.DecryptionServices;
using CryptographyServices.EncryptionServices;
using CryptographyServices.KeyExchangeServices;
using System;
using System.Windows;

namespace ChatClient.Factories.WindowFactories
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IAuthenticator _authenticator;
        private readonly IDiffieHellmanKeyExchangeService _keyExchangeService;
        private readonly IDiffieHellmanEncryptionService _messageEncryptionService;
        private readonly IDiffieHellmanDecryptionService _messageDecryptionService;
        public WindowFactory(IAuthenticator authenticator, IDiffieHellmanKeyExchangeService keyExchangeService, 
            IDiffieHellmanEncryptionService messageEncryptionService, IDiffieHellmanDecryptionService messageDecryptionService)
        {
            _authenticator = authenticator;
            _keyExchangeService = keyExchangeService;
            _messageEncryptionService = messageEncryptionService;
            _messageDecryptionService = messageDecryptionService;
        }

        public Window CreateWindow(ViewType type)
        {
            return type switch
            {
                ViewType.MainWindow => new MainWindow(new MainWindowViewModelFactory(_authenticator, _keyExchangeService, _messageEncryptionService, _messageDecryptionService).CreateViewModel()),
                ViewType.LoginWindow => new LoginWindow(new LoginViewModelFactory(_authenticator, this).CreateViewModel()),
                ViewType.RegisterWindow => new RegisterWindow(new RegisterViewModelFactory(_authenticator, this).CreateViewModel()),
                _ => throw new Exception(),// TODO: Custom exception
            };
        }
    }
}
