using EmployeeAPI.BuisnessLogic;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

      
        [HttpPost("insert")]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            try
            {
                int result = _employeeService.InsertEmployee(employee);
                return Ok(new { Message = "Employee added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Error = ex.Message });
            }
        }

        
        [HttpGet("get")]
        public IActionResult GetEmployees()
        {
            try
            {
                var employees = _employeeService.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Error = ex.Message });
            }
        }
        [HttpGet("roles")]
        public IActionResult GetJobRoles()
        {
            try
            {
                var roles = _employeeService.GetJobRoles();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet("titles")]
        public IActionResult GetJobTitles()
        {
            try
            {
                var titles = _employeeService.GetJobTitles();
                return Ok(titles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpPut("update")]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Invalid employee data.");
            }

            bool isUpdated = _employeeService.UpdateEmployee(employee);

            if (isUpdated)
            {
                return Ok(new { message = "Employee updated successfully" });
            }
            else
            {
                return NotFound(new { message = "Employee not found" });
            }
        }

    }
}
