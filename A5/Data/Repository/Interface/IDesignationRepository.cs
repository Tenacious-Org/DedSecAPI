using A5.Models;

namespace A5.Data.Repository.Interface
{
    public interface IDesignationRepository
    {
        public IEnumerable<Designation> GetDesignationsByDepartmentId(int departmentId);
        public IEnumerable<Designation> GetAllDesignation();
        public bool CreateDesignation(Designation designation);
        public int GetCount(int designationId);
        public bool UpdateDesignation(Designation designation);
        public bool DisableDesignation(int designationId,int userId);
        public Designation? GetDesignationById(int designationId);
        public  object ErrorMessage(string ValidationMessage);

    }
}