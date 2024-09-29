using Microsoft.AspNetCore.Mvc;
using StoreSampleAPI.DTOs;
using StoreSampleAPI.Models;
using StoreSampleAPI.Services;

namespace StoreSampleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("by-customer/{custId}")]
        public async Task<ActionResult<CustomerOrdersDTO>> GetOrdersByCustomerId(int custId)
        {
            var result = await _orderService.GetOrdersByCustomerId(custId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateOrder([FromBody] CreateOrderDTO createOrderDto)
        {
            var orderId = await _orderService.CreateOrder(createOrderDto.Order, createOrderDto.OrderDetail);
            return Ok(orderId);
        }
    }
}
