namespace SeleniumTests;

public class ProductData
{
    public string FullName { get; set; }
    public string CategoryName { get; set; }
    public string SubcategoryName { get; set; }
    
    public ProductData()
    {
        FullName = "";
        CategoryName = "";
        SubcategoryName = "";
    }

    public ProductData(string fullName, string categoryName, string subcategoryName)
    {
        FullName = fullName;
        CategoryName = categoryName;
        SubcategoryName = subcategoryName;
    }
}
