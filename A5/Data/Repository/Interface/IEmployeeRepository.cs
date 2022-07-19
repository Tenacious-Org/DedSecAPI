using A5.Models;

namespace A5.Data.Repository.Interface
{
    public interface IEmployeeRepository
        {
        public bool CreateEmployee(Employee employee);
        public bool DisableEmployee(int id,int employeeId);
        public bool UpdateEmployee(Employee employee);
        public Employee? GetEmployeeById(int id);
        public IEnumerable<Employee> GetAllEmployees();
        // public IEnumerable<Employee> GetByHR(int id);
        // public IEnumerable<Employee> GetByReportingPerson(int id);
       // public IEnumerable<Employee> GetEmployeeByDepartmentId(int departmentId);
        public IEnumerable<Employee> GetReportingPersonByDepartmentId(int departmentId);
        public IEnumerable<Employee> GetHrByDepartmentId(int departmentId);
        public IEnumerable<Employee> GetEmployeeByRequesterId(int requesterId);
        public IEnumerable<Employee> GetEmployeeByOrganisation(int organisationId);
        public Employee GetEmployee(string Email, string Password);        
        public int GetEmployeeCount(int id);




    }
}