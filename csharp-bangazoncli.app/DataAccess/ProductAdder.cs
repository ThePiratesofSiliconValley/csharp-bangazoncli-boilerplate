using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace csharp_bangazoncli.app.DataAccess
{
    class ProductAdder
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;

        public bool AddNewProduct(string productName, string productDescription, double productPrice, int quantity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"Insert
                                    INTO Products 
                                            (ProductName, 
                                             ProductDescription, 
                                             ProductPrice, 
                                             Quantity)
                                    VALUES
                                            (@productName,
                                             @productDescription,
                                             @productPrice,
                                             @quantity)";

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

                var result = cmd.ExecuteNonQuery();

                return result == 1;
            }
        }
        

    }
}
