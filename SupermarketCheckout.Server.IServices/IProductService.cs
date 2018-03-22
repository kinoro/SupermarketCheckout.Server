using SupermarketCheckout.Server.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Server.IServices
{
    public interface IProductService
    {
        Task<ProductDTO> AddAsync(ProductDTO productDTO);
        Task<ProductDTO> GetAsync(string sku);
        Task<List<ProductDTO>> GetAllAsync();
    }
}
