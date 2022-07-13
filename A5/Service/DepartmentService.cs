using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;
using A5.Service.Interfaces;
using A5.Service.Validations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using A5.Data;

namespace A5.Service
{
    public class DepartmentService : EntityBaseRepository<Department>, IDepartmentService
    {
        private readonly AppDbContext _context;
        private readonly MasterRepository _master;
         private readonly ILogger<EntityBaseRepository<Department>> _logger; 
        
        public DepartmentService(AppDbContext context, MasterRepository master, ILogger<EntityBaseRepository<Department>> logger) : base(context,logger) {
                _context=context;
                _master = master;
                _logger=logger;
         } 
         
        public bool CreateDepartment(Department department)
        {
            var obj = new DepartmentServiceValidations(_context);
            if(!obj.CreateValidation(department)) throw new ValidationException("Invalid data");
            bool NameExists=_context.Departments!.Any(nameof=>nameof.DepartmentName==department.DepartmentName);
            if(NameExists) throw new ValidationException("Department Name already exists");
            try{
                return Create(department);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Department Service: CreateDepartment(Departmetn department) : (Error:{Message}",exception.Message);
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
             var obj = new DepartmentServiceValidations(_context);
            if(!obj.UpdateValidation(department)) throw new ValidationException("Invalid Data");
             bool NameExists=_context.Departments!.Any(nameof=>nameof.DepartmentName==department.DepartmentName);
            if(NameExists) throw new ValidationException("Department Name already exists");
            try{
                return Update(department);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Department Service: UpdateDepartment(Department department) : (Error:{Message}",exception.Message);
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
            var obj = new DepartmentServiceValidations(_context);
            if(!obj.ValidateGetById(id)) throw new ValidationException("Invalid Data");
            try{
                return GetById(id);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Department Service: GetByDepartment(int id) : (Error:{Message}",exception.Message);
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
            var obj = new DepartmentServiceValidations(_context);
            if(!obj.DisableValidation(id)) throw new ValidationException("Invalid Data");
            
            try
            {
                return Disable(id);

            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Department Service: DisableDepartment(int id) : (Error:{Message}",exception.Message);
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
            var department = _master.GetAllDepartments();
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
        public int GetCount(int id)
        {
             var checkEmployee = _context.Set<Employee>().Where(nameof => nameof.IsActive == true && nameof.DepartmentId == id).Count();
             return checkEmployee;
        }
        public IEnumerable<Department> GetDepartmentsByOrganisationId(int id)
         { 
            DepartmentServiceValidations.ValidateGetByOrganisation(id);
            try
            {
                var organisationDetails = _context.Set<Department>().Where(nameof => nameof.OrganisationId == id && nameof.IsActive == true).ToList();
                return organisationDetails;
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Department Service: GetDepartmentsByOrganisationId(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
             
         }
           public object ErrorMessage(string ValidationMessage)
        {
            return new{message=ValidationMessage};
        }
       
    }

   
}

