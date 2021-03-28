using AccountAndConnection;
using CryptographyServices.KeyExchangeServices;
using Microsoft.AspNet.Identity;
using Services.DataServices;
using Services.Exceptions;
using System;
using System.Threading.Tasks;

namespace Services.AuthServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountDataService _accountDataService;
        private readonly IDiffieHellmanKeyExchangeService _keyExchangeService;

        public AuthenticationService(IAccountDataService accountDataService, IDiffieHellmanKeyExchangeService keyExchangeService)
        {
            _accountDataService = accountDataService;
            _keyExchangeService = keyExchangeService;
        }

        public async Task<BaseAccount> Login(string email, string password)
        {
            BaseAccount storedAccount = await _accountDataService.GetByEmail(email);

            IPasswordHasher hasher = new PasswordHasher();
            var result = hasher.VerifyHashedPassword(storedAccount.PasswordHash, password);
            if (result != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException();
            }

            return storedAccount;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string passwordConfirm)
        {
            RegistrationResult result = RegistrationResult.Success;

            var emailAccount = await _accountDataService.GetByEmail(email);
            if (emailAccount != null)
            {
                result = RegistrationResult.EmailAlreadyExists;
            }

            var usernameAccount = await _accountDataService.GetByUsername(username);
            if (usernameAccount != null)
            {
                throw new Exception(); // TODO: use custom exception
            }

            if (!password.Equals(passwordConfirm))
            {
                result = RegistrationResult.PasswordsDoNotMatch;
            }

            if (result == RegistrationResult.Success)
            {
                IPasswordHasher hasher = new PasswordHasher();
                string passwordHash = hasher.HashPassword(password);

                Account account = new Account(_keyExchangeService)
                {
                    Email = email,
                    Username = username,
                    PasswordHash = passwordHash
                };

                await _accountDataService.Create(account);
            }
            return result;
        }
    }
}