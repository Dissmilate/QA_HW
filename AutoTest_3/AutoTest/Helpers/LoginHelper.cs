using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests;

public class LoginHelper : HelperBase
{
    public LoginHelper(ApplicationManager manager) : base(manager)
    {
    }
    
    public void Login(AccountData account)
    {
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
}