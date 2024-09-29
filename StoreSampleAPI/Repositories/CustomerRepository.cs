using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreSampleAPI.DTOs;
using StoreSampleAPI.Interfaces;
using StoreSampleAPI.Models;

public class CustomerRepository : ICustomerRepository
{
    private readonly StoreSampleContext _context;
    private readonly IMapper _mapper;

    public CustomerRepository(StoreSampleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerOrderPredictionDTO>> GetCustomersWithOrderPredictions()
    {
        var customersWithOrders = await _context.Customers
            .Select(c => new
            {
                Customer = c,
                Orders = c.Orders.Select(o => o.Orderdate)
            })
            .ToListAsync();

        return customersWithOrders.Select(co => new CustomerOrderPredictionDTO
        {
            CustId = co.Customer.Custid,
            CompanyName = co.Customer.Companyname,
            LastOrderDate = co.Orders.Any() ? co.Orders.Max() : null,
            NextPredictedOrder = CalculateNextPredictedOrder(co.Orders)
        });
    }

    private static DateTime? CalculateNextPredictedOrder(IEnumerable<DateTime> orderDates)
    {
        var orderedDates = orderDates.OrderBy(d => d).ToList();
        if (!orderedDates.Any())
        {
            return null; 
        }
        if (orderedDates.Count == 1)
        {
            return orderedDates[0].AddDays(30); 
        }

        var dateDifferences = new List<double>();
        for (int i = 1; i < orderedDates.Count; i++)
        {
            dateDifferences.Add((orderedDates[i] - orderedDates[i - 1]).TotalDays);
        }

        var averageDays = dateDifferences.Average();
        return orderedDates.Last().AddDays(averageDays);
    }
}