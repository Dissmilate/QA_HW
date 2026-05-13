using System.Xml.Serialization;

namespace SeleniumTests
{
    public static class DataProviders
    {
        public static IEnumerable<ProductData> ProductDataFromXmlFile()
        {
            // Ищем файл в нескольких местах
            string[] possiblePaths = new[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "products.xml"),
                Path.Combine(Directory.GetCurrentDirectory(), "products.xml"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "Debug", "net8.0", "products.xml"),
                @"C:\Users\emirh\RiderProjects\AutoTest\AutoTestDataGenerator\bin\Release\net8.0\products.xml"
            };

            string filePath = null;
            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    filePath = path;
                    break;
                }
            }

            if (filePath == null)
            {
                Console.WriteLine("products.xml not found!");
                return new List<ProductData>();
            }

            Console.WriteLine($"Loading products from: {filePath}");

            using (StreamReader reader = new StreamReader(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<ProductData>));
                var products = (List<ProductData>)serializer.Deserialize(reader);
                Console.WriteLine($"Loaded {products.Count} products");
                return products;
            }
        }
    }
}