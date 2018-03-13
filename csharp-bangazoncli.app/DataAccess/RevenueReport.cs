using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_bangazoncli.app.DataAccess
{
    class RevenueReport
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;

        internal void GetTop3Revenue()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();

                cmd.CommandText = @"with OrderItemsCte (productID, quantity) as 
                                    (
	                                    select productId, SUM(quantity) as TotalQuantity from OrderLine
	                                    group by ProductId
                                    ),
                                    TotalOrdersCte (productId, orders) as
                                    (
	                                    select productId, count(orderId) as TotalOrders from OrderLine
	                                    group by ProductId
                                    ),
                                    TotalCustomersCte (productId, totalCustomers) as
                                    (
	                                    select p.productId, count(p.customerId) as customerCount from Products p
	                                    join orders o on o.CustomerId = p.CustomerId
	                                    join TotalOrdersCte toc on toc.productId = p.ProductId
	                                    group by p.ProductId
                                    ),
                                    OrderTotalsCte (ProductId, totalGross, productprice, productname, purchasers) as
                                    (
	                                    select p.productId, oi.Quantity * p.ProductPrice, p.ProductPrice as totalGross, p.ProductName, tc.totalCustomers from OrderItemsCTE oi
	                                    join products p on p.ProductId = oi.productID
	                                    join TotalCustomersCte tc on tc.productId = oi.productID
                                    ),
                                    TotalGrossCte (quantity, productId, totalGross, productprice, productname, totalpurchasers) as
                                    (
	                                    select oi.quantity, oi.productId, ot.totalGross, ot.productprice, ot.productname, ot.purchasers from OrderItemsCte oi
	                                    join OrderTotalsCTE ot on ot.productID = oi.productid
                                    )
	                                    select top(3) tg.totalGross, tg.productName, tg.totalpurchasers, toc.orders from TotalGrossCte tg
	                                    join TotalOrdersCte toc on toc.productId = tg.productId
	                                    order by totalGross desc";
                connection.Open();

                var reader = cmd.ExecuteReader();
                var Top3Grossers = new List<TopGrosser>();

                while (reader.Read())
                {
                    var topGrosser = new TopGrosser
                    {
                        TotalGross = decimal.Parse(reader["totalGross"].ToString()),
                        ProductName = reader["productName"].ToString(),
                        TotalPurchasers = int.Parse(reader["totalpurchasers"].ToString()),
                        TotalOrders = int.Parse(reader["orders"].ToString())
                    };
                    Top3Grossers.Add(topGrosser);
                }

                foreach (var grosser in Top3Grossers)
                {
                    Console.WriteLine($"{grosser.ProductName}: {grosser.TotalOrders}: {grosser.TotalPurchasers}: {grosser.TotalGross}");
                }
                Console.ReadKey();
            }
        }
    }

    internal class TopGrosser
    {
        public decimal TotalGross { get; set; }
        public string ProductName { get; set; }
        public int TotalPurchasers { get; set; }
        public int TotalOrders { get; set; }
    }
}
