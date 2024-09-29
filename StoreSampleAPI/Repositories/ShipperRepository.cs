using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreSampleAPI.DTOs;
using StoreSampleAPI.Interfaces;
using StoreSampleAPI.Models;

namespace StoreSampleAPI.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly StoreSampleContext _context;
        private readonly IMapper _mapper;

        public ShipperRepository(StoreSampleContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShipperDTO>> GetAllShippers()
        {
            var shippers = await _context.Shippers.ToListAsync();
            return _mapper.Map<IEnumerable<ShipperDTO>>(shippers);
        }
    }
}
