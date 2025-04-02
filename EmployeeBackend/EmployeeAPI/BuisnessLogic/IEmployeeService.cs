using EmployeeAPI.Models;
namespace EmployeeAPI.BuisnessLogic
{
    public interface IEmployeeService
    {
       int InsertEmployee(Employee employee);
        List<Employee> GetEmployees();

        List<JobRoleDto> GetJobRoles();
        List<JobTitleDto> GetJobTitles();
        bool UpdateEmployee(Employee employee);


    }
}
