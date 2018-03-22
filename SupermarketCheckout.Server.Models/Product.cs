using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Server.Models
{
    public class Product
    {
        public string SKU { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
