using StoreSampleAPI.DTOs;
using StoreSampleAPI.Interfaces;
using StoreSampleAPI.Models;

namespace StoreSampleAPI.Services
{
    public class ShipperService
    {
        private readonly IShipperRepository _shipperRepository;

        public ShipperService(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        public async Task<IEnumerable<ShipperDTO>> GetAllShippers()
        {
            return await _shipperRepository.GetAllShippers();
        }
    }
}
