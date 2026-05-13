using NUnit.Framework;

namespace SeleniumTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [SetUp]
        public void SetupLogout()
        {
            app.Auth.Logout();
        }

        [Test, Order(2)]
        public void LoginTest()
        {
            app.Navigation.OpenHomePage();
            AccountData account = new AccountData("Horcrux", "admin123");
            app.Auth.Login(new AccountData(account.Username, account.Password));
            
            Assert.IsTrue(app.Auth.IsLoggedIn(), "Login failed - user is not logged in");
        }
        
        [Test]
        public void LoginWithValidDataTest()
        {
            var user = new AccountData(Settings.Login, Settings.Password);
            app.Auth.Login(user);

            Assert.IsTrue(app.Auth.IsLoggedIn(), "User should be logged in");
        }

        [Test, Order(1)]
        public void RegisterTest()
        {
            var user = new AccountData("horcrux", "admin123")
            {
                FirstName = "HORCRUX",
                LastName = "HORCRUX",
                Email = "emaailll@mail.ru",
                Address = "qqqqqqqq",
                City = "qqqqqqq",
                State = "Aberdeen",
                Zip = "qqqqqq",
                Country = "United Kingdom",
                Subscribe = true
            };
        
            app.Auth.Register(user);
        }
        

        [Test]
        public void LoginWithInvalidDataTest()
        {
            var invalidUser = new AccountData("wrong_user", "wrong_password");
            app.Auth.Login(invalidUser);

            Assert.IsFalse(app.Auth.IsLoggedIn(), "User should not be logged in with invalid credentials");
        }
    }
}