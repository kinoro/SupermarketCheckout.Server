using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Server.Models
{
    public class Discount
    {
        public Guid Id { get; set; }
        public string ProductSKU { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
