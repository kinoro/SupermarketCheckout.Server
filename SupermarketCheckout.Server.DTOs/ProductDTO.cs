using SupermarketCheckout.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Server.DTOs
{
    public class ProductDTO
    {
        [MinLength(3)]
        public string SKU { get; set; }

        [MinLength(1)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        // In reality this mapping would more likely be done using something like AutoMapper
        public Product ToModel()
        {
            var product = new Product
            {
                SKU = this.SKU,
                Description = this.Description,
                Price = this.Price
            };
            return product;
        }

        public static ProductDTO FromModel(Product product)
        {
            var productDTO = new ProductDTO
            {
                SKU = product.SKU,
                Description = product.Description,
                Price = product.Price
            };
            return productDTO;
        }

        public static List<ProductDTO> FromModels(List<Product> products)
        {
            var productDTOs = products
                .Select(x => ProductDTO.FromModel(x))
                .ToList();

            return productDTOs;
        }
    }
}
