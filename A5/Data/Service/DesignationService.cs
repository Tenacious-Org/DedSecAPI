using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;
using A5.Data.Service.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace A5.Data.Service
{
    public class DesignationService : EntityBaseRepository<Designation>, IDesignationService
    {
        private readonly AppDbContext _context;
        public DesignationService(AppDbContext context) : base(context)
        {
            _context = context;
        }

         public IEnumerable<Designation> GetDesignationsByDepartmentId(int id)
         {
             
            try
            {
                var data =  _context.Set<Designation>().Where(nameof =>nameof.DepartmentId == id && nameof.IsActive == true).ToList();
                var count = data.Count();
                if(count != 0)
                {
                    return data;
                }
                else
                {
                    throw new ValidationException(" Department not Found!! ");
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
             
         }
         public object GetAllDesignations()
         {
            var result = (from des in _context.Designations
              join dep in _context.Departments on des.DepartmentId equals dep.Id
              select new{
                 des.Id,
                 des.DesignationName,
                 dep.DepartmentName,
                 des.IsActive,
                 des.AddedBy,
                 des.AddedOn,
                 des.UpdatedBy,
                 des.UpdatedOn
             }).ToList();
             return result;
         }
    }
    
}