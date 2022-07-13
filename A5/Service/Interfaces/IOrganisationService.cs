using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;

namespace A5.Service.Interfaces
{
    public interface IOrganisationService : IEntityBaseRepository<Organisation>
    {
        bool CreateOrganisation(Organisation organisation);
        bool UpdateOrganisation(Organisation organisation);
        Organisation? GetByOrganisation(int id);
        bool DisableOrganisation(int id);
        int GetCount(int id);
        public  object ErrorMessage(string ValidationMessage);
    }
}