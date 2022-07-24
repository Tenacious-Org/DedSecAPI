using A5.Models;

namespace A5.Data.Repository.Interface
{
    public interface IEmployeeRepository
    {
        public bool CreateEmployee(Employee employee);
        public bool DisableEmployee(int employeeId, int userId);
        public bool UpdateEmployee(Employee employee);
        public Employee? GetEmployeeById(int employeeId);
        public IEnumerable<Employee> GetAllEmployees();
        public bool ForgotPassword(string aceId, string emailId);
        public IEnumerable<Employee> GetReportingPersonByDepartmentId(int departmentId);
        public IEnumerable<Employee> GetHrByDepartmentId(int departmentId);
        public IEnumerable<Employee> GetEmployeeByVpDesignation();
        public IEnumerable<Employee> GetEmployeeByRequesterId(int requesterId);
        public IEnumerable<Employee> GetEmployeeByOrganisation(int organisationId);
        public Employee GetEmployee(string Email, string Password);
        public int GetEmployeeCount(int employeeId);




    }
}