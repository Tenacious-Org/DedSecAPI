using A5.Models;
using System.Collections.Generic;
namespace Testing.MockData
{
    static class DepartmentMock
    {
        public static Department GetInvalidDepartment()
        {
            return new Department();
        }
        public static  Department GetvalidDepartment()
        {
            return new  Department()
            {
                Id=1,
                DepartmentName="Tenacious",
                OrganisationId=1,
                IsActive=true
               

            };
        }
        public static List< Department> GetListOfDepartment()
        {
            return new List< Department>
            {
                new Department(){Id=1,DepartmentName="Tenacious",OrganisationId=1,IsActive=true},
                new Department(){Id=2,DepartmentName="Ten",OrganisationId=1,IsActive=true},
                new Department(){Id=3,DepartmentName="Nine",OrganisationId=1,IsActive=true},
                new Department(){Id=4,DepartmentName="Development",OrganisationId=1,IsActive=true},
                 new Department(){Id=5,DepartmentName="Testing",OrganisationId=1,IsActive=true}
            };
        }
    
}
}