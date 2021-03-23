using ChatClient.Factories.WindowFactories;
using ChatClient.States.Authenticators;
using ChatClient.Views;
using System.Windows;
using System.Windows.Input;

namespace ChatClient.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly IWindowFactory _windowFactory;

        private string _email;
        private string _password;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }


        public LoginViewModel(IAuthenticator authenticator, IWindowFactory windowFactory)
        {
            _authenticator = authenticator;
            _windowFactory = windowFactory;

            Login = new CommandHandler((o) => LoginExec((ICloseable)o), () => CanExecute);
            OpenRegisterWindow = new CommandHandler((o) => OpenRegisterWindowExec((ICloseable)o), () => CanExecute);
        }

        public ICommand Login { get; private set; }
        public ICommand OpenRegisterWindow { get; private set; }

        private async void LoginExec(ICloseable window)
        {
            if (await _authenticator.Login(Email, Password))
            {
                var mainWindow = _windowFactory.CreateWindow(Factories.ViewType.MainWindow);
                mainWindow.Show();
                window?.Close();
            }
            else
            {
                // TODO: some dialog
            }
        }

        private void OpenRegisterWindowExec(ICloseable window)
        {
            var registerWindow = _windowFactory.CreateWindow(Factories.ViewType.RegisterWindow);
            registerWindow.Show();
            window?.Close();
        }
    }
}
