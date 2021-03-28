using ChatClient.States.Authenticators;
using ChatClient.ViewModels;
using CryptographyServices.KeyExchangeServices;

namespace ChatClient.Factories.ViewModelFactories
{
    public class MainWindowViewModelFactory : IViewModelFactory<MainWindowViewModel>
    {
        private readonly IAuthenticator _authenticator;
        private readonly IDiffieHellmanKeyExchangeService _keyExchangeService;

        public MainWindowViewModelFactory(IAuthenticator authenticator, IDiffieHellmanKeyExchangeService keyExchangeService)
        {
            _authenticator = authenticator;
            _keyExchangeService = keyExchangeService;
        }

        public MainWindowViewModel CreateViewModel()
        {
            return MainWindowViewModel.GetInstance(_authenticator, _keyExchangeService);
        }
    }
}
