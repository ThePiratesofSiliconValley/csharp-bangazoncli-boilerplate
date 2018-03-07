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

        public bool AddNewCustomerInfo(int customerId, string firstName, string lastName, string address, 
                                        string city, string state, string postalCode, string phone)
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
                customerIdParam.Value = customerId;
                cmd.Parameters.Add(customerIdParam);

                var firstNameParam = new SqlParameter("@firstName", SqlDbType.NVarChar);
                firstNameParam.Value = firstName;
                cmd.Parameters.Add(firstNameParam);

                var lastNameParam = new SqlParameter("@lastName", SqlDbType.NVarChar);
                lastNameParam.Value = lastName;
                cmd.Parameters.Add(lastNameParam);

                var addressParam = new SqlParameter("@address", SqlDbType.NVarChar);
                addressParam.Value = address;
                cmd.Parameters.Add(addressParam);

                var cityParam = new SqlParameter("@city", SqlDbType.NVarChar);
                cityParam.Value = city;
                cmd.Parameters.Add(cityParam);

                var stateParam = new SqlParameter("@state", SqlDbType.NVarChar);
                stateParam.Value = state;
                cmd.Parameters.Add(stateParam);

                var postalCodeParam = new SqlParameter("@postalCode", SqlDbType.NVarChar);
                postalCodeParam.Value = postalCode;
                cmd.Parameters.Add(postalCodeParam);

                var phoneParam = new SqlParameter("@phone", SqlDbType.NVarChar);
                phoneParam.Value = phone;
                cmd.Parameters.Add(phoneParam);

                
                var result = cmd.ExecuteNonQuery();

                return result == 1;
            }
        }
    }
}
