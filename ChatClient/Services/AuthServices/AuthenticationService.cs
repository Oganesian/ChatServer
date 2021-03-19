using ChatClient.ClientConnection;
using ChatClient.DataBaseConnection;
using ChatClient.Exceptions;
using ChatClient.Models;
using ChatClient.Services.AuthServices;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace ChatClient.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        //public async Task<Account> Login(string email, string password)
        //{
        //    Account storedAccount = await DbConnectAccounts.GetByEmail(email);

        //    IPasswordHasher hasher = new PasswordHasher();
        //    var result = hasher.VerifyHashedPassword(storedAccount.PasswordHash, password);
        //    if (result != PasswordVerificationResult.Success)
        //    {
        //        throw new InvalidPasswordException();
        //    }

        //    return storedAccount;
        //}

        //public async Task<RegistrationResult> Register(string email, string username, string password, string passwordConfirm)
        //{
        //    RegistrationResult result = RegistrationResult.Success;

        //    var emailAccount = await DbConnectAccounts.GetByEmail(email);
        //    if (emailAccount != null)
        //    {
        //        result = RegistrationResult.EmailAlreadyExists;
        //    }

        //    var usernameAccount = await DbConnectAccounts.GetByUsername(username);
        //    if (usernameAccount != null)
        //    {
        //        throw new Exception(); // TODO: use custom exception
        //    }

        //    if (password.Equals(passwordConfirm))
        //    {
        //        result = RegistrationResult.PasswordsDoNotMatch;
        //    }

        //    if(result == RegistrationResult.Success)
        //    {
        //        IPasswordHasher hasher = new PasswordHasher();
        //        string passwordHash = hasher.HashPassword(password);

        //        Account account = new Account()
        //        {
        //            Email = email,
        //            Username = username,
        //            PasswordHash = passwordHash
        //        };

        //        await DbConnectAccounts.Write(account);
        //    }
        //    return result;
        //}
        public Task<BaseAccount> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<RegistrationResult> Register(string email, string username, string password, string passwordConfirm)
        {
            throw new NotImplementedException();
        }
    }
}