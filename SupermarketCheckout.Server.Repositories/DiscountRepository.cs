using SupermarketCheckout.Server.IRepositories;
using SupermarketCheckout.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Server.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        public static List<Discount> discounts = new List<Discount>(); // Would normally have a db

        public void AddOrUpdate(Discount discount)
        {
            discounts.RemoveAll(x => x.Id == discount.Id);
            discounts.Add(discount);
        }

        public void Delete(Guid id)
        {
            discounts.RemoveAll(x => x.Id == id);
        }

        public Discount Get(Guid id)
        {
            return discounts.SingleOrDefault(x => x.Id == id);
        }

        public IList<Discount> GetAll()
        {
            return discounts;
        }

        public List<Discount> GetAllForSKUs(IEnumerable<string> skus)
        {
            return discounts.Where(x => skus.Contains(x.ProductSKU)).ToList();
        }
    }
}
