using StoreSampleAPI.DTOs;
using StoreSampleAPI.Models;

namespace StoreSampleAPI.Interfaces
{
    public interface IShipperRepository
    {
        Task<IEnumerable<ShipperDTO>> GetAllShippers();
    }
}
