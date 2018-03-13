using csharp_bangazoncli.app.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_bangazoncli.app.DataAccess
{
    class ProductQuery
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;

        public List<Product> GetAllProducts()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select productId, productName, productPrice, quantity from products";
                connection.Open();

                var reader = cmd.ExecuteReader();
                var products = new List<Product>();

                while (reader.Read())
                {
                    var product = new Product
                    {
                        ProductId = int.Parse(reader["productId"].ToString()),
                        ProductName = reader["productName"].ToString(),
                        ProductPrice = double.Parse(reader["productPrice"].ToString()),
                        Quantity = int.Parse(reader["quantity"].ToString())
                    };

                    products.Add(product);
                }

                return products;
            }
        }

        public List<Product> GetCustomerProducts(int customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select * from products where CustomerId = @customerId";
                connection.Open();

                var customerIdParam = new SqlParameter("@customerId", SqlDbType.Int);
                customerIdParam.Value = customerId;
                cmd.Parameters.Add(customerIdParam);

                var reader = cmd.ExecuteReader();
                var products = new List<Product>();

                while (reader.Read())
                {
                    var product = new Product
                    {
                        ProductId = int.Parse(reader["productId"].ToString()),
                        ProductName = reader["productName"].ToString(),
                        ProductPrice = double.Parse(reader["productPrice"].ToString())
                    };

                    products.Add(product);
                }

                return products;
            }
        }

        public Product GetSingleProduct(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select * from products where productId = @productId";

                connection.Open();
                var productIdParam = new SqlParameter("@productId", SqlDbType.Int);
                productIdParam.Value = productId;
                cmd.Parameters.Add(productIdParam);

                var reader = cmd.ExecuteReader();
                var product = new Product();
                while (reader.Read())
                {
                    product.ProductId = int.Parse(reader["productId"].ToString());
                    product.ProductName = reader["productName"].ToString();
                    product.ProductDescription = reader["productdescription"].ToString();
                    product.ProductPrice = double.Parse(reader["productprice"].ToString());
                    product.Quantity = int.Parse(reader["quantity"].ToString());
                    product.CustomerId = int.Parse(reader["customerid"].ToString());
                }

                return product;
            }
        }
    }

}
