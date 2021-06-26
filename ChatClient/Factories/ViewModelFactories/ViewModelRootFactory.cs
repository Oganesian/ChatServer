using ChatClient.Factories.WindowFactories;
using ChatClient.States.Authenticators;
using ChatClient.ViewModels;
using CryptographyServices.DecryptionServices;
using CryptographyServices.EncryptionServices;
using CryptographyServices.KeyExchangeServices;
using System;

namespace ChatClient.Factories.ViewModelFactories
{
    public class ViewModelRootFactory : IViewModelRootFactory
    {
        private readonly IAuthenticator _authenticator;
        private readonly IDiffieHellmanKeyExchangeService _keyExchangeService;
        private readonly IDiffieHellmanEncryptionService _messageEncryptionService;
        private readonly IDiffieHellmanDecryptionService _messageDecryptionService;
        private readonly IWindowFactory _windowFactory;

        public ViewModelRootFactory(IAuthenticator authenticator, IWindowFactory windowFactory, IDiffieHellmanKeyExchangeService keyExchangeService)
        {
            _authenticator = authenticator;
            _windowFactory = windowFactory;
            _keyExchangeService = keyExchangeService;
        }

        public BindableBase CreateViewModel(ViewType type)
        {
            return type switch
            {
                ViewType.MainWindow => MainWindowViewModel.GetInstance(_authenticator, _keyExchangeService, _messageEncryptionService, _messageDecryptionService),
                ViewType.LoginWindow => new LoginViewModel(_authenticator, _windowFactory),
                ViewType.RegisterWindow => new RegisterViewModel(_authenticator, _windowFactory),
                _ => throw new Exception(),
            };
        }
    }
}
