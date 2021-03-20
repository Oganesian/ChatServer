using ChatClient.States.Authenticators;
using System.Windows.Input;

namespace ChatClient.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IAuthenticator _authenticator;

        private string _email;

        private ICommand _login;

        public string Email

        {
            get => _email;
            set => SetProperty(ref _email, value);
        }


        public LoginViewModel(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public ICommand Login
        {
            get
            {
                if (_login == null)
                {
                    _login = new CommandHandler(() => LoginExec(), () => CanExecute);
                }
                return _login;
            }
        }

        private async void LoginExec()
        {
            bool success = await _authenticator.Login(Email, "");
        }
    }
}
