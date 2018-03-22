using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Server.DTOs
{
    [Serializable]
    public class BasketInDTO
    {
        public List<string> ProductSKUs { get; set; }
    }
}
