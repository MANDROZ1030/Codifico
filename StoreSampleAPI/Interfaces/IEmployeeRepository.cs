using StoreSampleAPI.DTOs;
using StoreSampleAPI.Models;

namespace StoreSampleAPI.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDTO>> GetAllEmployees();
    }
}
