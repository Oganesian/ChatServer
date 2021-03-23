using ChatClient.Factories.WindowFactories;
using ChatClient.States.Authenticators;
using ChatClient.ViewModels;

namespace ChatClient.Factories.ViewModelFactories
{
    public class RegisterViewModelFactory : IViewModelFactory<RegisterViewModel>
    {
        private readonly IAuthenticator _authenticator;
        private readonly IWindowFactory _windowFactory;

        public RegisterViewModelFactory(IAuthenticator authenticator, IWindowFactory windowFactory)
        {
            _authenticator = authenticator;
            _windowFactory = windowFactory;
        }

        public RegisterViewModel CreateViewModel()
        {
            return new RegisterViewModel(_authenticator, _windowFactory);
        }
    }
}
