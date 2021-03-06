﻿using csharp_bangazoncli.app.DataAccess.Models;
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
    class RevenueQuery
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;

        public List<RevenueResult> GetCustomerRevenue(string customerFirstName, string customerLastName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT Products.ProductName, 
		                                Products.ProductPrice,
		                                Customer.FirstName as SellerFirstName, 
		                                Customer.LastName as SellerLastName,
		                                 Products.Quantity as ProductQuantity, 
		                                 OrderLine.OrderId, 
		                                 OrderLine.OrderLineId, 
		                                 OrderLine.ProductId, 
		                                 OrderLine.Quantity as OrderLineQuantity,
                                         OrderLine.Quantity * Products.ProductPrice as indivItemTotal
                                FROM Products
	                                join Customer on Products.CustomerId = Customer.CustomerId
	                                join OrderLine on OrderLine.ProductId = Products.ProductId
                Where Customer.FirstName = @customerFirstName AND Customer.LastName = @customerLastName
                Order by OrderId";

                var customerFirstNameParam = new SqlParameter("@customerFirstName", SqlDbType.NVarChar);
                customerFirstNameParam.Value = customerFirstName;
                command.Parameters.Add(customerFirstNameParam);

                var customerLastNameParam = new SqlParameter("@customerLastName", SqlDbType.NVarChar);
                customerLastNameParam.Value = customerLastName;
                command.Parameters.Add(customerLastNameParam);


                var reader = command.ExecuteReader();

                var sellerRevenue = new List<RevenueResult>();

                while (reader.Read())
                {
                    var allSellersRevenue = new RevenueResult
                    {
                        ProductName = reader["ProductName"].ToString(),
                        ProductPrice = float.Parse(reader["ProductPrice"].ToString()),
                        SellerFirstName = reader["SellerFirstName"].ToString(),
                        SellerLastName = reader["SellerLastName"].ToString(),
                        ProductQuantity = int.Parse(reader["ProductQuantity"].ToString()),
                        OrderId = int.Parse(reader["OrderId"].ToString()),
                        OrderLineId = int.Parse(reader["OrderLineId"].ToString()),
                        ProductId = int.Parse(reader["ProductId"].ToString()),
                        OrderItemQuantity = int.Parse(reader["OrderLineQuantity"].ToString()),
                        indivItemTotal = double.Parse(reader["indivItemTotal"].ToString())
                    };

                    sellerRevenue.Add(allSellersRevenue);
                }
                return sellerRevenue;
            }
        }
    }
}