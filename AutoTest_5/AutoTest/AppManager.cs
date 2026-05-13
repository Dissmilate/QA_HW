using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    public class AppManager
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;

        private NavigationHelper navigation;
        private LoginHelper login;
        private OrderHelper order;
        private AccountHelper account;
        
        private static ThreadLocal<AppManager> app = new ThreadLocal<AppManager>();
        
        private AppManager()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            baseURL = "https://automationteststore.com/";
            verificationErrors = new StringBuilder();

            navigation = new NavigationHelper(this, baseURL);
            login = new LoginHelper(this);
            order = new OrderHelper(this);
            account = new AccountHelper(this);
        }
        
        ~AppManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            { 
                //ignore
            }
        }


        public static AppManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                AppManager newInstance = new AppManager();
                newInstance.Navigation.OpenHomePage();
                app.Value = newInstance;
            }
            return app.Value;
        }


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

        public AccountHelper Account
        {
            get
            {
                return account;
            }
        }
        
        public void Stop()
        {
                driver.Quit();
        }
    }
}