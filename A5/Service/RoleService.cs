using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;
using A5.Service.Interfaces;
using A5.Data;

namespace A5.Service
{
    public class RoleService {
        private readonly AppDbContext _context;
        public RoleService(AppDbContext context) 
        { 
            _context=context;
        }
        public Role GetById(int id)
        {
            try
            {
                return _context.Set<Role>().FirstOrDefault(nameof =>nameof.Id == id);              
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }

        public IEnumerable<Role> GetAll()
        {
            try
            {
                return _context.Set<Role>().ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }
    }
        
        
    
}