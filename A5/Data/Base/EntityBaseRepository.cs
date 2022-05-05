using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using A5.Models;

namespace A5.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IAudit,IEntityBase,IValidation<T>, new()
    {
        private readonly ILogger<EntityBaseRepository<T>> _logger;
        private readonly AppDbContext _context;
        public EntityBaseRepository(ILogger<EntityBaseRepository<T>> logger, AppDbContext context)
        {
            _logger = logger;
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
            if(entity != null && entity.Id == id)
            {
                var disable = _context.Set<T>().FirstOrDefault(nameof =>nameof.Id == id);
                disable.IsActive = false;
                _context.SaveChanges();
                result = true;
            }
            return result;
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
            bool result=false;
            var a=new T();
            a.ValidateGetById(id);
            try
            {
                return _context.Set<T>().FirstOrDefault(nameof =>nameof.Id == id);
                result=true;
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }
        public IEnumerable<T> GetAll()
        {
            var a =new T();
            a.GetAllValidtion();
            try
            {
                return _context.Set<T>().ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }



    }
}