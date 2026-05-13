using OpenQA.Selenium;

namespace SeleniumTests;

public class AccountHelper : HelperBase
{
    public AccountHelper(AppManager manager) : base(manager)
    {
    }

    public void GoToAccountPage()
    {
        manager.Navigation.OpenHomePage();
        WaitForElementAndClick(By.XPath("//a[contains(@class, 'menu_account')]"));
    }

    public void GoToEditProfilePage()
    {
        WaitForElementAndClick(By.XPath("//ul[@class='side_account_list']//a[contains(@href, 'account/edit')]"));
    }

    public void UpdatePhoneNumber(string newPhoneNumber)
    {
        var phoneField = WaitForElement(By.Id("AccountFrm_telephone"));
        phoneField.Clear();
        phoneField.SendKeys(newPhoneNumber);
        
        SubmitChanges();
    }

    public void SubmitChanges()
    {
        WaitForElementAndClick(By.XPath("//button[@type='submit'][contains(@class, 'btn-orange')]"));
    }

    public string GetCurrentPhoneNumber()
    {
        var phoneField = WaitForElement(By.Id("AccountFrm_telephone"));
        return phoneField.GetAttribute("value");
    }

    public bool IsSuccessMessageDisplayed()
    {
        try
        {
            return driver.FindElement(By.XPath("//div[contains(@class, 'alert-success')]")).Displayed;
        }
        catch
        {
            return false;
        }
    }
}