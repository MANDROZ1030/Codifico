using StoreSampleAPI.DTOs;

namespace StoreSampleAPI.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerOrderPredictionDTO>> GetCustomersWithOrderPredictions();

    }
}
