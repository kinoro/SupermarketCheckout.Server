using SupermarketCheckout.Server.IRepositories;
using SupermarketCheckout.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Server.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public static List<Product> products = new List<Product>(); // Would normally have a db

        public void AddOrUpdate(Product product)
        {
            products.RemoveAll(x => x.SKU == product.SKU);
            products.Add(product);
        }

        public void Delete(string sku)
        {
            products.RemoveAll(x => x.SKU == sku);
        }

        public Product Get(string sku)
        {
            return products.SingleOrDefault(x => x.SKU == sku);
        }

        public IList<Product> GetAll()
        {
            return products;
        }
    }
}
