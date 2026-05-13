using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void OpenHomePage()
        {
            driver.Navigate().GoToUrl(baseURL);
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

    }
}