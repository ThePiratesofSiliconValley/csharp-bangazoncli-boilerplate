using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csharp_bangazoncli.app.DataAccess.Models;

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

        internal bool UpdateProduct(Product updateThisProduct)
        {
            Console.WriteLine("What would you like to update?");
            Console.WriteLine($"1. Change title \"{updateThisProduct.ProductName}\"");
            Console.WriteLine($"2. Change description \"{updateThisProduct.ProductDescription}\"");
            Console.WriteLine($"3. Change quantity \"{updateThisProduct.Quantity}\"");
            Console.WriteLine($"4. Change price \"{updateThisProduct.ProductPrice}\"");
            var updateThisField = int.Parse(Console.ReadLine().ToString());
            Console.WriteLine("What would you like to update it to?");
            var updateValue = Console.ReadLine();
            

            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                connection.Open();
                int result;
                switch (updateThisField)
                {
                    case 1:
                        cmd.CommandText = @"update products
                                            set productname = @productName
                                            where productname = @oldName";
                        var updateName = new SqlParameter("@productName", SqlDbType.NVarChar);
                        updateName.Value = updateValue.ToString();
                        cmd.Parameters.Add(updateName);

                        var origName = new SqlParameter("@oldName", SqlDbType.NVarChar);
                        origName.Value = updateThisProduct.ProductName;
                        cmd.Parameters.Add(origName);

                        result = cmd.ExecuteNonQuery();
                        return result == 1;
                    case 2:
                        cmd.CommandText = @"update products
                                            set productdescription = @productDescription
                                            where productproductdescription = @oldDescription";
                        var updateDescription = new SqlParameter("@productDescription", SqlDbType.NVarChar);
                        updateDescription.Value = updateValue.ToString();
                        cmd.Parameters.Add(updateDescription);

                        var origDescription = new SqlParameter("@oldName", SqlDbType.NVarChar);
                        origDescription.Value = updateThisProduct.ProductName;
                        cmd.Parameters.Add(origDescription);

                        result = cmd.ExecuteNonQuery();
                        return result == 1;
                    case 3:
                        cmd.CommandText = @"update products
                                            set productdescription = @productDescription
                                            where productproductdescription = @oldDescription";
                        var updateDescription = new SqlParameter("@productDescription", SqlDbType.NVarChar);
                        updateDescription.Value = updateValue.ToString();
                        cmd.Parameters.Add(updateDescription);

                        var origDescription = new SqlParameter("@oldName", SqlDbType.NVarChar);
                        origDescription.Value = updateThisProduct.ProductName;
                        cmd.Parameters.Add(origDescription);

                        result = cmd.ExecuteNonQuery();
                        return result == 1;
                    case 4:
                        cmd.CommandText = @"update products
                                            set productdescription = @productDescription
                                            where productproductdescription = @oldDescription";
                        var updateDescription = new SqlParameter("@productDescription", SqlDbType.NVarChar);
                        updateDescription.Value = updateValue.ToString();
                        cmd.Parameters.Add(updateDescription);

                        var origDescription = new SqlParameter("@oldName", SqlDbType.NVarChar);
                        origDescription.Value = updateThisProduct.ProductName;
                        cmd.Parameters.Add(origDescription);

                        result = cmd.ExecuteNonQuery();
                        return result == 1;
                }


            }


        }

    }
}
