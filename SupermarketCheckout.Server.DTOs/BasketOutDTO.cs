using SupermarketCheckout.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Server.DTOs
{
    public class BasketOutDTO
    {
        public List<ProductDTO> Products { get; set; }
        public List<AppliedDiscountDTO> AppliedDiscounts { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
