﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_bangazoncli.app.DataAccess.Models
{
    class CompleteOrderModel
    {
        public int OrderId { get; set; }
        public decimal TotalOrderPrice { get; set; }
        public int TotalProducts { get; set; }
    }
}
