using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupermarketCheckout.Server.Models;
using SupermarketCheckout.Server.Repositories;

namespace SupermarketCheckout.Server.Api.Tests
{
    [TestClass]
    public class AssemblySetup
    {
        [AssemblyInitialize]
        public static void Init(TestContext tc)
        {
            var productRepository = new ProductRepository();
            productRepository.AddOrUpdate(new Product
            {
                SKU = "TST_A01",
                Description = "Apple",
                Price = 0.5m
            });

            productRepository.AddOrUpdate(new Product
            {
                SKU = "TST_B15",
                Description = "Biscuits",
                Price = 0.3m
            });

            productRepository.AddOrUpdate(new Product
            {
                SKU = "TST_C40",
                Description = "Coffee",
                Price = 1.8m
            });

            productRepository.AddOrUpdate(new Product
            {
                SKU = "TST_T23",
                Description = "Tissues",
                Price = 0.99m
            });

            var discountRepository = new DiscountRepository();

            discountRepository.AddOrUpdate(new Discount
            {
                Id = Guid.NewGuid(),
                ProductSKU = "TST_A01",
                Quantity = 3,
                Price = 1.3m
            });

            discountRepository.AddOrUpdate(new Discount
            {
                Id = Guid.NewGuid(),
                ProductSKU = "TST_B15",
                Quantity = 2,
                Price = .45m
            });
        }
    }
}
