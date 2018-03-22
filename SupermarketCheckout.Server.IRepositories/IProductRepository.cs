using SupermarketCheckout.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Server.IRepositories
{
    public interface IProductRepository
    {
        void AddOrUpdate(Product product);
        Product Get(string sku);
        IList<Product> GetAll();
        void Delete(string sku);
    }
}
