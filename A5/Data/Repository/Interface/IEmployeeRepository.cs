using A5.Models;

namespace A5.Data.Repository.Interface
{
    public interface IEmployeeRepository
    {
        bool CreateEmployee(Employee employee);
        bool DisableEmployee(int employeeId, int userId);
        bool UpdateEmployee(Employee employee);
        Employee? GetEmployeeById(int employeeId);
        IEnumerable<Employee> GetAllEmployees();
        bool ForgotPassword(string aceId, string emailId);
        IEnumerable<Employee> GetReportingPersonByDepartmentId(int departmentId);
        IEnumerable<Employee> GetHrByDepartmentId(int departmentId);
        IEnumerable<Employee> GetEmployeeByVpDesignation();
        IEnumerable<Employee> GetEmployeeByRequesterId(int requesterId);
        IEnumerable<Employee> GetEmployeeByOrganisation(int organisationId);
        Employee GetEmployee(string Email, string Password);
        int GetEmployeeCount(int employeeId);




    }
}