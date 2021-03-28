using AccountAndConnection;
using Services.AuthServices;
using System.Threading.Tasks;

namespace ChatClient.States.Authenticators
{
    public interface IAuthenticator
    {
        BaseAccount CurrentAccout { get; }
        bool IsLoggedIn { get;  }

        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);
        Task<bool> Login(string email, string password);
        void Logout();
    }
}
