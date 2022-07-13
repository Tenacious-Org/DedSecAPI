using System.ComponentModel.DataAnnotations;
using A5.Data.Repository.Interface;
using A5.Models;
using A5.Service.Validations;
using Microsoft.EntityFrameworkCore;

namespace A5.Data.Repository
{
    public class DepartmentRepository:EntityBaseRepository<Department>,IDepartmentRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EntityBaseRepository<Department>> _logger;
        public DepartmentRepository(AppDbContext context,ILogger<EntityBaseRepository<Department>> logger):base(context,logger)
        {
             _context=context;
             _logger=logger;
        }
         public bool CreateDepartment(Department department)
        {
            if(!DepartmentServiceValidations.CreateValidation(department)) throw new ValidationException("Invalid data");
            bool NameExists=_context.Departments!.Any(nameof=>nameof.DepartmentName==department.DepartmentName);
            if(NameExists) throw new ValidationException("Department Name already exists");
            try{
                return Create(department);
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
             bool NameExists=_context.Departments!.Any(nameof=>nameof.DepartmentName==department.DepartmentName);
            if(NameExists) throw new ValidationException("Department Name already exists");
            try{
                return Update(department);
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
                return GetById(id);
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
                return Disable(id);

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
       
         public IEnumerable<Department> GetAllDepartment()
        {
            try
            {
                var departments = _context.Set<Department>().Where(nameof =>nameof.IsActive).Include("Organisation").ToList();
                return departments;
            }
           catch(ValidationException exception)
            {
                _logger.LogError("MasterRepository: GetAllDepartments() : (Error:{Message}",exception.Message);
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
             var checkEmployee = _context.Set<Employee>().Where(nameof => nameof.IsActive == true && nameof.DepartmentId == id).Count();
             return checkEmployee;
        }
        public IEnumerable<Department> GetDepartmentsByOrganisationId(int id)
         { 
            if(!DepartmentServiceValidations.ValidateGetByOrganisation(id)) throw new ValidationException("Invalid data");
            try
            {
                var organisationDetails = _context.Set<Department>().Where(nameof => nameof.OrganisationId == id && nameof.IsActive == true).ToList();
                return organisationDetails;
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
           public object ErrorMessage(string ValidationMessage)
        {
            return new{message=ValidationMessage};
        }
       
    }
}