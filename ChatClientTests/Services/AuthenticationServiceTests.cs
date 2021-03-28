using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Services.DataServices;
using Services.AuthServices;
using AccountAndConnection;
using Services.Factories;

namespace ChatClient.Services.Tests
{
    [TestClass()]
    public class AuthenticationServiceTests
    {
        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IAccountDataService, AccountDataService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<ChatClientDbContextFactory>();

            return services.BuildServiceProvider();
        }

        [TestMethod()]
        public async Task RegisterTestAsync()
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            IAuthenticationService authService = serviceProvider.GetRequiredService<IAuthenticationService>();

            var result = await authService.Register("teaaasdst@gmail.com", "yunewglion", "123321V$%", "123321V$%");

            Assert.AreEqual(result, RegistrationResult.Success);
        }

        [TestMethod()]
        public async Task LoginTestAsync()
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            IAuthenticationService authService = serviceProvider.GetRequiredService<IAuthenticationService>();

            // AuthenticationService authService = new AuthenticationService(new AccountDataService());
            BaseAccount account = await authService.Login("teaast@gmail.com", "123321V$%");
            Assert.IsNotNull(account);
        }
    }
}