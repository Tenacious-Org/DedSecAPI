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
        bool DisableOrganisation(int organisationId,int employeeId);
        int GetCount(int organisationId);
        public IEnumerable<Organisation> GetAllOrganisation();

        public object ErrorMessage(string ValidationMessage);
    }
}