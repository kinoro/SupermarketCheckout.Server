using SupermarketCheckout.Server.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Server.IServices
{
    public interface IAppliedDiscountService
    {
        List<AppliedDiscountDTO> GetAppliedDiscounts(List<ProductDTO> allProducts);
    }
}
