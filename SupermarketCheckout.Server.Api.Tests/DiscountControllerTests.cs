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
    public class DiscountControllerTests
    {
        DiscountController controller;

        [TestInitialize]
        public void Init()
        {
            controller = new DiscountController(new DiscountService(new DiscountRepository(), new ProductRepository()));
        }

        [TestMethod]
        public async Task AddDiscount_IsSuccess()
        {
            var discount = new DiscountDTO()
            {
                ProductSKU = "TST_A01" ,
                Quantity = 3,
                Price = 2
            };

            var result = await controller.PostAsync(discount);
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<DiscountDTO>));

            var response = result as CreatedNegotiatedContentResult<DiscountDTO>;
            Assert.IsNotNull(response.Content);
            Assert.IsNotNull(response.Content.Id);
            Assert.AreEqual(discount.ProductSKU, response.Content.ProductSKU);
            Assert.AreEqual(discount.Quantity, response.Content.Quantity);
            Assert.AreEqual(discount.Price, response.Content.Price);
        }

        [TestMethod]
        public async Task AddDiscount_InvalidSKU_IsFail()
        {
            var discount = new DiscountDTO()
            {
                ProductSKU = "RubbishSKU",
                Quantity = 3,
                Price = 2
            };

            var result = await controller.PostAsync(discount);
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
            var response = result as BadRequestErrorMessageResult;
            Assert.AreEqual(string.Format(DiscountService.ERROR_INVALID_PRODUCT_SKU, discount.ProductSKU), response.Message);

        }

        [TestMethod]
        public async Task UpdateDiscount_IsSuccess()
        {
            var discount = new DiscountDTO()
            {
                ProductSKU = "TST_A01",
                Quantity = 3,
                Price = 2
            };

            var postResult = await controller.PostAsync(discount);
            Assert.IsInstanceOfType(postResult, typeof(CreatedNegotiatedContentResult<DiscountDTO>));

            var postResponse = postResult as CreatedNegotiatedContentResult<DiscountDTO>;
            Assert.IsNotNull(postResponse.Content);

            var createdDiscount = postResponse.Content as DiscountDTO;
            createdDiscount.Price = 1;
            var putResult = await controller.PutAsync(createdDiscount);

            var putResponse = putResult as OkNegotiatedContentResult<DiscountDTO>;
            Assert.IsNotNull(putResponse.Content);
            Assert.IsNotNull(putResponse.Content.Id);
            Assert.AreEqual(createdDiscount.ProductSKU, putResponse.Content.ProductSKU);
            Assert.AreEqual(createdDiscount.Quantity, putResponse.Content.Quantity);
            Assert.AreEqual(createdDiscount.Price, putResponse.Content.Price);
        }
    }
}
