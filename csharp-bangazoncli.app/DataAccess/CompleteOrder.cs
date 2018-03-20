using csharp_bangazoncli.app.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace csharp_bangazoncli.app.DataAccess
{
    class CompleteOrder
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;

        public List<OrderDetailsModel> GetAllCustomerOrders(int customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"Select * from Orders where customerId = @customerId";

                var customerIdParam = new SqlParameter("@customerId", SqlDbType.Int);
                customerIdParam.Value = customerId;

                cmd.Parameters.Add(customerIdParam);
                var reader = cmd.ExecuteReader();
                var allOrdersForSelectedCustomer = new List<OrderDetailsModel>();
                while (reader.Read())
                {
                    var selectedCustomerOrders = new OrderDetailsModel
                    {
                        OrderId = int.Parse(reader["OrderId"].ToString()),
                    };

                    allOrdersForSelectedCustomer.Add(selectedCustomerOrders);
                }
                return allOrdersForSelectedCustomer;
            }
        }

        

        public void DisplayOrderDetails(int orderId, int customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select ol.orderid,
		                                p.productname,
		                                ol.quantity,
		                                p.productprice,
		                                ol.quantity * p.productprice as totalProductPrice
                                    from orderline ol
                                        join orders o
                                        on o.OrderId = ol.OrderId
		                                join products p
		                                on ol.productid = p.productid
                                        where ol.OrderId = @orderId
                                        and o.customerId = @customerId
                                        order by ol.orderid";

                var orderIdParam = new SqlParameter("@orderId", SqlDbType.Int);
                orderIdParam.Value = orderId;
                cmd.Parameters.Add(orderIdParam);

                var customerIdParam = new SqlParameter("@customerId", SqlDbType.Int);
                customerIdParam.Value = customerId;
                cmd.Parameters.Add(customerIdParam);

                var reader = cmd.ExecuteReader();
                var totalOrder = new List<OrderDetailsModel>();



                while (reader.Read())
                {
                    var orderDetails = new OrderDetailsModel
                    {
                        ProductName = reader["ProductName"].ToString(),
                        Quantity = int.Parse(reader["Quantity"].ToString()),
                        ProductPrice = decimal.Parse(reader["ProductPrice"].ToString()),
                        TotalProductPrice = decimal.Parse(reader["TotalProductPrice"].ToString())
                    };

                    

                    totalOrder.Add(orderDetails);
                }


                foreach (var orders in totalOrder)
                {
                    Console.WriteLine($"Product: {orders.ProductName}      Quantity: {orders.Quantity}       Total Price: {orders.TotalProductPrice}");
                }
                
            }
        }

        public void TotalPriceOfOrder(int orderId, int customerId)
        {
                var orderTotalInfo = new CompleteOrderModel();
                var orderDetails = new List<OrderDetailsModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select ol.orderid,
		                                SUM(ol.quantity * p.productprice) as totalOrderPrice,
                                        SUM(ol.quantity) as TotalProducts
                                    from orderline ol
		                            join products p
		                                on p.productid = ol.productid
                                    where ol.orderid = @orderid
                                    group by ol.orderid";

                var orderIdParam = new SqlParameter("@orderId", SqlDbType.Int);
                orderIdParam.Value = orderId;
                cmd.Parameters.Add(orderIdParam);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    orderTotalInfo.OrderId = int.Parse(reader["OrderId"].ToString());
                    orderTotalInfo.TotalOrderPrice = decimal.Parse(reader["TotalOrderPrice"].ToString());
                    orderTotalInfo.TotalProducts = int.Parse(reader["TotalProducts"].ToString());
                }

                Console.WriteLine($"Your order total is: {orderTotalInfo.TotalOrderPrice}.  Are you ready to Purchase: (y/n)");
            }
            var userSelection = Console.ReadKey();

            if (userSelection.KeyChar == 'y')
            {
                using (var connection1 = new SqlConnection(_connectionString))
                {
                    connection1.Open();
                    var cmd1 = connection1.CreateCommand();
                    cmd1.CommandText = @"select * from PmtType	
                                                 where customerid = @customerId";

                    var customerIdParam = new SqlParameter("@customerId", SqlDbType.Int);
                    customerIdParam.Value = customerId;
                    cmd1.Parameters.Add(customerIdParam);
                    var reader1 = cmd1.ExecuteReader();
                    Console.WriteLine("Choose a Payment Option:");
                    while (reader1.Read())
                    {
                        var paymentTypes = new OrderDetailsModel
                        {
                            PaymentType = reader1["PmtType"].ToString(),
                            PaymentTypeId = int.Parse(reader1["PmtTypeId"].ToString())

                        };

                        orderDetails.Add(paymentTypes);
                    }


                    var paymentCounter = 0;
                    foreach (var orderDetail in orderDetails)
                    {
                        paymentCounter++;
                        Console.WriteLine($"{paymentCounter}. {orderDetail.PaymentType}");
                    }

                    if(paymentCounter <= 0)
                    {
                        Console.WriteLine("You must add a payment type in order to complete your order. You will be returned to the main menu.");
                        Console.ReadKey();                    
                    }
                    else
                    {
                        var selectedPaymentType = int.Parse(Console.ReadLine().ToString());
                        cmd1.CommandText = @"UPDATE Orders
                                    SET TotalPrice = @totalPrice
                                    ,PaymentTypeId = @paymentTypeId
                                    ,TotalProducts = @totalProducts
                                    WHERE OrderId = @orderId";

                        var orderIdParam = new SqlParameter("@orderId", SqlDbType.Int);
                        orderIdParam.Value = orderId;
                        cmd1.Parameters.Add(orderIdParam);

                        var paymentTypeIdParam = new SqlParameter("@paymentTypeId", SqlDbType.Int);
                        paymentTypeIdParam.Value = orderDetails[selectedPaymentType - 1].PaymentTypeId;
                        cmd1.Parameters.Add(paymentTypeIdParam);

                        var totalPriceParam = new SqlParameter("@totalPrice", SqlDbType.Money);
                        totalPriceParam.Value = orderTotalInfo.TotalOrderPrice;
                        cmd1.Parameters.Add(totalPriceParam);

                        var totalProductsParam = new SqlParameter("@totalProducts", SqlDbType.Int);
                        totalProductsParam.Value = orderTotalInfo.TotalProducts;
                        cmd1.Parameters.Add(totalProductsParam);

                        var result = cmd1.ExecuteNonQuery();
                    }
                    Console.WriteLine("Your order is complete!  Please visit our wonderful console app again for your future needs!");
                }

            }
            if (userSelection.KeyChar == 'n')
            {
                Console.WriteLine("You will be returned to the main menu.");
            }

        }

        //using (var connection2 = new SqlConnection(_connectionString))
        //{var cmd2 = connection2.CreateCommand();
        //    var selectedPaymentType = int.Parse(Console.ReadLine().ToString());
        //    connection2.Open();



    }
}
