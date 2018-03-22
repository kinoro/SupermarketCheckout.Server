using SupermarketCheckout.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Server.DTOs
{
    [Serializable]
    public class DiscountDTO : IValidatableObject
    {
        public Guid Id { get; set; }

        [Required]
        public string ProductSKU { get; set; }

        [Required]
        [Range(1, 99)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }


        // In reality this mapping would more likely be done using something like AutoMapper
        public Discount ToModel()
        {
            var discount = new Discount
            {
                Id = this.Id,
                ProductSKU = this.ProductSKU,
                Quantity = this.Quantity,
                Price = this.Price
            };
            return discount;
        }

        public static DiscountDTO FromModel(Discount discount)
        {
            var discountDTO = new DiscountDTO
            {
                Id = discount.Id,
                ProductSKU = discount.ProductSKU,
                Quantity = discount.Quantity,
                Price = discount.Price
            };
            return discountDTO;
        }

        public static List<DiscountDTO> FromModels(List<Discount> discounts)
        {
            var discountDTOs = discounts
                .Select(x => DiscountDTO.FromModel(x))
                .ToList();

            return discountDTOs;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Price >= Quantity)
            {
                yield return new ValidationResult(
                    "Price must be less than Quantity",
                    new string[] { "Price" }
                    );
            }
        }
    }
}
