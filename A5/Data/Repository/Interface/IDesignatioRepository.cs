using A5.Models;

namespace A5.Data.Repository.Interface
{
    public interface IDesignationRepository
    {
        public IEnumerable<Designation> GetDesignationsByDepartmentId(int id);
        public IEnumerable<Designation> GetAllDesignation();
        public bool CreateDesignation(Designation designation);
        public int GetCount(int id);
        public bool UpdateDesignation(Designation designation);
        public bool DisableDesignation(int id);
        public Designation? GetDesignationById(int id);
        public  object ErrorMessage(string ValidationMessage);

    }
}