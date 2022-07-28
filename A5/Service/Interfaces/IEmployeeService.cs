using System.Collections.Generic;
using System.Linq;
using A5.Data.Repository.Interface;
using A5.Models;

namespace A5.Data.Service.Interfaces
{
    public interface IEmployeeService
    {
         bool CreateEmployee(Employee employee);
         bool DisableEmployee(int employeeId, int userId);
         bool UpdateEmployee(Employee employee);
         object? GetEmployeeById(int employeeId);
         IEnumerable<object> GetAllEmployees();
         IEnumerable<Employee> GetReportingPersonByDepartmentId(int departmentId);
         IEnumerable<Employee> GetHrByDepartmentId(int departmentId);
         IEnumerable<Employee> GetEmployeeByVpDesignation();
         IEnumerable<Employee> GetEmployeeByRequesterId(int requesterId);
         IEnumerable<Employee> GetEmployeeByOrganisation(int organisationId);
         Employee GetEmployee(string Email, string Password);
         int GetEmployeeCount(int employeeId);


    }
}