namespace SeleniumTests;

public class ProductData
{
    public ProductData(string fullName, string categoryName, string subcategoryName)
    {
        FullName = fullName;
        CategoryName = categoryName;
        SubcategoryName = categoryName;
    }

    public string FullName { get; set; }
    public string CategoryName { get; set; }
    public string SubcategoryName { get; set; }
}
