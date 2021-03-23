using ChatClient.Factories.WindowFactories;
using ChatClient.States.Authenticators;
using ChatClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClient.Factories.ViewModelFactories
{
    public class LoginViewModelFactory : IViewModelFactory<LoginViewModel>
    {
        private readonly IAuthenticator _authenticator;
        private readonly IWindowFactory _windowFactory;

        public LoginViewModelFactory(IAuthenticator authenticator, IWindowFactory windowFactory)
        {
            _authenticator = authenticator;
            _windowFactory = windowFactory;
        }

        public LoginViewModel CreateViewModel()
        {
            return new LoginViewModel(_authenticator, _windowFactory);
        }
    }
}
