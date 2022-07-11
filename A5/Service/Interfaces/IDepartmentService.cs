using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;

namespace A5.Service.Interfaces
{
    public interface IDepartmentService : IEntityBaseRepository<Department>
    {
        bool CreateDepartment(Department department);
        bool UpdateDepartment(Department department);
        Department GetByDepartment(int id);
        bool DisableDepartment(int id);
        int GetCount(int id);
        public IEnumerable<object> GetAllDepartments();
        public IEnumerable<Department> GetDepartmentsByOrganisationId(int id);
        public  object ErrorMessage(string ValidationMessage);
    }
}