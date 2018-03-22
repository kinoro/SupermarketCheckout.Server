using SupermarketCheckout.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Server.IRepositories
{
    public interface IDiscountRepository
    {
        void AddOrUpdate(Discount discount);
        Discount Get(Guid id);
        IList<Discount> GetAll();
        void Delete(Guid id);
        List<Discount> GetAllForSKUs(IEnumerable<string> skus);
    }
}
