using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Server.DTOs
{
    [Serializable]
    public class AppliedDiscountDTO
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
