using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM products;").ToList();
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO products (Name, Price, CategoryID) VALUES (@name, @price, @categoryID);", new { name, price, categoryID });
        }

        public Product GetProductById(int id)
        {
            return _connection.QuerySingle<Product>("SELECT * FROM products WHERE ProductID = @id", new { id });
        }

        public void UpdateProduct(Product product)
        {
            _connection.Execute("UPDATE products" +
                                " SET Name = @name," +
                                " Price = @price," +
                                " CategoryID = @categoryID," +
                                " OnSale = @onSale," +
                                " StockLevel = @stockLevel" +
                                " WHERE ProductID = @id;",
                                new
                                {
                                    id = product.ProductID,
                                    name = product.Name,
                                    price = product.Price,
                                    categoryID = product.CategoryID,
                                    onSale = product.OnSale,
                                    stockLevel = product.StockLevel
                                });
        }
        public void DeleteProduct(int id)
        {
            _connection.Execute("DELETE FROM products WHERE productID = @id", new { id });
            _connection.Execute("DELETE FROM sales WHERE productID = @id", new { id });
            _connection.Execute("DELETE FROM reviews WHERE productID = @id", new { id });
        }
    }
}
