using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;
using A5.Data.Service.Interfaces;

namespace A5.Data.Service
{
    public class DepartmentService : EntityBaseRepository<Department>, IDepartmentService
    {
        private readonly AppDbContext _context;
        public DepartmentService(AppDbContext context) : base(context) 
        {
            _context = context;
        }
        

         public IEnumerable<Department> GetDepartmentsByOrganisationId(int id)
         { 
            Department.GetByOrganisationIdValidation(id);
            try
            {
                return _context.Set<Department>().Where(nameof =>nameof.OrganisationId == id).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
             
         }
    }
}