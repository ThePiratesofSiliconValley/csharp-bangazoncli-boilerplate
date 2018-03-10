using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_bangazoncli.app.DataAccess.Models
{
    class ProductsModel
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }

    }
}
