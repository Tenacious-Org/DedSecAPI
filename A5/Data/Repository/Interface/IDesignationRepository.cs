using A5.Models;

namespace A5.Data.Repository.Interface
{
    public interface IDesignationRepository
    {
        IEnumerable<Designation> GetDesignationsByDepartmentId(int departmentId);
        IEnumerable<Designation> GetAllDesignation();
        bool CreateDesignation(Designation designation);
        int GetCount(int designationId);
        bool UpdateDesignation(Designation designation);
        bool DisableDesignation(int designationId, int userId);
        Designation? GetDesignationById(int designationId);
        object ErrorMessage(string ValidationMessage);

    }
}