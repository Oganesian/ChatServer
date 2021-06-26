using ChatClient.States.Authenticators;
using ChatClient.ViewModels;
using CryptographyServices.DecryptionServices;
using CryptographyServices.EncryptionServices;
using CryptographyServices.KeyExchangeServices;

namespace ChatClient.Factories.ViewModelFactories
{
    public class MainWindowViewModelFactory : IViewModelFactory<MainWindowViewModel>
    {
        private readonly IAuthenticator _authenticator;
        private readonly IDiffieHellmanKeyExchangeService _keyExchangeService;
        private readonly IDiffieHellmanEncryptionService _messageEncryptionService;
        private readonly IDiffieHellmanDecryptionService _messageDecryptionService;

        public MainWindowViewModelFactory(IAuthenticator authenticator, IDiffieHellmanKeyExchangeService keyExchangeService, IDiffieHellmanEncryptionService messageEncryptionService, IDiffieHellmanDecryptionService messageDecryptionService)
        {
            _authenticator = authenticator;
            _keyExchangeService = keyExchangeService;
            _messageEncryptionService = messageEncryptionService;
            _messageDecryptionService = messageDecryptionService;
        }

        public MainWindowViewModel CreateViewModel()
        {
            return MainWindowViewModel.GetInstance(_authenticator, _keyExchangeService, _messageEncryptionService, _messageDecryptionService);
        }
    }
}
