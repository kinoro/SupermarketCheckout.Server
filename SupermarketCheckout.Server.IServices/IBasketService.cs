using SupermarketCheckout.Server.DTOs;
using SupermarketCheckout.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Server.IServices
{
    public interface IBasketService
    {
        Task<BasketOutDTO> CalculatePrice(BasketInDTO basketIn);
    }
}
