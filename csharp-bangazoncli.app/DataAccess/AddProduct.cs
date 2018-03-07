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
    class AddProduct
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;

        public bool AddProductToOrder(int selectedProduct, int numberToAdd, int orderId)
        {
            var productQuery = new ProductQuery();
            var getProductDetails = productQuery.GetSingleProduct(selectedProduct);

            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO OrderLine
                                               (OrderId
                                               ,ProductId
                                               ,Quantity)
                                         VALUES
                                               (@orderId
                                               ,@productId
                                               ,@numberToAdd)";
                connection.Open();

                var orderIdInsert = new SqlParameter("@orderId", SqlDbType.Int);
                orderIdInsert.Value = orderId;
                cmd.Parameters.Add(orderIdInsert);

                var productIdInsert = new SqlParameter("@productId", SqlDbType.Int);
                productIdInsert.Value = selectedProduct;
                cmd.Parameters.Add(productIdInsert);

                var numberToAddInsert = new SqlParameter("@numberToAdd", SqlDbType.Int);
                numberToAddInsert.Value = numberToAdd;
                cmd.Parameters.Add(numberToAddInsert);

                var result = cmd.ExecuteNonQuery();

                return result == 1;
            }
        }
    }
}
