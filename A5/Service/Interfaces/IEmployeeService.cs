using System.Collections.Generic;
using System.Linq;
using A5.Data.Repository.Interface;
using A5.Models;

namespace A5.Data.Service.Interfaces
{
    public interface IEmployeeService
    {
        public bool CreateEmployee(Employee employee);
        public bool DisableEmployee(int id);
        public bool UpdateEmployee(Employee employee);
        public object? GetEmployeeById(int id);
        public IEnumerable<object> GetAllEmployees();
        public IEnumerable<Employee> GetByHR(int id);
        public IEnumerable<Employee> GetByReportingPerson(int id);
        public IEnumerable<Employee> GetEmployeeByDepartmentId(int id);
        public IEnumerable<Employee> GetReportingPersonByDepartmentId(int id);
        public IEnumerable<Employee> GetHrByDepartmentId(int id);
        public IEnumerable<Employee> GetEmployeeByRequesterId(int id);
        public IEnumerable<Employee> GetEmployeeByOrganisation(int id);
        public bool ChangePassword(Employee employee, int id, String Email);
        public Employee GetEmployee(string Email, string Password);
        public int GetEmployeeCount(int id);


    }
}