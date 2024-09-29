using StoreSampleAPI.DTOs;
using StoreSampleAPI.Interfaces;
using StoreSampleAPI.Models;

namespace StoreSampleAPI.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CustomerOrdersDTO> GetOrdersByCustomerId(int customerId)
        {
            var (companyName, orders) = await _orderRepository.GetOrdersByCustomerId(customerId);

            if (companyName == null)
            {
                return null;
            }

            return new CustomerOrdersDTO
            {
                CompanyName = companyName,
                Orders = orders
            };
        }

        public async Task<int> CreateOrder(OrderDTO order, OrderDetailDTO orderDetail)
        {
            return await _orderRepository.CreateOrder(order, orderDetail);
        }
    }
}
