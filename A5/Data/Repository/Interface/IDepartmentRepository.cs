using A5.Models;

namespace A5.Data.Repository.Interface
{
    public interface IDepartmentRepository
    {
        bool CreateDepartment(Department department);
        bool UpdateDepartment(Department department);
        public Department ? GetDepartmentById(int departmentId);
        bool DisableDepartment(int departmentId,int userId);
        int GetCount(int departmentId);
        public IEnumerable<Department> GetAllDepartment();
        public IEnumerable<Department> GetDepartmentsByOrganisationId(int organisationId);
    }
}