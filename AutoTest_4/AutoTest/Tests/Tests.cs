using OpenQA.Selenium;

namespace SeleniumTests
{
    [TestFixture]
    public class Tests : TestBase
    {
        [Test, Order(2)]
        public void LoginTest()
        {
            app.Navigation.OpenHomePage();
            AccountData account = new AccountData("Horcrux", "admin123");
            app.Auth.Login(new AccountData(account.Username, account.Password));
            
            Assert.IsTrue(app.Auth.IsUserLoggedIn(), "Login failed - user is not logged in");
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
        public void MakeOrderTest()
        {
            app.Navigation.OpenHomePage();
            
            ProductData product = new ProductData("Womens high heel point toe stiletto sandals ankle strap court shoes",
                "Apparel", "Shoes");
            
            app.Order.MakeOrder(product);
            Assert.IsTrue(app.Order.IsOrderSuccessful(), "Order was not successful");
        }
        
        [Test]
        public void EditProfileTest()
        {
            app.Navigation.OpenHomePage();
    
            app.Account.GoToAccountPage();
            app.Account.GoToEditProfilePage();
    
            string newPhone = $"+7{Random.Shared.Next(900, 999)}{Random.Shared.Next(1000000, 9999999)}";
            app.Account.UpdatePhoneNumber(newPhone);
    
            Assert.IsTrue(app.Account.IsSuccessMessageDisplayed(), "Profile update failed");
    
            app.Account.GoToEditProfilePage();
            Assert.AreEqual(newPhone, app.Account.GetCurrentPhoneNumber());
        }
    }
}