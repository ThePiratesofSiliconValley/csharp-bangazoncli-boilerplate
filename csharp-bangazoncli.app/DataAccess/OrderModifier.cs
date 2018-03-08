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
    class OrderModifier
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;

        public int CreateOrder(int customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                int newOrder;


                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"insert into orders (customerId) values (@customerId)
                                    select SCOPE_IDENTITY()";

                    connection.Open();

                var selectedCustomer = new SqlParameter("@customerId", SqlDbType.Int);
                selectedCustomer.Value = customerId;
                cmd.Parameters.Add(selectedCustomer);

                newOrder = int.Parse(cmd.ExecuteScalar().ToString());
                return newOrder;
            }
        }
    }
}
