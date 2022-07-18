using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using A5.Models;
using Microsoft.EntityFrameworkCore;
using A5.Data.Repository.Interface;
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
                entity.AddedOn=DateTime.UtcNow;
                _context.SaveChanges();
            return true; 
                
           }
            catch (Exception exception){
                _logger.LogError("EntityBaseRepository : Create({T} entity) : (Error:{Message}",exception.Message);
              return false;
            }

        }


        public bool Disable(int id,int employeeId)
        {
          bool result = false;
            try
            {
                if(id!=0)
                {
                var entity=GetById(id);
                var disable = _context.Set<T>().FirstOrDefault(nameof =>nameof.Id == id);
                disable!.IsActive = false;
                entity!.UpdatedBy=employeeId;
                entity.UpdatedOn=DateTime.UtcNow;
                _context.SaveChanges();
                result= true;
                }
                return result; 
            }
            catch (Exception exception){
                _logger.LogError("EntityBaseRepository : Disable(int id,int employeeId) : (Error:{Message}",exception.Message);
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
                    entity.UpdatedOn=DateTime.UtcNow;
                    _context.SaveChanges();
                    result = true;
                }
                return result;               
            }
            catch (Exception exception){
                _logger.LogError("EntityBaseRepository : Update(T entity) : (Error:{Message}",exception.Message);
              return result;
            }    
        }

        public T? GetById(int id)
        { 
            try
            {
                return _context.Set<T>().FirstOrDefault(nameof =>nameof.Id == id);              
            }
            catch (Exception exception){
                _logger.LogError("EntityBaseRepository : GetById(int id) : (Error:{Message}",exception.Message);
              throw;
            }
            
        }
        public IEnumerable<T> GetAll()
        {
            try
            {
                return _context.Set<T>().Where(nameof =>nameof.IsActive == true).ToList();
            }
            catch (Exception exception){
                _logger.LogError("EntityBaseRepository : GetAll() : (Error:{Message}",exception.Message);
               throw;
            }
            
        }



    }
}