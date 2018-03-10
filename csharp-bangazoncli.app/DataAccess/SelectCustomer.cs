using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using static csharp_bangazoncli.app.DataAccess.Models.CustomerList;
using System.Data.SqlClient;
using csharp_bangazoncli.app.DataAccess.Models;

namespace csharp_bangazoncli.app
{
    class SelectCustomer
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;
    
        public List<CustomerList> GetCustomerName()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT Customer.CustomerId, Customer.FirstName, Customer.LastName From Customer";

                var reader = cmd.ExecuteReader();

                var allCustomerNames = new List<CustomerList>();

                while(reader.Read())
                {
                    var allCustomerList = new CustomerList
                    {
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        CustomerId = int.Parse(reader["customerid"].ToString())
                    };

                    allCustomerNames.Add(allCustomerList);
                }
                return allCustomerNames;

                
            }
        }

        
    }

 
    
        
    
}
