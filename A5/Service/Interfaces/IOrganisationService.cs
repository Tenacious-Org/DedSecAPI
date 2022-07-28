using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository.Interface;

namespace A5.Service.Interfaces
{
    public interface IOrganisationService
    {
        bool CreateOrganisation(Organisation organisation);
        bool UpdateOrganisation(Organisation organisation);
        Organisation? GetOrganisationById(int organisationId);
        bool DisableOrganisation(int organisationId,int userId);
        int GetCount(int organisationId);
         IEnumerable<Organisation> GetAllOrganisation();

         object ErrorMessage(string ValidationMessage);
    }
}