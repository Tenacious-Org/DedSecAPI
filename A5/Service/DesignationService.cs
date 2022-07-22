using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;
using A5.Data.Repository.Interface;
using A5.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using A5.Data.Validations;
using A5.Data;

namespace A5.Service
{
    public class DesignationService : IDesignationService
    {
        private readonly IDesignationRepository _desginationRepository;
        private readonly ILogger<IDesignationRepository> _logger;
        private readonly DesignationValidations _designationValidations;
        public DesignationService(IDesignationRepository desginationRepository, ILogger<IDesignationRepository> logger, DesignationValidations designationValidations)
        {
            _desginationRepository = desginationRepository;
            _logger = logger;
            _designationValidations = designationValidations;
        }

        //gets the list of designation by department id
        public IEnumerable<Designation> GetDesignationsByDepartmentId(int departmentId)
        {
            if (departmentId <= 0) throw new ValidationException("Department Id must be greater than zero");
            try
            {
                return _desginationRepository.GetDesignationsByDepartmentId(departmentId);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("DesignationService : GetDesignationsByDepartmentId(departmentId : {departmentId}) : (Error:{Message}", departmentId, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("DesignationService : GetDesignationsByDepartmentId(departmentId : {departmentId}) : (Error:{Message}", departmentId, exception.Message);
                throw;
            }
        }

        //gets the designations value and returns the anonymous object
        public IEnumerable<object> GetAllDesignations()
        {
            try
            {
                var designation = _desginationRepository.GetAllDesignation();
                return designation.Select(Designation => new
                {
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
            catch (Exception exception)
            {

                _logger.LogError("DesignationService : GetAllDesignations() : (Error:{Message}", exception.Message);
                throw;
            }


        }

        //creates the designation using designation object
        public bool CreateDesignation(Designation designation)
        {
            if (designation == null) throw new ValidationException("Designation should not be null");
            _designationValidations.CreateValidation(designation);
            try
            {
                return _desginationRepository.CreateDesignation(designation);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("DesignationService: CreateDesignation(Designation) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("DesignationService: CreateDesignation(Designation) : (Error:{Message}", exception.Message);
                throw;
            }
        }

        //gets the designation count by using designation id
        public int GetCount(int designationId)
        {
            if (designationId <= 0) throw new ValidationException("Designation Id must be greater than zero");
            try{
                 return _desginationRepository.GetCount(designationId);
            }
            catch(ValidationException exception)
            {
                 _logger.LogError("DesignationService: GetCount(designationId : {designationId}) : (Error:{Message}", designationId,exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                 _logger.LogError("DesignationService: GetCount(designationId : {designationId}) : (Error:{Message}", designationId,exception.Message);
                throw;
            }
           
        }
        public object ErrorMessage(string ValidationMessage)
        {
            return new { message = ValidationMessage };
        }


        //updates the designation using designation object
        public bool UpdateDesignation(Designation designation)
        {
            if (designation == null) throw new ValidationException("Designation should not be null");
            _designationValidations.UpdateValidation(designation);
            try
            {
                return _desginationRepository.UpdateDesignation(designation);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("Designation Service: UpdateDesignation(Designation) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Designation Service: UpdateDesignation(Designation) : (Error:{Message}", exception.Message);
                throw;
            }
        }

        //disbales the designation using designation id and current user id
        public bool DisableDesignation(int designationId, int userId)
        {
            if (designationId <= 0) throw new ValidationException("Designation Id must be greater than zero");
            if (userId <= 0) throw new ValidationException("User Id must be greater than zero");
            _designationValidations.DisableValidation(userId);
            try
            {
                return _desginationRepository.DisableDesignation(designationId, userId);
            }
            catch (Exception exception)
            {
                _logger.LogError("Designation Service: DisableDesignation(int designationId,int employeeId) : (Error:{Message}", exception.Message);
                throw;
            }
        }

        //gets the designation by designation Id
        public Designation? GetDesignationById(int designationId)
        {
             if (designationId <= 0) throw new ValidationException("Designation Id must be greater than zero");   
            try
            {
                return _desginationRepository.GetDesignationById(designationId);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("DesignationController : GetDesignationById(int id) : (Error : {Message})", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("DesginationService: GetDesignationById(int designationId) : (Error:{Message}", exception.Message);
                throw;
            }
        }

    }

}

