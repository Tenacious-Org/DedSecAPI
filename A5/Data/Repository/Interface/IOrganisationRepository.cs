using A5.Models;
namespace A5.Data.Repository.Interface
{
    public interface IOrganisationRepository
    {
         bool CreateOrganisation(Organisation organisation);
        bool UpdateOrganisation(Organisation organisation);
        Organisation? GetOrganisationById(int organisationId);
        bool DisableOrganisation(int organisationId,int employeeId);
        int GetCount(int organisationId);
        public IEnumerable<Organisation> GetAllOrganisation();

        public  object ErrorMessage(string ValidationMessage);
    }
}