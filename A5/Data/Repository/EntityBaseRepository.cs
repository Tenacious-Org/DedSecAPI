using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using A5.Models;
using Microsoft.EntityFrameworkCore;
namespace A5.Data.Repository
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IAudit, IEntityBase,  new()
    {
        private readonly AppDbContext _context;
        private readonly Department department;

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
            // if(entity!=null){
               
            // }
            return result; 
                
           }
           catch(ValidationException exception)
           {
               throw exception;
           }

        }


        public bool Disable(int id)
        {
          bool result = false;
            try
            {
                if(id!= null )
                {
                var disable = _context.Set<T>().FirstOrDefault(nameof =>nameof.Id == id);
                disable.IsActive = false;
                _context.SaveChanges();
                result= true;
                }
                return result; 
            }
            catch(Exception exception)
            {
                throw exception;
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
            catch(Exception exception){
                throw exception;
            }
            
        }

        public T GetById(int id)
        {
            
            try
            {
                return _context.Set<T>().FirstOrDefault(nameof =>nameof.Id == id);              
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }
        public IEnumerable<T> GetAll()
        {
            try
            {
                return _context.Set<T>().Where(nameof =>nameof.IsActive == true).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }



    }
}