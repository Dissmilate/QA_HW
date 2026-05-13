using OpenQA.Selenium;

namespace SeleniumTests
{
    [TestFixture]
    public class Tests : TestBase
    {
        [Test]
        public void LoginTest()
        {
            app.Navigation.OpenHomePage();
            app.Auth.Login(new AccountData("Horcrux", "admin123"));
        }

        [Test]
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
            app.Auth.Login(new AccountData("Horcrux", "admin123"));
            app.Navigation.OpenHomePage();
            
            ProductData product = new ProductData("Womens high heel point toe stiletto sandals ankle strap court shoes",
                "Apparel", "Shoes");
            
            app.Order.MakeOrder(product);
            Assert.IsTrue(app.Order.IsOrderSuccessful(), "Order was not successful");
        }
        
    }
}