using StoreSampleAPI.DTOs;
using StoreSampleAPI.Interfaces;
using StoreSampleAPI.Models;

namespace StoreSampleAPI.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployees()
        {
            return await _employeeRepository.GetAllEmployees();
        }
    }
}
