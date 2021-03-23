using ChatClient.Factories.WindowFactories;
using ChatClient.States.Authenticators;
using ChatClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClient.Factories.ViewModelFactories
{
    public class ViewModelRootFactory : IViewModelRootFactory
    {
        private readonly IAuthenticator _authenticator;
        private readonly IWindowFactory _windowFactory;

        public ViewModelRootFactory(IAuthenticator authenticator, IWindowFactory windowFactory)
        {
            _authenticator = authenticator;
            _windowFactory = windowFactory;
        }

        public BindableBase CreateViewModel(ViewType type)
        {
            return type switch
            {
                ViewType.MainWindow => MainWindowViewModel.GetInstance(_authenticator),
                ViewType.LoginWindow => new LoginViewModel(_authenticator, _windowFactory),
                ViewType.RegisterWindow => new RegisterViewModel(_authenticator, _windowFactory),
                _ => throw new Exception(),
            };
        }
    }
}
