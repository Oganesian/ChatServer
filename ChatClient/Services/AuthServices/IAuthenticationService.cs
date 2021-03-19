using ChatClient.ClientConnection;
using ChatClient.Models;
using System.Threading.Tasks;

namespace ChatClient.Services.AuthServices
{
    public enum RegistrationResult
    {
        Success,
        PasswordsDoNotMatch,
        EmailAlreadyExists
    }

    public interface IAuthenticationService
    {
        Task<RegistrationResult> Register(string email, string username, string password, string passwordConfirm);
        Task<BaseAccount> Login(string username, string password);
    }
}
