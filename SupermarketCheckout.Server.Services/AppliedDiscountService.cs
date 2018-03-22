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
    public class AppliedDiscountService : IAppliedDiscountService
    {
        private IDiscountRepository Discounts { get; set; }

        private List<Discount> potentialDiscounts;

        public AppliedDiscountService(IDiscountRepository discountRepository)
        {
            Discounts = discountRepository;
        }

        public List<AppliedDiscountDTO> GetAppliedDiscounts(List<ProductDTO> allProducts)
        {
            var appliedDiscounts = new List<AppliedDiscountDTO>();

            // Load all discounts that MIGHT apply to the list of products
            potentialDiscounts = Discounts.GetAllForSKUs(allProducts.Select(x => x.SKU));

            // While there are matching discounts available for the remainingProducts...
            var remainingProducts = new List<ProductDTO>(allProducts);
            var nextEligibleDiscount = GetNextEligibleDiscount(remainingProducts);
            while (nextEligibleDiscount != null)
            {
                // Create applied discount
                var productDTO = allProducts.First(x => x.SKU == nextEligibleDiscount.ProductSKU);
                var appliedDiscount = CreateAppliedDiscountForProduct(nextEligibleDiscount, productDTO);
                appliedDiscounts.Add(appliedDiscount);

                // Remove quantity from remaining products
                for (var i=0; i<nextEligibleDiscount.Quantity; i++)
                {
                    var productToRemove = remainingProducts.First(x => x.SKU == nextEligibleDiscount.ProductSKU);
                    remainingProducts.Remove(productToRemove);
                }

                // Refresh to find next eligible discount for remaining products
                nextEligibleDiscount = GetNextEligibleDiscount(remainingProducts);
            }

            return appliedDiscounts;
        }

        private Discount GetNextEligibleDiscount(List<ProductDTO> remainingProducts)
        {
            // For each discount, see if there are matching number of products
            foreach(var potentialDiscount in potentialDiscounts)
            {
                var numMatchingProducts = remainingProducts.Count(x => x.SKU == potentialDiscount.ProductSKU);
                if (numMatchingProducts >= potentialDiscount.Quantity)
                {
                    return potentialDiscount;
                }
            }

            return null;
        }

        private AppliedDiscountDTO CreateAppliedDiscountForProduct(Discount discount, ProductDTO productDTO)
        {
            var appliedDiscount = new AppliedDiscountDTO
            {
                Description = string.Format("{0} x {1} = {2}", discount.Quantity, productDTO.Description, discount.Price),
                Amount = discount.Price - (discount.Quantity * productDTO.Price)
            };

            return appliedDiscount;
        }
    }
}
