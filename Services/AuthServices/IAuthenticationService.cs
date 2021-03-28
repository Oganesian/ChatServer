using AccountAndConnection;
using System.Threading.Tasks;

namespace Services.AuthServices
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
        Task<BaseAccount> Login(string email, string password);
    }
}
