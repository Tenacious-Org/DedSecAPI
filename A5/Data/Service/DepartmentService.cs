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
        //  public IEnumerable<Department> GetDepartments()
        //  {
        //      var result = (from o in _context.Organisations
        //       join d in _context.Departments on o.Id equals d.OrganisationId 
        //       select new{
        //          d.Id,
        //          d.DepartmentName,
        //          o.OrganisationName,
        //          d.IsActive,
        //          d.AddedBy,
        //          d.AddedOn,
        //          d.UpdatedBy,
        //          d.UpdatedOn
        //      }).ToList();
        //      return _context.Set<Department>().Include(Organisation).(from o in _context.Organisations
        //       join d in _context.Departments on o.Id equals d.OrganisationId 
        //       select new{
        //          d.Id,
        //          d.DepartmentName,
        //          o.OrganisationName,
        //          d.IsActive,
        //          d.AddedBy,
        //          d.AddedOn,
        //          d.UpdatedBy,
        //          d.UpdatedOn
        //      }).ToList();
        //  }
    }
}