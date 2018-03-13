using csharp_bangazoncli.app.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;


namespace csharp_bangazoncli.app.DataAccess
{
    class CompleteOrder
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;

        public List<CompleteOrderModel> TotalPriceOfOrder()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select ol.orderid,
		                                SUM(ol.quantity * p.productprice) as totalProductPrice
                                    from orderline ol
		                            join products p
		                                on p.productid = ol.productid
                                    group by ol.orderid";

                var reader = cmd.ExecuteReader();
                var totalOrder = new List<CompleteOrderModel>();
                while (reader.Read())
                {
                    var orderTotalInfo = new CompleteOrderModel
                    {
                        OrderId = int.Parse(reader["OrderId"].ToString()),
                        TotalProductPrice = decimal.Parse(reader["TotalProductPrice"].ToString())
                    };

                    totalOrder.Add(orderTotalInfo);
                }

                return totalOrder;
            }
        }
    }
}
