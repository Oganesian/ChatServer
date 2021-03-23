using ChatClient.Factories.ViewModelFactories;
using ChatClient.States.Authenticators;
using ChatClient.Views;
using System;
using System.Windows;

namespace ChatClient.Factories.WindowFactories
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IAuthenticator _authenticator;

        public WindowFactory(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public Window CreateWindow(ViewType type)
        {
            return type switch
            {
                ViewType.MainWindow => new MainWindow(new MainWindowViewModelFactory(_authenticator).CreateViewModel()),
                ViewType.LoginWindow => new LoginWindow(new LoginViewModelFactory(_authenticator, this).CreateViewModel()),
                ViewType.RegisterWindow => new RegisterWindow(new RegisterViewModelFactory(_authenticator, this).CreateViewModel()),
                _ => throw new Exception(),// TODO: Custom exception
            };
        }
    }
}
