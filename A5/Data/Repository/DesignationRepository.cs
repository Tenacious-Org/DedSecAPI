using System.ComponentModel.DataAnnotations;
using A5.Data.Repository.Interface;
using A5.Models;
using A5.Service.Validations;
using Microsoft.EntityFrameworkCore;

namespace A5.Data.Repository
{
    public class DesignationRepository : EntityBaseRepository<Designation>, IDesignationRepository
    {
        private readonly AppDbContext _context;
         private readonly ILogger<EntityBaseRepository<Designation>> _logger;
        public DesignationRepository(AppDbContext context,ILogger<EntityBaseRepository<Designation>> logger) : base(context,logger)
        {
            _context = context;
            _logger=logger;
        }

         public IEnumerable<Designation> GetDesignationsByDepartmentId(int id)
         {
             if(!DesignationServiceValidations.ValidateGetByDepartment(id)) throw new ValidationException("Invalid data");
            try
            {
                var data =  _context.Set<Designation>().Where(nameof =>nameof.DepartmentId == id && nameof.IsActive == true).ToList();
                var count = data.Count;
                if(count != 0)
                {
                    return data;
                }
                else
                {
                    throw new ValidationException(" Department not Found!! ");
                }
            }
             catch(ValidationException exception)
            {
                _logger.LogError("DesignationService : GetDesignationsByDepartmentId(int id) : (Error:{Message}",exception.Message);
               throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
               throw;
            }
         }
          public IEnumerable<Designation> GetAllDesignation()
        {
            try
            {
                var designations = _context.Set<Designation>().Where(nameof =>nameof.IsActive == true).Include("Department").ToList();
                return designations;
            }
            catch(ValidationException exception)
            {
                _logger.LogError("MasterRepository: GetAllDesignation() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
            
        }
        public Designation? GetDesignationById(int id)
        {
            if(!DepartmentServiceValidations.ValidateGetById(id)) throw new ValidationException("Invalid Data");
            try{
                return GetById(id);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("DesignationRepository: GetDesginationById(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
          public bool CreateDesignation(Designation designation)
        {
            if(!DesignationServiceValidations.CreateValidation(designation)) throw new ValidationException("Invalid data");
            bool NameExists=_context.Designations!.Any(nameof=>nameof.DesignationName==designation.DesignationName && nameof.DepartmentId==designation.DepartmentId);
            if(NameExists) throw new ValidationException("Designation Name already exists");
            try{
                return Create(designation);
            }
             catch(ValidationException exception)
            {
                _logger.LogError("DesignationService: CreateDesignation(Designation) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        public int GetCount(int id)
        {
             var checkEmployee = _context.Set<Designation>().Where(nameof => nameof.IsActive == true && nameof.Id == id).Count();
             return checkEmployee;
        }
         public object ErrorMessage(string ValidationMessage)
        {
            return new{message=ValidationMessage};
        }
        public bool UpdateDesignation(Designation designation)
        {
            if(!DesignationServiceValidations.UpdateValidation(designation)) throw new ValidationException("Invalid Data");
             bool NameExists=_context.Designations!.Any(nameof=>nameof.DesignationName==designation.DesignationName);
            if(NameExists) throw new ValidationException("Designation Name already exists");
            try{
                return Update(designation);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Designation Service: UpdateDesignation(Designation) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        public bool DisableDesignation(int id)
        {
            if(!DesignationServiceValidations.DisableValidation(id)) throw new ValidationException("Invalid data");
            try{
                return Disable(id);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Designation Service: UpdateDesignation(Designation) : (Error:{Message}",exception.Message);
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