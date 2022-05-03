using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Base;
using A5.Data.Service.Interfaces;

namespace A5.Data.Service
{
    public class DesignationService : EntityBaseRepository<Designation>, IDesignationService
    {
        private readonly AppDbContext _context;
        public DesignationService(AppDbContext context) : base(context) {
            _context = context;
         }

         public IEnumerable<Designation> GetDesignationsByDepartmentId(int id)=> _context.Set<Designation>().Where(nameof =>nameof.DepartmentId == id).ToList();
        
    }
    
}