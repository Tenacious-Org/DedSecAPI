using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;
using A5.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using A5.Service.Validations;
using A5.Data;

namespace A5.Service
{
    public class DesignationService : EntityBaseRepository<Designation>, IDesignationService
    {
        private readonly AppDbContext _context;
        private readonly MasterRepository _master;
         private readonly ILogger<EntityBaseRepository<Designation>> _logger;
        public DesignationService(AppDbContext context, MasterRepository master,ILogger<EntityBaseRepository<Designation>> logger) : base(context,logger)
        {
            _context = context;
            _master = master;
            _logger=logger;
        }

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
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Designation Service : GetDesignationsByDepartmentId(int id) : (Error:{Message}",exception.Message);
               throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
               throw;
            }
         }
         public IEnumerable<object> GetAllDesignations()
         {
            var designation = _master.GetAllDesignation();
            return designation.Select( Designation => new{
                id = Designation.Id,
                designationName = Designation.DesignationName,
                departmentName = Designation?.Department?.DepartmentName,
                isActive = Designation?.IsActive,
                addedBy = Designation?.AddedBy,
                addedOn = Designation?.AddedOn,
                updatedBy = Designation?.UpdatedBy,
                updatedOn = Designation?.UpdatedOn
            });
         }
          public bool CreateDesignation(Designation designation)
        {
            var obj = new DesignationServiceValidations();
            if(!obj.CreateValidation(designation)) throw new ValidationException("Invalid data");
            bool NameExists=_context.Designations!.Any(nameof=>nameof.DesignationName==designation.DesignationName && nameof.DepartmentId==designation.DepartmentId);
            if(NameExists) throw new ValidationException("Designation Name already exists");
            try{
                return Create(designation);
            }
             catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Designation Service: CreateDesignation(Designation) : (Error:{Message}",exception.Message);
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
             var obj = new DesignationServiceValidations();
            if(!obj.UpdateValidation(designation)) throw new ValidationException("Invalid Data");
             bool NameExists=_context.Designations!.Any(nameof=>nameof.DesignationName==designation.DesignationName);
            if(NameExists) throw new ValidationException("Designation Name already exists");
            try{
                return Update(designation);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Designation Service: UpdateDesignation(Designation) : (Error:{Message}",exception.Message);
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
 
    