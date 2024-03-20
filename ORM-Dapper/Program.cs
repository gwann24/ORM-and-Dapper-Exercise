using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            var departmentRepo = new DapperDepartmentRepository(conn);
            
            var departments = departmentRepo.GetAllDepartments();
            foreach (var department in departments)
            {
                Console.WriteLine($"{department.DepartmentID} | {department.Name}");
            };
            /*
            Console.WriteLine("Type a new Department name");
            var newDepartment = Console.ReadLine();
            departmentRepo.InsertDepartment(newDepartment);

            departments = departmentRepo.GetAllDepartments();
            foreach (var department in departments)
            {
                Console.WriteLine($"{department.DepartmentID} | {department.Name}");
            };
            */
            
            var productRepo = new DapperProductRepository(conn);

            var products = productRepo.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductID} | {product.Name} | {product.Price} | {product.CategoryID} | {product.OnSale} | {product.StockLevel}");
            };
            
            Console.WriteLine("Type a new Product name");
            var newProductName = Console.ReadLine();
            Console.WriteLine("Type a new Product price");
            var newProductPrice = double.Parse(Console.ReadLine());
            Console.WriteLine("Type a new Product CategoryID");
            var newCatID = int.Parse(Console.ReadLine());

            productRepo.CreateProduct(newProductName, newProductPrice, newCatID);
            products = productRepo.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductID} | {product.Name} | {product.Price} | {product.CategoryID} | {product.OnSale} | {product.StockLevel}");
            };
            Console.WriteLine("Enter ProductID to update");
            var userProductId = int.Parse(Console.ReadLine());
            var product2Update = productRepo.GetProductById(userProductId);
            Console.WriteLine($"{product2Update.ProductID} | {product2Update.Name}");
            product2Update.OnSale = 1;
            product2Update.StockLevel = "1000";
            Console.WriteLine(product2Update.StockLevel);
            productRepo.UpdateProduct(product2Update);
            products = productRepo.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductID} | {product.Name} | {product.Price} | {product.CategoryID} | {product.OnSale} | {product.StockLevel}");
            };
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            productRepo.DeleteProduct(userProductId);
            products = productRepo.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductID} | {product.Name} | {product.Price} | {product.CategoryID} | {product.OnSale} | {product.StockLevel}");
            };
        }
    }
}
