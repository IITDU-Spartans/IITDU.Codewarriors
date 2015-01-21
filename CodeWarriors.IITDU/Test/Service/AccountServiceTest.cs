using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeWarriors.IITDU.Models;
using NUnit.Framework;
using NUnit.Mocks;
using Rhino.Mocks;
using CodeWarriors.IITDU.Service;
namespace Test.Service
{
    [TestFixture]
    public class AccountServiceTest
    {
        [Test]
        public void TestValidateLogin()
        {
            var accountService = MockRepository.GenerateMock<IAccountService>();
            accountService.Stub(s => s.ValidateLogin("User", "Password")).Return(true);

            Assert.AreEqual(accountService.ValidateLogin("User", "Password"), true);
            Assert.AreEqual(accountService.ValidateLogin("User", "Passwords"), false);

        }

        [Test]
        public void TestValidateRegistration()
        {
            var accountService = MockRepository.GenerateMock<IAccountService>();
            accountService.Stub(s => s.ValidateRegistration("User")).Return(false);
            accountService.Stub(s => s.ValidateRegistration("Demo")).Return(true);
            Assert.AreEqual(accountService.ValidateRegistration("User"), false);
            Assert.AreEqual(accountService.ValidateRegistration("Demo"), true);
        }

        [Test]
        public void TestGetUser()
        {
            var accountService = MockRepository.GenerateMock<IAccountService>();
            accountService.Stub(s => s.GetUser("User")).Return(new User()
            {
                UserId = 1,
                UserName = "User",
                Password = "Password"
            });
            Assert.NotNull(accountService.GetUser("User"));
            Assert.IsNull(accountService.GetUser("Demo"));
            Assert.AreEqual(accountService.GetUser("User").UserId, 1);
            Assert.AreEqual(accountService.GetUser("User").UserName, "User");
            Assert.AreEqual(accountService.GetUser("User").Password, "Password");
        }

        [Test]
        public void TestGetProfile()
        {
            var accountService = MockRepository.GenerateMock<IAccountService>();
            accountService.Stub(s => s.GetProfile("User")).Return(new UserProfile()
            {
                UserId = 1,
                FirstName = "First",
                LastName = "Last",
                Gender = "Male",
                Country = "BD",
                Age = 21,
                Email = "user@gmail.com"
            });
            Assert.NotNull(accountService.GetProfile("User"));
            Assert.IsNull(accountService.GetProfile("Demo"));
            Assert.AreEqual(accountService.GetProfile("User").UserId, 1);
            Assert.AreEqual(accountService.GetProfile("User").FirstName, "First");
            Assert.AreEqual(accountService.GetProfile("User").LastName, "Last");
            Assert.AreEqual(accountService.GetProfile("User").Age, 21);
            Assert.AreEqual(accountService.GetProfile("User").Gender, "Male");
            Assert.AreEqual(accountService.GetProfile("User").Country, "BD");
            Assert.AreEqual(accountService.GetProfile("User").Email, "user@gmail.com");
        }
    }
}
