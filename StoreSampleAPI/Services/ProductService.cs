using StoreSampleAPI.DTOs;
using StoreSampleAPI.Interfaces;
using StoreSampleAPI.Models;

namespace StoreSampleAPI.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }
    }
}
