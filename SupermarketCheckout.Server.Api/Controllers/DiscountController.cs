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
    /// Create and update discounts
    /// </summary>
    public class DiscountController : ApiController
    {
        private IDiscountService DiscountService { get; }

        /// <summary>
        /// Init discount controller
        /// </summary>
        /// <param name="discountService"></param>
        public DiscountController(IDiscountService discountService)
        {
            DiscountService = discountService;
        }

        /// <summary>
        /// Create a discount
        /// </summary>
        /// <param name="discountDTO">The discount model</param>
        /// <returns></returns>
        /// <response code="201"></response>
        [ResponseType(typeof(DiscountDTO))]
        public async Task<IHttpActionResult> PostAsync(DiscountDTO discountDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            DiscountDTO createdDiscountDTO = null;
            try
            {
                createdDiscountDTO = await DiscountService.AddAsync(discountDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created<DiscountDTO>("discount/" + discountDTO.Id, createdDiscountDTO);
        }

        /// <summary>
        /// Update a discount
        /// </summary>
        /// <param name="discountDTO">The discount model</param>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(DiscountDTO))]
        public async Task<IHttpActionResult> PutAsync(DiscountDTO discountDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            DiscountDTO updatedDiscountDTO = null;
            try
            {
                updatedDiscountDTO = await DiscountService.UpdateAsync(discountDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok<DiscountDTO>(updatedDiscountDTO);
        }
    }
}
