using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_bangazoncli.app.DataAccess.Models
{
    class RevenueResult
    {
        public string ProductName { get; set; }
		public double ProductPrice { get; set; }
		public string SellerFirstName { get; set; }
		public string SellerLastName { get; set; }
		public int ProductQuantity { get; set; }
		public int OrderId { get; set; } 
		public int OrderLineId { get; set; }
		public int ProductId { get; set; }
		public int OrderItemQuantity { get; set; }
        public double indivItemTotal { get; set; }
    }
}
