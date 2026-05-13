namespace SeleniumTests;

public class ProductData
{
    public ProductData(string fullName, string categoryName)
    {
        FullName = fullName;
        CategoryName = categoryName;
    }

    public string FullName { get; set; }
    public string CategoryName { get; set; }
}
