﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using csharp_bangazoncli.app.DataAccess.Models;

namespace csharp_bangazoncli.app.DataAccess
{
    class RemoveProduct
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;

        public RemoveCustomerProduct(CustomerList customer)
        {
            var productQuery = new ProductQuery(); // Calling the ProductQuery class, to get the GetAllProducts List

            var getAllProducts = productQuery.GetCustomerProducts(customer.CustomerId); // Assigns the GetAllProducts List to the getAllProducts variable

            var counter = 0;
            foreach (var product in getAllProducts)
            {
                counter++;
                Console.WriteLine($"{counter}. {product.ProductName}");
            }

            var selectedProduct = int.Parse(Console.ReadLine());

            var productToDelete = getAllProducts[selectedProduct - 1];

            var deleteProduct = DeleteProduct(productToDelete.ProductId);
        }

        public CheckOrderLine(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select count(*) from oderline
                                    where ProductId = @productId";
                connection.Open();

                var productIdParam = new SqlParameter("@productId", SqlDbType.Int);
                productIdParam.Value = productId;
                cmd.Parameters.Add(productIdParam);
                

            }
        }
    }
}
