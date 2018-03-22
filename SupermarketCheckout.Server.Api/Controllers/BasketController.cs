using SupermarketCheckout.Server.DTOs;
using SupermarketCheckout.Server.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SupermarketCheckout.Server.Api.Controllers
{
    public class BasketController : ApiController
    {
        IBasketService BasketService { get; }

        public BasketController(IBasketService basketService)
        {
            BasketService = basketService;
        }

        public async Task<IHttpActionResult> PostAsync(BasketInDTO basketIn)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            BasketOutDTO basketOut = null;
            try
            {
                basketOut = await BasketService.CalculatePrice(basketIn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok<BasketOutDTO>(basketOut);
        }
    }
}
