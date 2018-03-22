using SupermarketCheckout.Server.Models;
using SupermarketCheckout.Server.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SupermarketCheckout.Server.Api.App_Start
{
    public static class DatabaseInitializer
    {
        public static void Build()
        {
            var productRepository = new ProductRepository();

            productRepository.AddOrUpdate(new Product
            {
                SKU = "A99",
                Description = "Apple",
                Price = 0.5m
            });

            productRepository.AddOrUpdate(new Product
            {
                SKU = "B15",
                Description = "Biscuits",
                Price = 0.3m
            });

            productRepository.AddOrUpdate(new Product
            {
                SKU = "C40",
                Description = "Coffee",
                Price = 1.8m
            });

            productRepository.AddOrUpdate(new Product
            {
                SKU = "T23",
                Description = "Tissues",
                Price = 0.99m
            });
        }
    }
}