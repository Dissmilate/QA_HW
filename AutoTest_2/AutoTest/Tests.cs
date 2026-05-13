using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumTests
{
    [TestFixture]
    public class Tests : TestBase
    {
        [Test]
        public void LoginTest()
        {
            OpenHomePage();
            Login(new AccountData("Horcrux", "admin123"));
        }

        [Test]
        public void RegisterTest()
        {
            OpenHomePage();
            driver.FindElement(By.LinkText("Login or register")).Click();
    
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
    
            Register(user);
        }

        [Test]
        public void MakeOrderTest()
        {
            OpenHomePage();
            Login(new AccountData("Horcrux", "admin123"));
            OpenHomePage();

            JsClick(By.XPath("//section[@id='categorymenu']/nav/ul/li[2]/a"));
            driver.FindElements(By.XPath("//div[@id='maincontainer']//ul/li/div/a"))[1].Click();

            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=product/category&path=68_70");
            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=product/product&path=68_70&product_id=121");

            driver.FindElement(By.LinkText("Add to Cart")).Click();
            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=checkout/cart");

            JsClick(By.Id("cart_checkout1"));
            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=checkout/confirm");
            JsClick(By.Id("checkout_btn"));
            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=checkout/success");

            WaitUntil(d => d.Url.Contains("checkout"));
        }
    }
}