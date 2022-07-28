using A5.Models;
namespace A5.Data.Repository.Interface
{

    public interface IOrganisationRepository
    {
        bool CreateOrganisation(Organisation organisation);
        bool UpdateOrganisation(Organisation organisation);
        Organisation? GetOrganisationById(int organisationId);
        bool DisableOrganisation(int organisationId, int userId);
        int GetCount(int organisationId);
        IEnumerable<Organisation> GetAllOrganisation();
        object ErrorMessage(string ValidationMessage);
    }
}