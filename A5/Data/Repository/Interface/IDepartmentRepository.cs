using A5.Models;

namespace A5.Data.Repository.Interface
{
    public interface IDepartmentRepository
    {
        bool CreateDepartment(Department department);
        bool UpdateDepartment(Department department);
        Department? GetDepartmentById(int departmentId);
        bool DisableDepartment(int departmentId, int userId);
        int GetCount(int departmentId);
        IEnumerable<Department> GetAllDepartment();
        IEnumerable<Department> GetDepartmentsByOrganisationId(int organisationId);
    }
}