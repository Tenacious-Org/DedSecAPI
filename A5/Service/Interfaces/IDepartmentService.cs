using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository.Interface;

namespace A5.Service.Interfaces
{
    public interface IDepartmentService
    {
        bool CreateDepartment(Department department);
        bool UpdateDepartment(Department department);
        Department? GetDepartmentById(int id);
        bool DisableDepartment(int id,int employeeId);
        int GetCount(int id);
        public IEnumerable<object> GetAllDepartments();
        public IEnumerable<Department> GetDepartmentsByOrganisationId(int id);
        public object ErrorMessage(string ValidationMessage);
    }
}