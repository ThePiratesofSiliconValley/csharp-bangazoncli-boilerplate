using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using csharp_bangazoncli.app.DataAccess.Models;

namespace csharp_bangazoncli.app.DataAccess
{
    class CreateCustomerAccount
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;

        public bool AddNewCustomerInfo(string info)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO Customer
                                        (CustomerId,FirstName,LastName,Address,City,State,PostalCode,Phone)
                                    VALUES
                                        (@customerId,@firstName,@lastName,@address,@city,@state,@postalCode,@phone)";

                connection.Open();
            }
        }
    }
}
