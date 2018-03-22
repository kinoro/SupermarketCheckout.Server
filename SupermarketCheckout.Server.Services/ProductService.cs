using SupermarketCheckout.Server.DTOs;
using SupermarketCheckout.Server.IRepositories;
using SupermarketCheckout.Server.IServices;
using SupermarketCheckout.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Server.Services
{
    public class ProductService : IProductService
    {
        public const string ERROR_DUPLICATE_SKU = "Cannot add product - SKU {0} already exists";
        public const string ERROR_NOT_FOUND = "Could not find product with SKU {0}";

        private IProductRepository Products { get; set; }

        public ProductService(IProductRepository productRepository)
        {
            Products = productRepository;
        }

        public Task<ProductDTO> AddAsync(ProductDTO productDTO)
        {
            var existingProduct = Products.Get(productDTO.SKU);
            if (existingProduct != null)
            {
                throw new Exception(string.Format(ERROR_DUPLICATE_SKU, productDTO.SKU));
            }

            var product = productDTO.ToModel();
            Products.AddOrUpdate(product);

            return Task.FromResult<ProductDTO>(ProductDTO.FromModel(product));
        }

        public Task<ProductDTO> GetAsync(string sku)
        {
            var product = Products.Get(sku);
            if (product == null)
            {
                throw new Exception(string.Format(ERROR_NOT_FOUND, sku));
            }

            return Task.FromResult(ProductDTO.FromModel(product));
        }

        public Task<List<ProductDTO>> GetAllAsync()
        {
            var productDTOs = Products.GetAll().Select(x => ProductDTO.FromModel(x));
            return Task.FromResult(productDTOs.ToList());
        }
    }
}
