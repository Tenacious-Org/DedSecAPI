using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using A5.Models;
using Microsoft.EntityFrameworkCore;

namespace A5.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IAudit, IEntityBase, IValidation<T>, new()
    {
        private readonly AppDbContext _context;
        public EntityBaseRepository( AppDbContext context)
        {
            _context = context;
        }

        //Methods
        public bool Create(T entity)
        {
           bool result = false;
           var a = new T();
           a.CreateValidation(entity);
           try
           {
               _context.Set<T>().Add(entity);
                _context.SaveChanges();
                result = true;
                return result;
           }
           catch(Exception exception)
           {
               throw exception;
           }

        }

        public bool Disable(T entity, int id)
        {
            bool result = false;
            var a=new T();
            a.DisableValidation(entity,id);
            try{
                if(entity != null && entity.Id == id)
            {
                var disable = _context.Set<T>().FirstOrDefault(nameof =>nameof.Id == id);
                disable.IsActive = false;
                _context.SaveChanges();
                result = true;
            }
            return result;
            }
            catch(Exception exception)
            {
                throw exception;
            }
           
            
        }

        public bool Update(T entity, int id)
        {
            bool result = false;
            var a =new T();
            a.UpdateValidation(entity,id);
            try{
                if(entity != null && entity.Id == id)
                {
                    _context.Set<T>().Update(entity);
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
                return _context.Set<T>().FirstOrDefault(nameof =>nameof.Id == id) ;              
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