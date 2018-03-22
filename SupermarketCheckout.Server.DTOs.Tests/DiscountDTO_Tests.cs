using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupermarketCheckout.Server.Models;

namespace SupermarketCheckout.Server.DTOs.Tests
{
    [TestClass]
    public class DiscountDTO_Tests
    {
        [TestMethod]
        public void DiscountDTO_IsValid()
        {
            var discount = new DiscountDTO()
            {
                ProductSKU = "TST_A01",
                Quantity = 3,
                Price = 2
            };

            var context = new ValidationContext(discount, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(discount, context, results, true);

            // Assert here
            Assert.IsTrue(isModelStateValid);
        }

        [TestMethod]
        public void DiscountDTO_MissingSKU_IsInvalid()
        {
            var discount = new DiscountDTO()
            {
                Quantity = 3,
                Price = 2
            };

            var context = new ValidationContext(discount, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(discount, context, results, true);

            // Assert here
            Assert.IsFalse(isModelStateValid);
        }

        [TestMethod]
        public void DiscountDTO_QuantityIsZero_IsInvalid()
        {
            var discount = new DiscountDTO()
            {
                ProductSKU = "TST_A01",
                Quantity = 0,
                Price = 0
            };

            var context = new ValidationContext(discount, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(discount, context, results, true);

            // Assert here
            Assert.IsFalse(isModelStateValid);
        }

        public void DiscountDTO_PriceLessThanZero_IsInvalid()
        {
            var discount = new DiscountDTO()
            {
                ProductSKU = "TST_A01",
                Quantity = 2,
                Price = -3
            };

            var context = new ValidationContext(discount, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(discount, context, results, true);

            // Assert here
            Assert.IsFalse(isModelStateValid);
        }
    }
}
