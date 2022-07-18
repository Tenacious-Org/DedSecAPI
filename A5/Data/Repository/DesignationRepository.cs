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

    
        //to get designations by departmentId using id
         public IEnumerable<Designation> GetDesignationsByDepartmentId(int id)
         {
             DesignationServiceValidations.ValidateGetByDepartment(id);
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
                _logger.LogError("DesignationRespository : GetDesignationsByDepartmentId(int id) : (Error:{Message}",exception.Message);
               throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
               throw;
            }
         }

         //to get all designations
          public IEnumerable<Designation> GetAllDesignation()
        {
            try
            {
                var designations = _context.Set<Designation>().Where(nameof =>nameof.IsActive == true).Include("Department").ToList();
                return designations;
            }
            catch(ValidationException exception)
            {
                _logger.LogError("DesignationRespository: GetAllDesignation() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
            
        }

        //to get designation by using id
        public Designation? GetDesignationById(int id)
        {
            if(id<=0) throw new ValidationException(" Designation Id should nto be null or negative");
            try{
                return GetById(id);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("DesignationRespository: GetDesginationById(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

        // to create designation using designation object
          public bool CreateDesignation(Designation designation)
        {
            DesignationServiceValidations.CreateValidation(designation);
            bool NameExists=_context.Designations!.Any(nameof=>nameof.DesignationName==designation.DesignationName && nameof.DepartmentId==designation.DepartmentId);
            if(NameExists) throw new ValidationException("Designation Name already exists");
            try{
                return Create(designation);
            }
             catch(ValidationException exception)
            {
                _logger.LogError("DesignationRespository: CreateDesignation(Designation) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

        // to get count by using id
        public int GetCount(int id)
        {
             var checkEmployee = _context.Set<Employee>().Where(nameof => nameof.IsActive == true && nameof.DesignationId == id).Count();
             return checkEmployee;
        }
         public object ErrorMessage(string ValidationMessage)
        {
            return new{message=ValidationMessage};
        }

        // to update designation using designation object
        public bool UpdateDesignation(Designation designation)
        {
            DesignationServiceValidations.UpdateValidation(designation);
             bool NameExists=_context.Designations!.Any(nameof=>nameof.DesignationName==designation.DesignationName && nameof.DepartmentId==designation.DepartmentId);
            if(NameExists) throw new ValidationException("Designation Name already exists");
            try{
                return Update(designation);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("DesignationRespository: UpdateDesignation(Designation) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

        //to disable designation using id and employee id
        public bool DisableDesignation(int id,int employeeId)
        {
            DesignationServiceValidations.DisableValidation(id);
            try{
                return Disable(id,employeeId);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("DesignationRespository: UpdateDesignation(Designation) : (Error:{Message}",exception.Message);
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