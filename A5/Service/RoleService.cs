using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;
using A5.Service.Interfaces;
using A5.Data;
using System.ComponentModel.DataAnnotations;
using A5.Data.Validations;

namespace A5.Service
{
    public class RoleService {
        private readonly AppDbContext _context;
        private readonly ILogger<RoleService> _logger;
        public RoleService(AppDbContext context,ILogger<RoleService> logger) 
        { 
            _context=context;
            _logger=logger;
        }
        //Gets the role of particular id using role id.
        public Role? GetById(int id)
        {
            if(id<=0) throw new ValidationException("Id should not be zero or negative");
            try
            {
                return _context.Set<Role>().FirstOrDefault(nameof =>nameof.Id == id);              
            }
            catch(ValidationException exception)
            {
                _logger.LogError("RoleService: GetById(id  :{id}) : (Error:{Message}",id,exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("RoleService: GetById(id  :{id}) : (Error:{Message}",id,exception.Message);
                throw;
            }
            
        }
        //gets all the roles.
        public IEnumerable<Role> GetAll()
        {
            try
            {
                return _context.Set<Role>().ToList();
            }
            catch(ValidationException exception)
            {
                _logger.LogError("RoleService: GetAll() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("RoleService: GetAll() : (Error:{Message}",exception.Message);
                throw;
            }
            
        }
    }
        
        
    
}