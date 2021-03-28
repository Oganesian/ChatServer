using AccountAndConnection;
using ChatClient.Services.AuthServices;
using System;
using System.Threading.Tasks;

namespace ChatClient.States.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;

        public Authenticator(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public BaseAccount CurrentAccout { get; private set; }

        public bool IsLoggedIn => CurrentAccout != null;

        public async Task<bool> Login(string email, string password)
        {
            try
            {
                CurrentAccout = await _authenticationService.Login(email, password);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Logout() => CurrentAccout = null;
        

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            return await _authenticationService.Register(email, username, password, confirmPassword);
        }
    }
}
