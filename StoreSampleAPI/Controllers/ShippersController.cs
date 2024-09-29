using Microsoft.AspNetCore.Mvc;
using StoreSampleAPI.DTOs;
using StoreSampleAPI.Models;
using StoreSampleAPI.Services;

namespace StoreSampleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippersController : ControllerBase
    {
        private readonly ShipperService _shipperService;

        public ShippersController(ShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipperDTO>>> GetAllShippers()
        {
            var shippers = await _shipperService.GetAllShippers();
            return Ok(shippers);
        }
    }
}
