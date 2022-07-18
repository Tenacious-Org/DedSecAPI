using System.ComponentModel.DataAnnotations;
using A5.Data.Repository.Interface;
using A5.Models;
using A5.Service.Validations;
using Microsoft.EntityFrameworkCore;

namespace A5.Data.Repository
{
    public class DepartmentRepository : EntityBaseRepository<Department>, IDepartmentRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EntityBaseRepository<Department>> _logger;
        public DepartmentRepository(AppDbContext context, ILogger<EntityBaseRepository<Department>> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public bool CreateDepartment(Department department)
        {
            DepartmentServiceValidations.CreateValidation(department);
            bool NameExists = _context.Departments!.Any(nameof => nameof.DepartmentName == department.DepartmentName && nameof.OrganisationId == department.OrganisationId);
            if (NameExists) throw new ValidationException("Department Name already exists");
            try
            {
                return Create(department);
            }
            catch (Exception exception)
            {
                _logger.LogError("DepartmentRepository: CreateDepartment(Departmetn department) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        public bool UpdateDepartment(Department department)
        {
            DepartmentServiceValidations.UpdateValidation(department);
            bool NameExists = _context.Departments!.Any(nameof => nameof.DepartmentName == department.DepartmentName && nameof.OrganisationId == department.OrganisationId);
            if (NameExists) throw new ValidationException("Department Name already exists");
            try
            {
                return Update(department);
            }
            catch (Exception exception)
            {
                _logger.LogError("DepartmentRepository: UpdateDepartment(Department department) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        public Department? GetDepartmentById(int id)
        {
            if(id<=0) throw new ValidationException("Department Id should not be null or negative");
            try
            {
                return GetById(id);
            }
            catch (Exception exception)
            {
                _logger.LogError("DepartmentRepository: GetDepartmentById({id}) : (Error:{Message}",id, exception.Message);
                throw;
            }
        }
        public bool DisableDepartment(int id, int employeeId)
        {
            if(id<=0) throw new ValidationException("Department Id should not be null or negative");
            if(employeeId<=0) throw new ValidationException("Employee Id should not be null or negative");
            try
            {
                return Disable(id, employeeId);
            }
            catch (Exception exception)
            {
                _logger.LogError("DepartmentRepository: DisableDepartment(Id:{id},EmployeeId:{employeeId}) : (Error:{Message}", id,employeeId,exception.Message);
                throw;
            }
        }

        public IEnumerable<Department> GetAllDepartment()
        {
            try
            {
                var departments = _context.Set<Department>().Where(nameof => nameof.IsActive).Include("Organisation").ToList();
                return departments;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
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
            if(id<=0) throw new ValidationException("Organisation Id should not be null or negative");
            try
            {
                var organisationDetails = _context.Set<Department>().Where(nameof => nameof.OrganisationId == id && nameof.IsActive == true).ToList();
                return organisationDetails;
            }
            catch (Exception exception)
            {
                _logger.LogError("DepartmentRepository: GetDepartmentsByOrganisationId({id}) : (Error:{Message}", id,exception.Message);
                throw;
            }

        }
        public object ErrorMessage(string ValidationMessage)
        {
            return new { message = ValidationMessage };
        }

    }
}