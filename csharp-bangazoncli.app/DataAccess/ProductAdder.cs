using System;
using System.Configuration;
using System.Data.SqlClient;

namespace csharp_bangazoncli.app.DataAccess
{
    class ProductAdder
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;

        public bool Insert()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"Insert
                                    into Products
                                    where productId = @ProductId,
                                          productName = @ProductName,
                                          productDescription = @ProductDescription,
                                          productPrice = @ProductPrice,
                                          quantity = @Quantity,";

                connection.Open();

                var result = cmd.ExecuteNonQuery();

                return result == 1;
            }
        }
        

    }
}
