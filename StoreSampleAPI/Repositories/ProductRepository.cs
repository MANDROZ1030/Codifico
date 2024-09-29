using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreSampleAPI.DTOs;
using StoreSampleAPI.Interfaces;
using StoreSampleAPI.Models;

namespace StoreSampleAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreSampleContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(StoreSampleContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => !p.Discontinued)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }
    }
}
