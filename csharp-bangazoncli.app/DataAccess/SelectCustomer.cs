using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using static csharp_bangazoncli.app.DataAccess.Models.CustomerList;
using System.Data.SqlClient;

namespace csharp_bangazoncli.app
{
    class SelectCustomer
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;
    
        public List<CustomerNameList> GetCustomerName()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT Customer.FirstName, Customer.LastName From Customer";

                var reader = cmd.ExecuteReader();

                var allCustomerNames = new List<CustomerNameList>();

                while(reader.Read())
                {
                    var allCustomerList = new CustomerNameList
                    {
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString()
                    };

                    allCustomerNames.Add(allCustomerList);
                }
                return allCustomerNames;

                
            }
        }

        
    }

 
    
        
    
}
