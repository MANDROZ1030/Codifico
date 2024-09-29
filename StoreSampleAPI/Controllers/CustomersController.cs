using Microsoft.AspNetCore.Mvc;
using StoreSampleAPI.DTOs;
using StoreSampleAPI.Services;

namespace StoreSampleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("with-predictions")]
        public async Task<ActionResult<IEnumerable<CustomerOrderPredictionDTO>>> GetCustomersWithOrderPredictions()
        {
            var customers = await _customerService.GetCustomersWithOrderPredictions();
            return Ok(customers);
        }
    }
}
