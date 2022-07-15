using A5.Models;
namespace A5.Data.Repository.Interface
{
    public interface IOrganisationRepository
    {
         bool CreateOrganisation(Organisation organisation,int employeeId);
        bool UpdateOrganisation(Organisation organisation,int employeeId);
        Organisation? GetByOrganisation(int id);
        bool DisableOrganisation(int id,int employeeId);
        int GetCount(int id);
        public IEnumerable<Organisation> GetAllOrganisation();

        public  object ErrorMessage(string ValidationMessage);
    }
}