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
    public class BasketService : IBasketService
    {
        public const string ERROR_INVALID_BASKET_PRODUCT = "Invalid product in basket = SKU {0} not found";

        IAppliedDiscountService AppliedDiscountService { get; }
        IProductRepository ProductRepository { get; }

        public BasketService(IAppliedDiscountService appliedDiscountService, IProductRepository productRepository)
        {
            AppliedDiscountService = appliedDiscountService;
            ProductRepository = productRepository;
        }

        public Task<BasketOutDTO> CalculatePrice(BasketInDTO basketIn)
        {
            // First load all the products
            var products = GetBasketProducts(basketIn);
            var productsPrice = products.Sum(x => x.Price);

            // Now determine the discounts that can be applied
            var appliedDiscounts = AppliedDiscountService.GetAppliedDiscounts(products);
            var appliedDiscountsAmount = appliedDiscounts.Sum(x => x.Amount);

            // Finally wrap up and return
            var basketOut = new BasketOutDTO
            {
                Products = products,
                AppliedDiscounts = appliedDiscounts,
                TotalPrice = productsPrice + appliedDiscountsAmount
            };

            return Task.FromResult<BasketOutDTO>(basketOut);
        }

        private List<ProductDTO> GetBasketProducts(BasketInDTO basketIn)
        {
            var productDTOs = new List<ProductDTO>();
            foreach (var sku in basketIn.ProductSKUs)
            {
                var product = ProductRepository.Get(sku);
                if (product == null)
                {
                    throw new Exception(string.Format(ERROR_INVALID_BASKET_PRODUCT, sku));
                }

                productDTOs.Add(ProductDTO.FromModel(product));
            }

            return productDTOs;
        }

    }
}
