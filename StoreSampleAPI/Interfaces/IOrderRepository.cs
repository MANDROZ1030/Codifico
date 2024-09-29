using StoreSampleAPI.DTOs;
using StoreSampleAPI.Models;

namespace StoreSampleAPI.Interfaces
{
    public interface IOrderRepository
    {
        Task<(string CompanyName, IEnumerable<OrderDTO> Orders)> GetOrdersByCustomerId(int CustId);
        Task<int> CreateOrder(OrderDTO order, OrderDetailDTO orderDetail);
    }
}
