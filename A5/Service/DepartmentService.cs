using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;
using A5.Service.Interfaces;
using A5.Service.Validations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using A5.Data;
using A5.Data.Repository.Interface;

namespace A5.Service
{
    public class DepartmentService :  IDepartmentService
    {
         private readonly ILogger<EntityBaseRepository<Department>> _logger; 
         private readonly IDepartmentRepository _departmentRepository;
        
        public DepartmentService( ILogger<EntityBaseRepository<Department>> logger,IDepartmentRepository departmentRepository){
               
                _logger=logger;
                _departmentRepository=departmentRepository;
         } 
         
        public bool CreateDepartment(Department department)
        {
            if(!DepartmentServiceValidations.CreateValidation(department)) throw new ValidationException("Invalid data");
            try{
                return _departmentRepository.CreateDepartment(department);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("DepartmentService: CreateDepartment(Departmetn department) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        public bool UpdateDepartment(Department department)
        {
            if(!DepartmentServiceValidations.UpdateValidation(department)) throw new ValidationException("Invalid Data");
            try{
                return _departmentRepository.UpdateDepartment(department);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("DepartmentService: UpdateDepartment(Department department) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        public Department? GetByDepartment(int id)
        {
            if(!DepartmentServiceValidations.ValidateGetById(id)) throw new ValidationException("Invalid Data");
            try{
                return _departmentRepository.GetByDepartment(id);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("DepartmentService: GetByDepartment(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        public bool DisableDepartment(int id)
        {
            if(!DepartmentServiceValidations.DisableValidation(id)) throw new ValidationException("Invalid Data");
            
            try
            {
                return _departmentRepository.DisableDepartment(id);

            }
            catch(ValidationException exception)
            {
                _logger.LogError("DepartmentService: DisableDepartment(int id) : (Error:{Message}",exception.Message);
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
             return _departmentRepository.GetCount(id);
        }
        public IEnumerable<Department> GetDepartmentsByOrganisationId(int id)
         { 
            DepartmentServiceValidations.ValidateGetByOrganisation(id);
            try
            {
               return _departmentRepository.GetDepartmentsByOrganisationId(id);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("DepartmentService: GetDepartmentsByOrganisationId(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
             
         }
          public IEnumerable<object> GetAllDepartments()
         {
            var department = _departmentRepository.GetAllDepartment();
            return department.Select( Department => new{
                id = Department.Id,
                departmentName = Department.DepartmentName,
                organisationName = Department?.Organisation?.OrganisationName,
                isActive = Department?.IsActive,
                addedBy = Department?.AddedBy,
                addedOn = Department?.AddedOn,
                updatedBy = Department?.UpdatedBy,
                updatedOn = Department?.UpdatedOn
            });
             
         }
           public object ErrorMessage(string ValidationMessage)
        {
            return new{message=ValidationMessage};
        }
       
    }

   
}

