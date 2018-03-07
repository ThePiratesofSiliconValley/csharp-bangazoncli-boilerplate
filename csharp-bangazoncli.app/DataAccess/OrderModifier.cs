using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_bangazoncli.app.DataAccess
{
    class OrderModifier
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;

        public int CreateOrder()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                int newOrder;


                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"insert into orders (customerId) values (123)
                                    select SCOPE_IDENTITY()";

                    connection.Open();

                newOrder = int.Parse(cmd.ExecuteScalar().ToString());
                return newOrder;
            }
        }
    }
}
