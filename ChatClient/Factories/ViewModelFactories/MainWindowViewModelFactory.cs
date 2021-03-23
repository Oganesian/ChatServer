using ChatClient.States.Authenticators;
using ChatClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClient.Factories.ViewModelFactories
{
    public class MainWindowViewModelFactory : IViewModelFactory<MainWindowViewModel>
    {
        private readonly IAuthenticator _authenticator;

        public MainWindowViewModelFactory(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public MainWindowViewModel CreateViewModel()
        {
            return MainWindowViewModel.GetInstance(_authenticator);
        }
    }
}
