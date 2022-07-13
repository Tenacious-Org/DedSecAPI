using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;
using A5.Data.Repository.Interface;
using A5.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using A5.Service.Validations;
using A5.Data;

namespace A5.Service
{
    public class DesignationService :  IDesignationService
    {
        private readonly IDesignationRepository _desginationRepository;
         private readonly ILogger<IDesignationRepository> _logger;
        public DesignationService(IDesignationRepository desginationRepository, ILogger<IDesignationRepository> logger) 
        {
            _desginationRepository = desginationRepository;
            _logger=logger;
        }

         public IEnumerable<Designation> GetDesignationsByDepartmentId(int id)
         {
            if(!DesignationServiceValidations.ValidateGetByDepartment(id)) throw new ValidationException("Invalid data");
            try
            {
                return _desginationRepository.GetDesignationsByDepartmentId(id);
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
         public IEnumerable<object> GetAllDesignations()
         {
            var designation = _desginationRepository.GetAllDesignation();
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
            if(!DesignationServiceValidations.CreateValidation(designation)) throw new ValidationException("Invalid data");
            try{
                return _desginationRepository.CreateDesignation(designation);
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
             return _desginationRepository.GetCount(id);
        }
         public object ErrorMessage(string ValidationMessage)
        {
            return new{message=ValidationMessage};
        }
        public bool UpdateDesignation(Designation designation)
        {
            if(!DesignationServiceValidations.UpdateValidation(designation)) throw new ValidationException("Invalid data"); 
            try{
                return _desginationRepository.UpdateDesignation(designation);
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
                return _desginationRepository.DisableDesignation(id);
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

        public Designation? GetDesignationById(int id)
        {
            if(!DesignationServiceValidations.ValidateGetById(id)) throw new ValidationException("Invalid data");
             try{
                return _desginationRepository.GetDesignationById(id);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("DesginationService: GetByDepartment(int id) : (Error:{Message}",exception.Message);
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
 
    