using System;
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
    class CreateCustomerAccount
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;

        public bool AddNewCustomerInfo(int customerId, string firstName, string lastName, string address, string city, string state, string postalCode, string phone)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO Customer
                                        (CustomerId,FirstName,LastName,Address,City,State,PostalCode,Phone)
                                    VALUES
                                        (@customerId,@firstName,@lastName,@address,@city,@state,@postalCode,@phone)";

                var customerIdParam = new SqlParameter("@custimerId", SqlDbType.Int);
                var firstNameParam = new SqlParameter("@firstName", SqlDbType.NVarChar);
                var lastNameParam = new SqlParameter("@lastName", SqlDbType.NVarChar);
                var addressParam = new SqlParameter("@address", SqlDbType.NVarChar);
                var cityParam = new SqlParameter("@city", SqlDbType.NVarChar);
                var stateParam = new SqlParameter("@state", SqlDbType.NVarChar);
                var postalCodeParam = new SqlParameter("@postalCode", SqlDbType.NVarChar);
                var phoneParam = new SqlParameter("@phone", SqlDbType.NVarChar);

                
                cmd.ExecuteNonQuery();
            }
        }
    }
}
