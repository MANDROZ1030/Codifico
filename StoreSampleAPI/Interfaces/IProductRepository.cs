using StoreSampleAPI.DTOs;
using StoreSampleAPI.Models;

namespace StoreSampleAPI.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTO>> GetAllProducts();
    }
}
