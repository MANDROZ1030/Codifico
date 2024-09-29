using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreSampleAPI.DTOs;
using StoreSampleAPI.Interfaces;
using StoreSampleAPI.Models;

namespace StoreSampleAPI.Repositories
{
    public class OrderRepository :IOrderRepository
    {
        private readonly StoreSampleContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(StoreSampleContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(string CompanyName, IEnumerable<OrderDTO> Orders)> GetOrdersByCustomerId(int custId)
        {
            var customer = await _context.Customers
                .Where(c => c.Custid == custId)
                .Select(c => c.Companyname)
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                return (null, Enumerable.Empty<OrderDTO>());
            }

            var orders = await _context.Orders
                .Where(o => o.Custid == custId)
                .OrderByDescending(o => o.Orderdate)
                .ToListAsync();

            var orderDTOs = _mapper.Map<IEnumerable<OrderDTO>>(orders);

            return (customer, orderDTOs);
        }

        public async Task<int> CreateOrder(OrderDTO orderDto, OrderDetailDTO orderDetailDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = _mapper.Map<Order>(orderDto);
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                var orderDetail = _mapper.Map<OrderDetail>(orderDetailDto);
                orderDetail.Orderid = order.Orderid;
                _context.OrderDetails.Add(orderDetail);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return order.Orderid;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
