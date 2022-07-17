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
        Organisation? GetByOrganisation(int id);
        bool DisableOrganisation(int id,int employeeId);
        int GetCount(int id);
        public IEnumerable<Organisation> GetAllOrganisation();

        public object ErrorMessage(string ValidationMessage);
    }
}