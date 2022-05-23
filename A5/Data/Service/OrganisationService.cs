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
        public OrganisationService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Employee> GetEmployeeByOrganisation(int id)
        {
            try
            {
                var result = _context.Set<Employee>().Where(nameof =>nameof.IsActive == true && nameof.OrganisationId == id).ToList();
                return result;
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }
    }
}