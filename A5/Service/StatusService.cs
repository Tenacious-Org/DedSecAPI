using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;
using A5.Service.Interfaces;
using A5.Service.Validations;
using A5.Data;
using System.ComponentModel.DataAnnotations;

namespace A5.Service
{
    public class StatusService {
        private readonly AppDbContext _context;
        private readonly ILogger<StatusService> _logger;
        public StatusService(AppDbContext context,ILogger<StatusService> logger) 
        { 
            _context=context;
            _logger=logger;
        }
        public Status? GetById(int id)
        {
            StatusServiceValidations.ValdiateGetById(id);
            try
            {
                return _context.Set<Status>().FirstOrDefault(nameof =>nameof.Id == id);              
            }
            catch(ValidationException exception)
            {
                _logger.LogError("StatusService: GetById(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

        public IEnumerable<Status> GetAll()
        {
            try
            {
                return _context.Set<Status>().Where(nameof =>nameof.IsActive == true).ToList();
            }
           catch(ValidationException exception)
            {
                _logger.LogError("StatusService: GetAll() : (Error:{Message}",exception.Message);
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