using StoreSampleAPI.DTOs;
using StoreSampleAPI.Interfaces;

namespace StoreSampleAPI.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerOrderPredictionDTO>> GetCustomersWithOrderPredictions()
        {
            return await _customerRepository.GetCustomersWithOrderPredictions();
        }
    }
}
