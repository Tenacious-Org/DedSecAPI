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
        Department? GetDepartmentById(int departmentId);
        bool DisableDepartment(int departmentId,int userId);
        int GetCount(int departmentId);
        public IEnumerable<object> GetAllDepartments();
        public IEnumerable<Department> GetDepartmentsByOrganisationId(int organisationId);
        public object ErrorMessage(string ValidationMessage);
    }
}