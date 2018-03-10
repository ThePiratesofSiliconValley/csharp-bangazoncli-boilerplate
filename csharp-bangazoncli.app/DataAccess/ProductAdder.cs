using csharp_bangazoncli.app.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace csharp_bangazoncli.app.DataAccess
{
    class ProductAdder
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;

        public bool AddNewProduct(string productName, string productDescription, double productPrice, int quantity, int customerId)
        {
            


            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"Insert
                                    INTO Products 
                                            (ProductName, 
                                             ProductDescription, 
                                             ProductPrice, 
                                             Quantity,
                                             CustomerId)
                                    VALUES
                                            (@productName,
                                             @productDescription,
                                             @productPrice,
                                             @quantity,
                                             @customerId)";

                connection.Open();

                var productNameParam = new SqlParameter("@productName", SqlDbType.NVarChar);
                productNameParam.Value = productName;
                cmd.Parameters.Add(productNameParam);

                var productDescriptionParam = new SqlParameter("@productDescription", SqlDbType.NVarChar);
                productDescriptionParam.Value = productDescription;
                cmd.Parameters.Add(productDescriptionParam);

                var productPriceParam = new SqlParameter("@productPrice", SqlDbType.Money);
                productPriceParam.Value = productPrice;
                cmd.Parameters.Add(productPriceParam);

                var quantityParam = new SqlParameter("@quantity", SqlDbType.Int);
                quantityParam.Value = quantity;
                cmd.Parameters.Add(quantityParam);

                var customerIdParam = new SqlParameter("@customerId", SqlDbType.Int);
                customerIdParam.Value = customerId;
                cmd.Parameters.Add(customerIdParam);

                var result = cmd.ExecuteNonQuery();

                return result == 1;
            }
        }

        public List<CustomerList> GetAllCustomers()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"Select * from Customer";
                var reader = cmd.ExecuteReader();
                var allCustomers = new List<CustomerList>();
                while (reader.Read())
                {
                    var customer = new CustomerList
                    {
                        CustomerId = int.Parse(reader["CustomerId"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString()
                    };

                    allCustomers.Add(customer);
                }
                return allCustomers;
            }
        }
    }
}
