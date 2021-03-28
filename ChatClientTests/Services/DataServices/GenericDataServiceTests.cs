using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.DataServices;
using AccountAndConnection;
using Services.Factories;

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