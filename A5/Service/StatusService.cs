using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;
using A5.Service.Interfaces;
using A5.Service.Validations;
using A5.Data;

namespace A5.Service
{
    public class StatusService {
        private readonly AppDbContext _context;
        public StatusService(AppDbContext context) 
        { 
            _context=context;
        }
        public Status GetById(int id)
        {
            StatusServiceValidations.ValdiateGetById(id);
            try
            {
                return _context.Set<Status>().FirstOrDefault(nameof =>nameof.Id == id);              
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }

        public IEnumerable<Status> GetAll()
        {
            try
            {
                return _context.Set<Status>().Where(nameof =>nameof.IsActive == true).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }
    }
        
        
    
}