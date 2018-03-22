using SupermarketCheckout.Server.DTOs;
using SupermarketCheckout.Server.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SupermarketCheckout.Server.Api.Controllers
{
    /// <summary>
    /// Manage your basket
    /// </summary>
    public class BasketController : ApiController
    {
        IBasketService BasketService { get; }

        /// <summary>
        /// Init basket controller
        /// </summary>
        /// <param name="basketService"></param>
        public BasketController(IBasketService basketService)
        {
            BasketService = basketService;
        }

        /// <summary>
        /// Submit a basket to get the price
        /// </summary>
        /// <remarks>
        /// Submit a basket with product SKUs as the body. This will return details of the products, any applied discounts, and the total price
        /// </remarks>
        /// <param name="basketIn">The basket model</param>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(BasketOutDTO))]
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
