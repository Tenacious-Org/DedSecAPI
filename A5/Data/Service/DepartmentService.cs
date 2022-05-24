using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;
using A5.Data.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        
            try
            {
                var organisationDetails = _context.Set<Department>().Where(nameof => nameof.OrganisationId == id && nameof.IsActive == true).ToList();
                return organisationDetails;
            }
            catch(Exception exception)
            {
                throw exception;
            }
             
         }
         public object GetAllDepartments()
         {
            var result = (from d in _context.Departments
              join o in _context.Organisations on d.OrganisationId equals o.Id
              select new{
                 d.Id,
                 d.DepartmentName,
                 o.OrganisationName,
                 d.IsActive,
                 d.AddedBy,
                 d.AddedOn,
                 d.UpdatedBy,
                 d.UpdatedOn
             }).ToList();
             return result;
         }
    }
}