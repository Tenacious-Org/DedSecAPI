using A5.Models;

namespace A5.Data.Repository.Interface
{
    public interface IDepartmentRepository
    {
        bool CreateDepartment(Department department);
        bool UpdateDepartment(Department department);
        public Department ? GetByDepartment(int id);
        bool DisableDepartment(int id,int employeeId);
        int GetCount(int id);
        public IEnumerable<Department> GetAllDepartment();
        public IEnumerable<Department> GetDepartmentsByOrganisationId(int id);
        public  object ErrorMessage(string ValidationMessage);
    }
}