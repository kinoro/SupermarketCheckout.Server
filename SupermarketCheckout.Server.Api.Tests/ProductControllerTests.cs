using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupermarketCheckout.Server.Api.Controllers;
using SupermarketCheckout.Server.DTOs;
using SupermarketCheckout.Server.Repositories;
using SupermarketCheckout.Server.Services;

namespace SupermarketCheckout.Server.Api.Tests
{
    [TestClass]
    public class ProductControllerTests
    {
        ProductController controller;

        [TestInitialize]
        public void Init()
        {
            controller = new ProductController(new ProductService(new ProductRepository()));
        }

        [TestMethod]
        public async Task AddProduct_IsSuccess()
        {
            var product = new ProductDTO
            {
                SKU = "TEST_A01",
                Description = "Apple",
                Price = 0.99m
            };

            var result = await controller.PostAsync(product);
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<ProductDTO>));

            var response = result as CreatedNegotiatedContentResult<ProductDTO>;
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(product.SKU, response.Content.SKU);
            Assert.AreEqual(product.Description, response.Content.Description);
            Assert.AreEqual(product.Price, response.Content.Price);
        }

        [TestMethod]
        public async Task AddMultipleProductsWithSameSKU_IsFail()
        {
            var product = new ProductDTO
            {
                SKU = "TEST_A02",
                Description = "Apple",
                Price = 0.99m
            };

            var result = await controller.PostAsync(product);
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<ProductDTO>));

            result = await controller.PostAsync(product);
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
            var response = result as BadRequestErrorMessageResult;
            Assert.AreEqual(string.Format(ProductService.ERROR_DUPLICATE_SKU, product.SKU), response.Message);
        }

        [TestMethod]
        public async Task GetProduct_IsSuccess()
        {
            var product = new ProductDTO
            {
                SKU = "TEST_A03",
                Description = "Apple",
                Price = 0.99m
            };

            var result = await controller.PostAsync(product);
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<ProductDTO>));

            result = await controller.GetAsync(product.SKU);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<ProductDTO>));

            var response = result as OkNegotiatedContentResult<ProductDTO>;
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(product.SKU, response.Content.SKU);
            Assert.AreEqual(product.Description, response.Content.Description);
            Assert.AreEqual(product.Price, response.Content.Price);
        }

        [TestMethod]
        public async Task GetProducts_IsSuccess()
        {
            var product1 = new ProductDTO
            {
                SKU = "TEST_A04",
                Description = "Apple",
                Price = 0.99m
            };

            var product2 = new ProductDTO
            {
                SKU = "TEST_P01",
                Description = "Pear",
                Price = 0.79m
            };

            var result = await controller.PostAsync(product1);
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<ProductDTO>));
            result = await controller.PostAsync(product2);
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<ProductDTO>));

            result = await controller.GetAsync();
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<List<ProductDTO>>));

            var response = result as OkNegotiatedContentResult<List<ProductDTO>>;
            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.Content.Count > 1);
        }
    }
}
