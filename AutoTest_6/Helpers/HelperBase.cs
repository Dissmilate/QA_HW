using OpenQA.Selenium;

using OpenQA.Selenium;

namespace SeleniumTests
{
    public class HelperBase
    {
        protected AppManager manager;
        protected IWebDriver driver;
        protected bool acceptNextAlert = true;

        public HelperBase(AppManager manager)
        {
            this.manager = manager;
            this.driver = manager.Driver;
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        public string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
        
        public void FillField(By locator, string text)
        {
            var element = driver.FindElement(locator);
            element.Click();
            element.Clear();
            element.SendKeys(text);
        }
        
        protected void WaitUntil(Func<IWebDriver, bool> condition, int timeoutSec = 10)
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

        protected IWebElement WaitForElement(By locator, int timeoutSec = 10)
        {
            IWebElement element = null;
            WaitUntil(d =>
            {
                try
                {
                    element = d.FindElement(locator);
                    return element.Displayed;
                }
                catch
                {
                    return false;
                }
            }, timeoutSec);
            return element;
        }

        protected void WaitForElementAndClick(By locator, int timeoutSec = 10)
        {
            var element = WaitForElement(locator, timeoutSec);
            element.Click();
        }

        
    }
}