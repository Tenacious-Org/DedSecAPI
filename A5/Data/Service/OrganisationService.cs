using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;
using A5.Data.Service.Interfaces;

namespace A5.Data.Service
{
    public class OrganisationService : EntityBaseRepository<Organisation>, IOrganisationService
    {
        private readonly AppDbContext _context;
        private readonly EmployeeService _employeeService;
        private readonly OrganisationService _organisationService;
        public OrganisationService(AppDbContext context) : base(context)
        {
             _context = context;
        }
        public string DisableOrganisation(int id)
        {
            var result = "Organisation can't be disabled.";
            var checkEmployee = _context.Set<Employee>().Where(nameof =>nameof.IsActive == true && nameof.OrganisationId == id).ToList().Count();
            if(checkEmployee == 0)
            {
                _organisationService.Disable(id);
                result = "Organisation Disabled Successfully.";
            }
            else
            {
                result = "There are Certain Employees in that Organisation. You're gonna change their Organisation to disable this Organisation.";
            }
            return result;
        }
    }
}