using System;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    public class TestBase
    {
        public IWebDriver driver;
        public StringBuilder verificationErrors;
        public string baseURL;
        public WebDriverWait wait;
        public bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "https://automationteststore.com/";
            driver.Manage().Window.Maximize();
            verificationErrors = new StringBuilder();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        

        public void OpenHomePage()
        {
            driver.Navigate().GoToUrl("https://automationteststore.com/");
        }

        public void GoToLoginPage()
        {
            driver.FindElement(By.LinkText("Login or register")).Click();
        }

        public void FillField(By locator, string text)
        {
            var element = driver.FindElement(locator);
            element.Click();
            element.Clear();
            element.SendKeys(text);
        }

        public void SelectFromDropdown(By locator, string optionText)
        {
            new SelectElement(driver.FindElement(locator)).SelectByText(optionText);
        }

        public void SubmitLogin()
        {
            driver.FindElement(By.XPath("//form[@id='loginFrm']/fieldset/button")).Click();
        }

        public void SubmitRegistration()
        {
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        public void AcceptPrivacyPolicy()
        {
            driver.FindElement(By.Id("AccountFrm_agree")).Click();
        }

        public void JsClick(By locator)
        {
            var element = wait.Until(d => d.FindElement(locator));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
        }

        public void WaitUntil(Func<IWebDriver, bool> condition, int timeoutSec = 10)
        {
            var end = DateTime.Now.AddSeconds(timeoutSec);
            while (DateTime.Now < end)
            {
                try
                {
                    if (condition(driver))
                        return;
                }
                catch { }
                Thread.Sleep(200);
            }
            throw new Exception("Condition not met within timeout");
        }

        public void Login(AccountData account)
        {
            GoToLoginPage();
            FillField(By.Id("loginFrm_loginname"), account.Username);
            FillField(By.Id("loginFrm_password"), account.Password);
            SubmitLogin();
        }

        public void Register(AccountData account)
        {
            SubmitRegistration();

            FillField(By.Id("AccountFrm_firstname"), account.FirstName);
            FillField(By.Id("AccountFrm_lastname"), account.LastName);
            FillField(By.Id("AccountFrm_email"), account.Email);
            FillField(By.Id("AccountFrm_address_1"), account.Address);
            FillField(By.Id("AccountFrm_city"), account.City);

            SelectFromDropdown(By.Id("AccountFrm_zone_id"), account.State);
            SelectFromDropdown(By.Id("AccountFrm_country_id"), account.Country);

            FillField(By.Id("AccountFrm_postcode"), account.Zip);
            FillField(By.Id("AccountFrm_loginname"), account.Username);
            FillField(By.Id("AccountFrm_password"), account.Password);
            FillField(By.Id("AccountFrm_confirm"), account.Password);

            if (account.Subscribe)
            {
                driver.FindElement(By.Id("AccountFrm_newsletter1")).Click();
            }

            AcceptPrivacyPolicy();
            SubmitRegistration();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
                driver.Dispose();
            }
            catch (Exception)
            {
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
    }
}