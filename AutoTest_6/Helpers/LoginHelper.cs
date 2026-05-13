using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests;

public class LoginHelper : HelperBase
{
    public LoginHelper(AppManager manager) : base(manager)
    {
    }
    
    
    public void Login(AccountData account)
    {
        // Если уже залогинены
        if (IsLoggedIn())
        {
            // Если залогинены под нужным пользователем — ничего не делаем
            if (IsLoggedIn(account.Username))
            {
                return;
            }
            // Если под другим — выходим
            Logout();
        }

        // Если не залогинены — выполняем авторизацию
        manager.Navigation.GoToLoginPage();
        FillField(By.Id("loginFrm_loginname"), account.Username);
        FillField(By.Id("loginFrm_password"), account.Password);
        SubmitLogin();
    }
    
    public void Register(AccountData account)
    {
        manager.Navigation.OpenHomePage();
        
        driver.FindElement(By.LinkText("Login or register")).Click();
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
    
    public void SubmitRegistration()
    {
        driver.FindElement(By.XPath("//button[@type='submit']")).Click();
    }
    
    public void SubmitLogin()
    {
        driver.FindElement(By.XPath("//form[@id='loginFrm']/fieldset/button")).Click();
    }
    
    public void SelectFromDropdown(By locator, string optionText)
    {
        new SelectElement(driver.FindElement(locator)).SelectByText(optionText);
    }
    
    public void AcceptPrivacyPolicy()
    {
        driver.FindElement(By.Id("AccountFrm_agree")).Click();
    }
    
    public bool IsLoggedIn()
    {
        try
        {
            var welcomeElement = driver.FindElement(By.XPath("//div[@id='customernav']//div[contains(@class, 'menu_text')]"));
            return welcomeElement.Displayed && welcomeElement.Text.Contains($"Welcome back ");
        }
        catch
        {
            return false;
        }
    }
    
    

    public bool IsLoggedIn(string username)
    {
        try
        {
            var welcomeElement = driver.FindElement(By.XPath("//div[@id='customernav']//div[contains(@class, 'menu_text')]"));
            string loggedInUser = welcomeElement.Text.Replace("Welcome back", "").Trim();
            return loggedInUser.Equals(username, StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    public void Logout()
    {
        try
        {
            var accountMenu = driver.FindElement(By.XPath("//a[contains(@class, 'menu_account')]"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(accountMenu).Perform();
            Thread.Sleep(300);

            var logoutLink = driver.FindElement(By.XPath("//a[contains(@href, 'account/logout')]"));
            logoutLink.Click();
            Thread.Sleep(500);
        }
        catch
        {
        }
    }
}