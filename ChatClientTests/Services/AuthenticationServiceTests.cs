using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChatClient.Services;
using System;
using System.Collections.Generic;
using System.Text;
using ChatClient.ClientConnection;
using ChatClient.Models;

namespace ChatClient.Services.Tests
{
    [TestClass()]
    public class AuthenticationServiceTests
    {
        [TestMethod()]
        public async System.Threading.Tasks.Task LoginTestAsync()
        {
            AuthenticationService authService = new AuthenticationService();
            BaseAccount account = await authService.Login("test@gmail.com", "123321V$%");
            Assert.IsNotNull(account);
        }
    }
}