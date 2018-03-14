using ConsoleTables;
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

                cmd.CommandText = @"select top 3 sum(p.ProductPrice*ol.Quantity) as totalGross,p.productName,p.ProductId, count(*) as                             TotalOrders, count(distinct(o.CustomerId)) as totalCustomers
                                    from Orders o join OrderLine ol on ol.OrderId = o.OrderId join Products p on p.ProductId = ol.ProductId
                                    group by p.ProductName,p.ProductId
                                    order by 1 desc";
                connection.Open();

                var reader = cmd.ExecuteReader();
                var Top3Grossers = new List<TopGrosser>();

                while (reader.Read())
                {
                    var topGrosser = new TopGrosser
                    {
                        TotalGross = Math.Round(Convert.ToDecimal(reader["totalGross"].ToString()), 2),
                        ProductName = reader["productName"].ToString(),
                        TotalPurchasers = int.Parse(reader["totalcustomers"].ToString()),
                        TotalOrders = int.Parse(reader["totalorders"].ToString())
                    };
                    Top3Grossers.Add(topGrosser);
                }

                BuildMyTable(Top3Grossers);
            }
        }

        private void BuildMyTable(List<TopGrosser> top3Grossers)
        {
            Console.WriteLine("Here are the top 3 grossing products.");
            var table = new ConsoleTable("Product", "Orders", "Purchasers", "Revenue");

            foreach (var product in top3Grossers)
            {
                var truncatedProduct = Truncate(product.ProductName, 15);

                table.AddRow(truncatedProduct, product.TotalOrders, product.TotalPurchasers, $"${product.TotalGross}");
            }

            table.Write(Format.Alternative);
            Console.WriteLine("Press any key to navigate back to the main menu.");
            Console.ReadKey();
        }


        string Truncate(string value, int maxLength)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
            {
                return value.Substring(0, maxLength) + "...";
            }

            return value;
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
