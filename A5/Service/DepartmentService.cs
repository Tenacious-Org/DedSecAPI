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
         
         //creates department using department object.
        public bool CreateDepartment(Department department)
        {
            DepartmentServiceValidations.CreateValidation(department);
            try{
                return _departmentRepository.CreateDepartment(department);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("DepartmentService: CreateDepartment(Department department) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("DepartmentService: CreateDepartment(Department department) : (Error:{Message}",exception.Message);
                throw;
            }
        }
        //updates department using department object.
        public bool UpdateDepartment(Department department)
        {
            DepartmentServiceValidations.UpdateValidation(department);
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
                _logger.LogError("DepartmentService: UpdateDepartment(Department department) : (Error:{Message}",exception.Message);
                throw;
            }
        }
        //Get department using Department Id
        public Department? GetDepartmentById(int id)
        {
            if(id<=0) throw new ValidationException("Department Id should not be null or negative");
            try{
                return _departmentRepository.GetDepartmentById(id);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("DepartmentService: GetByDepartment({id}) : (Error:{Message}",id,exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("DepartmentService: GetByDepartment({id}) : (Error:{Message}",id,exception.Message);
                throw;
            }
        }
        //Disable department using department id and employee id.
        public bool DisableDepartment(int id,int employeeId)
        {   
            if(id<=0) throw new ValidationException("Department Id should not be null or negative");
            if(employeeId<=0 )throw new ValidationException("Employee Id should not be null or negative");       
            try
            {
                return _departmentRepository.DisableDepartment(id,employeeId);

            }
            catch(ValidationException exception)
            {
                _logger.LogError("DepartmentService: DisableDepartment(id:{id},employeeId:{employeeId}) : (Error:{Message}",id,employeeId,exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        //Gets the count of employees under department.
      
        public int GetCount(int id)
        {
             return _departmentRepository.GetCount(id);
        }
        //Gets Department by organisation Id.
        public IEnumerable<Department> GetDepartmentsByOrganisationId(int id)
         { 
           if(id<=0) throw new ValidationException("organisation Id should not be null or negative");
            try
            {
               return _departmentRepository.GetDepartmentsByOrganisationId(id);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("DepartmentService: GetDepartmentsByOrganisationId({id}) : (Error:{Message}",id,exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("DepartmentService: GetDepartmentsByOrganisationId({id}) : (Error:{Message}",id,exception.Message);
                throw;
            }
             
         }
         //Gets all the department.
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

