using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupermarketCheckout.Server.Api.Controllers;
using SupermarketCheckout.Server.DTOs;
using SupermarketCheckout.Server.Models;
using SupermarketCheckout.Server.Repositories;
using SupermarketCheckout.Server.Services;

namespace SupermarketCheckout.Server.Api.Tests
{
    [TestClass]
    public class BasketControllerTests
    {
        BasketController controller;

        [TestInitialize]
        public void Init()
        {
            controller =    new BasketController(
                                new BasketService(
                                    new AppliedDiscountService(new DiscountRepository()),
                                    new ProductRepository()
                                )
                            );
        }

        [TestMethod]
        public async Task CalculateBasicPrice_IsSuccess()
        {
            var basket = new BasketInDTO
            {
                ProductSKUs = new List<string> { "TST_A01", "TST_T23" }
            };

            var result = await controller.PostAsync(basket);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<BasketOutDTO>));

            var basketOut = ((OkNegotiatedContentResult<BasketOutDTO>)result).Content;
            Assert.AreEqual(1.49m, basketOut.TotalPrice);
            Assert.AreEqual(basket.ProductSKUs.Count, basketOut.Products.Count);
            Assert.AreEqual(0, basketOut.AppliedDiscounts.Count);
        }

        [TestMethod]
        public async Task CalculateDiscountedPrice_IsSuccess()
        {
            var basket = new BasketInDTO
            {
                ProductSKUs = new List<string> { "TST_A01", "TST_A01", "TST_T23", "TST_A01" }
            };

            var result = await controller.PostAsync(basket);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<BasketOutDTO>));

            var basketOut = ((OkNegotiatedContentResult<BasketOutDTO>)result).Content;
            Assert.AreEqual(2.29m, basketOut.TotalPrice);
            Assert.AreEqual(basket.ProductSKUs.Count, basketOut.Products.Count);
            Assert.AreEqual(1, basketOut.AppliedDiscounts.Count);
        }

        [TestMethod]
        public async Task CalculateDiscountedPriceWithMixedDiscounts_IsSuccess()
        {
            var basket = new BasketInDTO
            {
                ProductSKUs = new List<string> { "TST_B15", "TST_A01", "TST_A01", "TST_B15", "TST_T23", "TST_A01", "TST_B15" }
            };
            
            var result = await controller.PostAsync(basket);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<BasketOutDTO>));

            var basketOut = ((OkNegotiatedContentResult<BasketOutDTO>)result).Content;
            Assert.AreEqual(3.04m, basketOut.TotalPrice);
            Assert.AreEqual(basket.ProductSKUs.Count, basketOut.Products.Count);
            Assert.AreEqual(2, basketOut.AppliedDiscounts.Count);
        }

        [TestMethod]
        public async Task CalculateDiscountedPriceWithMultipleSameDiscount_IsSuccess()
        {
            var basket = new BasketInDTO
            {
                ProductSKUs = new List<string> { "TST_A01", "TST_A01", "TST_A01", "TST_A01", "TST_A01", "TST_A01", "TST_A01", "TST_A01", }
            };

            var result = await controller.PostAsync(basket);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<BasketOutDTO>));

            var basketOut = ((OkNegotiatedContentResult<BasketOutDTO>)result).Content;
            Assert.AreEqual(3.6m, basketOut.TotalPrice);
            Assert.AreEqual(basket.ProductSKUs.Count, basketOut.Products.Count);
            Assert.AreEqual(2, basketOut.AppliedDiscounts.Count);
        }
    }
}
