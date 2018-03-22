using SupermarketCheckout.Server.DTOs;
using SupermarketCheckout.Server.IRepositories;
using SupermarketCheckout.Server.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Server.Services
{
    public class DiscountService : IDiscountService
    {
        public const string ERROR_DISCOUNT_NOT_FOUND = "Discount with id {0} not found";
        public const string ERROR_INVALID_PRODUCT_SKU = "Cannot create discount - Product with SKU {0} not found";

        private IDiscountRepository Discounts { get; set; }
        private IProductRepository Products { get; set; }

        public DiscountService(IDiscountRepository discountRepository, IProductRepository productRepository)
        {
            Discounts = discountRepository;
            Products = productRepository;
        }

        public Task<DiscountDTO> AddAsync(DiscountDTO discountDTO)
        {
            CheckValidProductSKU(discountDTO);

            var discount = discountDTO.ToModel();
            discount.Id = Guid.NewGuid();
            Discounts.AddOrUpdate(discount);

            return Task.FromResult<DiscountDTO>(DiscountDTO.FromModel(discount));
        }

        public Task<DiscountDTO> UpdateAsync(DiscountDTO discountDTO)
        {
            var existingDiscount = Discounts.Get(discountDTO.Id);
            if (existingDiscount == null)
            {
                throw new Exception(string.Format(ERROR_DISCOUNT_NOT_FOUND, discountDTO.Id));
            }

            CheckValidProductSKU(discountDTO);

            Discounts.AddOrUpdate(discountDTO.ToModel());

            return Task.FromResult<DiscountDTO>(discountDTO);
        }

        private void CheckValidProductSKU(DiscountDTO discountDTO)
        {
            var foundProduct = Products.Get(discountDTO.ProductSKU);
            if (foundProduct == null)
            {
                throw new Exception(string.Format(ERROR_INVALID_PRODUCT_SKU, discountDTO.ProductSKU));
            }
        }
    }
}
