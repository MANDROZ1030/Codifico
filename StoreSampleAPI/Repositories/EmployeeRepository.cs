using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreSampleAPI.DTOs;
using StoreSampleAPI.Interfaces;
using StoreSampleAPI.Models;

namespace StoreSampleAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly StoreSampleContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(StoreSampleContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
        }
    }
}
