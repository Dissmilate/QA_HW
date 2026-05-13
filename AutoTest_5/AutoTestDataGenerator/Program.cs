using System.Xml.Serialization;
using SeleniumTests;

namespace AutoTestDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: AutoTestDataGenerator.exe <count> <filename> <format>");
                Console.WriteLine("Example: AutoTestDataGenerator.exe 10 products.xml xml");
                Console.WriteLine("");
                Console.WriteLine("Generated products will have correct category-subcategory pairs:");
                Console.WriteLine("  - Apparel & accessories → Shoes, T-shirts");
                Console.WriteLine("  - Makeup → Cheeks, Eyes");
                Console.WriteLine("  - Skincare → Face, Body & Shower");
                Console.WriteLine("  - Fragrance → Men, Women");
                return;
            }

            int count = int.Parse(args[0]);
            string filename = args[1];
            string format = args[2].ToLower();

            if (format == "xml")
            {
                GenerateProductDataXml(count, filename);
            }
            else
            {
                Console.WriteLine($"Format '{format}' not supported yet. Use 'xml'.");
            }
        }

        static void GenerateProductDataXml(int count, string filename)
        {
            List<ProductData> products = new List<ProductData>();
            Random rnd = new Random();

            for (int i = 0; i < count; i++)
            {
                var category = allCategories[rnd.Next(allCategories.Length)];
                
                var product = category.Products[rnd.Next(category.Products.Length)];
                products.Add(product);
            }

            SaveToXml(products, filename);
            
            var stats = products.GroupBy(p => p.CategoryName)
                .Select(g => $"{g.Key}: {g.Count()} товаров")
                .ToList();
            
            Console.WriteLine($"Generated {count} product(s) to {filename}");
            Console.WriteLine("Distribution by category:");
            foreach (var stat in stats)
            {
                Console.WriteLine($"  - {stat}");
            }
        }

        static void SaveToXml<T>(List<T> data, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (FileStream stream = new FileStream(filename, FileMode.Create))
            {
                serializer.Serialize(stream, data);
            }
        }

        class CategoryData
        {
            public string Name { get; set; }
            public ProductData[] Products { get; set; }
        }


        static CategoryData[] allCategories = new CategoryData[]
        {
            new CategoryData
            {
                Name = "Apparel & accessories",
                Products = new ProductData[]
                {
                    new ProductData("Womens high heel point toe stiletto sandals ankle strap court shoes", "Apparel & accessories", "Shoes"),
                    new ProductData("Fiorella Purple Peep Toes", "Apparel & accessories", "Shoes"),
                    new ProductData("New Ladies High Wedge Heel Toe Thong Diamante Flip Flop Sandals", "Apparel & accessories", "Shoes"),
                    new ProductData("Ruby Shoo Womens Jada T-Bar", "Apparel & accessories", "Shoes"),
                    new ProductData("Designer Men Casual Formal Double Cuffs Grandad Band Collar Shirt Elegant Tie", "Apparel & accessories", "T-shirts"),
                    new ProductData("Casual 3/4 Sleeve Baseball T-Shirt", "Apparel & accessories", "T-shirts"),
                    new ProductData("Jersey Cotton Striped Polo Shirt", "Apparel & accessories", "T-shirts")
                }
            },

            new CategoryData
            {
                Name = "Makeup",
                Products = new ProductData[]
                {
                    new ProductData("Skinsheen Bronzer Stick", "Makeup", "Cheeks"),
                    new ProductData("Tropiques Minerale Loose Bronzer", "Makeup", "Cheeks"),
                    new ProductData("Benefit Bella Bamba", "Makeup", "Cheeks"),
                    new ProductData("BeneFit Girl Meets Pearl", "Makeup", "Cheeks"),
                    new ProductData("L'EXTRÊME Instant Extensions Lengthening Mascara", "Makeup", "Eyes"),
                    new ProductData("Waterproof Protective Undereye Concealer", "Makeup", "Eyes"),
                    new ProductData("Lancome Hypnose Doll Lashes Mascara 4-Piece Gift Set", "Makeup", "Eyes")
                }
            },

            new CategoryData
            {
                Name = "Skincare",
                Products = new ProductData[]
                {
                    new ProductData("Total Moisture Facial Cream", "Skincare", "Face"),
                    new ProductData("Lancome Visionnaire Advanced Skin Corrector", "Skincare", "Face"),
                    new ProductData("Body Cream by Bulgari", "Skincare", "Body & Shower"),
                    new ProductData("Jasmin Noir Body Lotion 6.8 fl oz", "Skincare", "Body & Shower")
                }
            },

            new CategoryData
            {
                Name = "Fragrance",
                Products = new ProductData[]
                {
                    // Men
                    new ProductData("Designer Men Casual Formal Double Cuffs Grandad Band Collar Shirt Elegant Tie", "Fragrance", "Men"),
                    // Women
                    new ProductData("Jasmin Noir Body Lotion 6.8 fl oz", "Fragrance", "Women")
                }
            }
        };
    }
}