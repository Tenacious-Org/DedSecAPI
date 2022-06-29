using A5.Models;
using System.Collections.Generic;
namespace Testing.MockData
{
    static class OrganisationMock
    {
        public static Organisation GetInvalidOrganisation()
        {
            return new Organisation();
        }
        public static Organisation GetValidOrganisation()
        {
            return new Organisation()
            {
                Id=1,
                OrganisationName="Tenacious",
                IsActive=true,
                AddedBy=1,
                UpdatedBy=1

            };
        }
        public static List<Organisation> GetListOfOrganisations()
        {
            return new List<Organisation>
            {
                new Organisation(){Id=1,OrganisationName="Tenacious",IsActive=true,AddedBy=1,UpdatedBy=1},
                new Organisation(){Id=2,OrganisationName="Ten",IsActive=true,AddedBy=1,UpdatedBy=1},
                new Organisation(){Id=3,OrganisationName="Nine",IsActive=true,AddedBy=1,UpdatedBy=1},
                new Organisation(){Id=4,OrganisationName="Development",IsActive=true,AddedBy=1,UpdatedBy=1},
                 new Organisation(){Id=5,OrganisationName="Testing",IsActive=true,AddedBy=1,UpdatedBy=1}
            };
        }
    }
}