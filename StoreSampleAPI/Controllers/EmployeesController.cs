using Microsoft.AspNetCore.Mvc;
using StoreSampleAPI.DTOs;
using StoreSampleAPI.Models;
using StoreSampleAPI.Services;

namespace StoreSampleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return Ok(employees);
        }
    }
}
