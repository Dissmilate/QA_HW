using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    public class ApplicationManager
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;

        private NavigationHelper navigation;
        private LoginHelper login;
        private OrderHelper order;

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public NavigationHelper Navigation
        {
            get
            {
                return navigation;
            }
        }

        public LoginHelper Auth
        {
            get
            {
                return login;
            }
        }

        public OrderHelper Order
        {
            get
            {
                return order;
            }
        }
        
        public ApplicationManager()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            baseURL = "https://automationteststore.com/";
            verificationErrors = new StringBuilder();

            navigation = new NavigationHelper(this, baseURL);
            login = new LoginHelper(this);
            order = new OrderHelper(this);
        }

        public void Stop()
        {
                driver.Quit();
        }
    }
}