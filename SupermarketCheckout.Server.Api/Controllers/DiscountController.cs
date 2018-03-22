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
    public class DiscountController : ApiController
    {
        private IDiscountService DiscountService { get; }

        public DiscountController(IDiscountService discountService)
        {
            DiscountService = discountService;
        }

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
