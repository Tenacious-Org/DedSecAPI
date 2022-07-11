using System.Collections.Generic;
using System.Linq;
using A5.Data.Repository;
using A5.Models;

namespace A5.Service.Interfaces
{
    public interface IEmployeeService : IEntityBaseRepository<Employee>
    {
         public IEnumerable<Employee> GetByHR(int id);
         public IEnumerable<Employee> GetByReportingPerson(int id);
         public IEnumerable<Employee> GetEmployeeByRequesterId(int id);
         public bool CreateEmployee(Employee employee);
       
    }
}