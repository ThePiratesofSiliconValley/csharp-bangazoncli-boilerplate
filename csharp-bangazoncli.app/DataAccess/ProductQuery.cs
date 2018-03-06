using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_bangazoncli.app.DataAccess
{
    class ProductQuery
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;

        //public List<Product> GetAllProducts()
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {

        //    }
        //}
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public int CustomerId { get; set; }
    }
}
