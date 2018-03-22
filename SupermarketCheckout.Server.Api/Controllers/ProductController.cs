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
    public class ProductController : ApiController
    {
        private IProductService ProductService { get; }

        public ProductController(IProductService productService)
        {
            ProductService = productService;
        }

        public async Task<IHttpActionResult> GetAsync()
        {
            List<ProductDTO> foundProductDTOs = new List<ProductDTO>();
            try
            {
                foundProductDTOs = await ProductService.GetAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok<List<ProductDTO>>(foundProductDTOs);
        }

        public async Task<IHttpActionResult> GetAsync(string sku)
        {
            ProductDTO foundProductDTO = null;
            try
            {
                foundProductDTO = await ProductService.GetAsync(sku);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok<ProductDTO>(foundProductDTO);
        }

        public async Task<IHttpActionResult> PostAsync(ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ProductDTO createdProductDTO = null;
            try
            {
                createdProductDTO = await ProductService.AddAsync(productDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created<ProductDTO>("product/"+ productDTO.SKU, createdProductDTO);
        }
    }
}
