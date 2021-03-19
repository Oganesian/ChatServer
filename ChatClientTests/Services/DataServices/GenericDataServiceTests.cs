using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChatClient.Services.DataServices;
using System;
using System.Collections.Generic;
using System.Text;
using ChatClient.ClientConnection;
using ChatClient.Factories;
using System.Linq;
using ChatClient.Models;

namespace ChatClient.Services.DataServices.Tests
{
    [TestClass()]
    public class GenericDataServiceTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            IDataService<BaseAccount> accountService = new GenericDataService<BaseAccount>(new ChatClientDbContextFactory());
            accountService.Create(new BaseAccount() { PublicId = 421, Username = "ww" }).Wait();

            //Assert.AreNotEqual(accountService.Delete(2), true);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            IDataService<BaseAccount> accountService = new GenericDataService<BaseAccount>(new ChatClientDbContextFactory());
            accountService.Update(2, new BaseAccount() { PublicId = 421, Username = "Troy" });
        }
    }
}