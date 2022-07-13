using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;
using A5.Service.Interfaces;
using A5.Data;
using System.ComponentModel.DataAnnotations;

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
        public Role? GetById(int id)
        {
            try
            {
                return _context.Set<Role>().FirstOrDefault(nameof =>nameof.Id == id);              
            }
            catch(ValidationException exception)
            {
                _logger.LogError("RoleService: GetById(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
            
        }

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
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
            
        }
    }
        
        
    
}