using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using A5.Models;
using Microsoft.EntityFrameworkCore;
namespace A5.Data.Repository
{
    public class EntityBaseRepository<T>  : IEntityBaseRepository<T> where T : class, IAudit, IEntityBase,  new()
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EntityBaseRepository<T>> _logger;

       
        public EntityBaseRepository( AppDbContext context,ILogger<EntityBaseRepository<T>> logger)
        {
            _context = context;
            _logger=logger;
        }

        //Methods
        public bool Create(T entity)
        {
   
           try
           {
              _context.Set<T>().Add(entity);
                entity.AddedBy=1;
                entity.AddedOn=DateTime.Now;
                _context.SaveChanges();
            return true; 
                
           }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Entity Base Repository : Create(T entity) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              return false;
            }

        }


        public bool Disable(int id)
        {
          bool result = false;
            try
            {
                if(id!=0)
                {
                var disable = _context.Set<T>().FirstOrDefault(nameof =>nameof.Id == id);
                disable!.IsActive = false;
                _context.SaveChanges();
                result= true;
                }
                return result; 
            }
              catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Entity Base Repository : Disable(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              return result;
            }
                    
        }

        public bool Update(T entity)
        {
            bool result = false;
            
            try{
                if(entity != null )
                {
                    _context.Set<T>().Update(entity);
                    entity.UpdatedBy=1;
                    entity.UpdatedOn=DateTime.Now;
                    _context.SaveChanges();
                    result = true;
                }
                return result;               
            }
             catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Entity Base Repository : Update(T entity) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              return result;
            }
            
        }

        public T? GetById(int id)
        {
            
            try
            {
                return _context.Set<T>().FirstOrDefault(nameof =>nameof.Id == id);              
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Entity Base Repository : GetById(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              throw;
            }
            
        }
        public IEnumerable<T> GetAll()
        {
            try
            {
                return _context.Set<T>().Where(nameof =>nameof.IsActive == true).ToList();
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Entity Base Repository : GetAll() : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
               throw;
            }
            
        }



    }
}