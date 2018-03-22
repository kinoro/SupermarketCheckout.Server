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
    /// Create and fetch products
    /// </summary>
    public class ProductController : ApiController
    {
        private IProductService ProductService { get; }

        /// <summary>
        /// Init product controller
        /// </summary>
        /// <param name="productService"></param>
        public ProductController(IProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(IEnumerable<ProductDTO>))]
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

        /// <summary>
        /// Get a specific product
        /// </summary>
        /// <param name="id">The product SKU</param>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(ProductDTO))]
        public async Task<IHttpActionResult> GetAsync(string id)
        {
            ProductDTO foundProductDTO = null;
            try
            {
                foundProductDTO = await ProductService.GetAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok<ProductDTO>(foundProductDTO);
        }

        /// <summary>
        /// Create a product
        /// </summary>
        /// <param name="productDTO">The product to create</param>
        /// <returns></returns>
        /// <response code="201"></response>
        [ResponseType(typeof(ProductDTO))]
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
