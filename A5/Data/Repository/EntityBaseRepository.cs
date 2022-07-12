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
  

        public EntityBaseRepository( AppDbContext context )
        {
            _context = context;
        }

        //Methods
        public bool Create(T entity)
        {

           bool result = false;
          
           try
           {
              _context.Set<T>().Add(entity);
                entity.AddedBy=1;
                entity.AddedOn=DateTime.Now;
                _context.SaveChanges();
                result = true;
          
            return result; 
                
           }
           catch(ValidationException)
           {
               throw;
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
            catch(Exception)
            {
                throw;
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
            catch(Exception){
                throw;
            }
            
        }

        public T? GetById(int id)
        {
            
            try
            {
                return _context.Set<T>().FirstOrDefault(nameof =>nameof.Id == id);              
            }
            catch(Exception)
            {
                throw ;
            }
            
        }
        public IEnumerable<T> GetAll()
        {
            try
            {
                return _context.Set<T>().Where(nameof =>nameof.IsActive == true).ToList();
            }
            catch(Exception)
            {
                throw ;
            }
            
        }



    }
}