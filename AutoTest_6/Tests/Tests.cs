using OpenQA.Selenium;

namespace SeleniumTests
{
    [TestFixture]
    public class Tests : AuthBase
    {
        
       
        [Test]
        public void MakeOrderTest()
        {
            app.Navigation.OpenHomePage();
            
            ProductData product = new ProductData("Womens high heel point toe stiletto sandals ankle strap court shoes",
                "Apparel", "Shoes");
            
            app.Order.MakeOrder(product);
            Assert.IsTrue(app.Order.IsOrderSuccessful(), "Order was not successful");
        }
        
        [Test]
        public void EditProfileTest()
        {
            app.Navigation.OpenHomePage();
    
            app.Account.GoToAccountPage();
            app.Account.GoToEditProfilePage();
    
            string newPhone = $"+7{Random.Shared.Next(900, 999)}{Random.Shared.Next(1000000, 9999999)}";
            app.Account.UpdatePhoneNumber(newPhone);
    
            Assert.IsTrue(app.Account.IsSuccessMessageDisplayed(), "Profile update failed");
    
            app.Account.GoToEditProfilePage();
            Assert.AreEqual(newPhone, app.Account.GetCurrentPhoneNumber());
        }
        
        [Test]
        public void TestDataLoading()
        {
            var products = DataProviders.ProductDataFromXmlFile();
            Assert.IsNotEmpty(products, "No products loaded from XML");
            
            foreach (var product in products)
            {
                Console.WriteLine($"Product: {product.FullName}, Category: {product.CategoryName}");
            }
        }

        [Test]
        [TestCaseSource(typeof(DataProviders), nameof(DataProviders.ProductDataFromXmlFile))]
        public void MakeOrderWithTestDataTest(ProductData product)
        {
            
            
            Console.WriteLine($"Testing product: {product.FullName}");
            Console.WriteLine($"Category: {product.CategoryName} -> Subcategory: {product.SubcategoryName}");
            
            app.Navigation.OpenHomePage();
            app.Order.OpenSubCategory(product.CategoryName, product.SubcategoryName);
            app.Order.OpenProductByTitle(product.FullName);
            app.Order.AddToCart();
            app.Order.GoToCart();
            app.Order.ProceedToCheckout();
            app.Order.ConfirmOrder();
            
            app.Order.WaitForOrderCompletion();
            Assert.IsTrue(app.Order.IsOrderSuccessful(), $"Order failed for product: {product.FullName}");
        }
    }
}