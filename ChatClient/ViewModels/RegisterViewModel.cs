using ChatClient.Factories.WindowFactories;
using ChatClient.States.Authenticators;
using ChatClient.Views;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatClient.ViewModels
{
    public class RegisterViewModel : BindableBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly IWindowFactory _windowFactory;

        private string _email;
        private string _username;
        private string _password;
        private string _passwordConfirmation;

        public RegisterViewModel(IAuthenticator authenticator, IWindowFactory windowFactory)
        {
            _authenticator = authenticator;
            _windowFactory = windowFactory;

            Register = new CommandHandler((o) => RegisterExec((ICloseable)o), () => CanExecute);
            OpenLoginWindow = new CommandHandler((o) => OpenLoginWindowExec((ICloseable)o), () => CanExecute);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string PasswordConfirmation
        {
            get => _passwordConfirmation;
            set => SetProperty(ref _passwordConfirmation, value);
        }

        public ICommand Register { get; private set; }
        public ICommand OpenLoginWindow { get; private set; }

        private async void RegisterExec(ICloseable window)
        {
            var registrationResult = await _authenticator.Register(Email, Username, Password, PasswordConfirmation);
            if (registrationResult == Services.AuthServices.RegistrationResult.Success)
            {
                var loginWindow = _windowFactory.CreateWindow(Factories.ViewType.LoginWindow);
                loginWindow.Show();
                window.Close();
            }
            else
            {
                // TODO: some dialog
            }
        }

        private void OpenLoginWindowExec(ICloseable window)
        {
            var loginWindow = _windowFactory.CreateWindow(Factories.ViewType.LoginWindow);
            loginWindow.Show();
            window?.Close();
        }
    }
}
