using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace SeleniumTests
{
    public class OrderHelper : HelperBase
    {
        public OrderHelper(AppManager manager) : base(manager)
        {
        }

        
        public void MakeOrder(ProductData product)
        {
            manager.Navigation.OpenHomePage();
            
            // Оставил для теста отдельно "Shoes" тк если брать из класса то пОчЕмУ-тО ВСЕ ЛОМАЕТСЯ
            manager.Order.OpenSubCategory(product.CategoryName, "Shoes");
            manager.Order.OpenProductByTitle(product.FullName);
            
            manager.Order.AddToCart();
            
            manager.Order.GoToCart();
            manager.Order.ProceedToCheckout();
            manager.Order.ConfirmOrder();
            
            manager.Order.WaitForOrderCompletion();
        }
        
        
        public void OpenSubCategory(string mainCategory, string subCategory)
        {
            var mainCategoryElement = driver.FindElement(By.XPath($"//ul[@class='nav-pills categorymenu']//a[contains(text(),'{mainCategory}')]"));
    
            Actions actions = new Actions(driver);
            actions.MoveToElement(mainCategoryElement).Perform();
    
            var subCategoryElement = driver.FindElement(By.XPath($"//div[@class='subcategories']//a[contains(text(),'{subCategory}')]"));
            subCategoryElement.Click();
        }
        
        public void OpenProductByTitle(string title)
        {
            var productLink = WaitForElement(By.XPath($"//a[@class='prdocutname'][@title='{title}']"));
            productLink.Click();
        }

        public void OpenProductByIndex(int index)
        {
            var products = driver.FindElements(By.XPath("//div[@class='product-thumb']//a[@class='productname']"));
            if (index < products.Count)
            {
                products[index].Click();
            }
            else
            {
                throw new Exception($"Product index {index} not found. Only {products.Count} products available.");
            }
        }

        public void OpenProductById(string productId)
        {
            driver.Navigate().GoToUrl($"https://automationteststore.com/index.php?rt=product/product&product_id={productId}");
        }

        public void AddToCart()
        {
            WaitForElementAndClick(By.LinkText("Add to Cart"));
        }

        public void GoToCart()
        {
            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=checkout/cart");
        }

        public void ProceedToCheckout()
        {
            JsClick(By.Id("cart_checkout1"));
        }

        public void ConfirmOrder()
        {
            JsClick(By.Id("checkout_btn"));
        }

        public void GoToSuccessPage()
        {
            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=checkout/success");
        }

        public void WaitForOrderCompletion()
        {
            WaitUntil(d => d.Url.Contains("checkout/success") || d.Url.Contains("success"), 15);
        }

        public bool IsOrderSuccessful()
        {
            return driver.Url.Contains("success");
        }

        private void JsClick(By locator)
        {
            var element = WaitForElement(locator);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
        }

        private IWebElement WaitForElement(By locator, int timeoutSec = 10)
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

        private void WaitForElementAndClick(By locator, int timeoutSec = 10)
        {
            var element = WaitForElement(locator, timeoutSec);
            element.Click();
        }
    }
}