using System.ComponentModel.DataAnnotations;
using A5.Data.Repository.Interface;
using A5.Models;
using A5.Data.Validations;
using Microsoft.EntityFrameworkCore;

namespace A5.Data.Repository
{
    public class DepartmentRepository : EntityBaseRepository<Department>, IDepartmentRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EntityBaseRepository<Department>> _logger;
        private readonly DepartmentValidations _departmentvalidations;
        public DepartmentRepository(AppDbContext context, ILogger<EntityBaseRepository<Department>> logger,DepartmentValidations departmentvalidations) : base(context, logger)
        {
            _context = context;
            _logger = logger;
            _departmentvalidations=departmentvalidations;
        }
          //creates department using department object.

        public bool CreateDepartment(Department department)
        {
           if (department == null) throw new ValidationException("Department should not be null");
            _departmentvalidations.CreateValidation(department);
            try
            {
                return Create(department);
            }

            catch (Exception exception)
            {
                _logger.LogError("DepartmentRepository: CreateDepartment(Department department) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        //updates department using department object.
        public bool UpdateDepartment(Department department)
        {
            if (department == null) throw new ValidationException("Department should not be null");
            _departmentvalidations.UpdateValidation(department);  
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
        //Gets department using Department Id
        public Department? GetDepartmentById(int departmentId)
        {
            if(departmentId<=0) throw new ValidationException("Department Id must be greater than zero");
            try
            {
                return GetById(departmentId);
            }
            catch (Exception exception)
            {
                _logger.LogError("DepartmentRepository: GetDepartmentById({departmentId}) : (Error:{Message}",departmentId, exception.Message);
                throw;
            }
        }
         //Disables department using department id and current user id.
        public bool DisableDepartment(int departmentId, int userId)
        {
              if (departmentId <= 0) throw new ValidationException("Department Id must be greater than zero");
            if (userId <= 0) throw new ValidationException("User Id must be greater than zero");
            _departmentvalidations.DisableValidation(userId);
            try
            {
                return Disable(departmentId, userId);
            }
            catch (Exception exception)
            {
                _logger.LogError("DepartmentRepository: DisableDepartment(departmentId:{departmentId},EmployeeId:{employeeId}) : (Error:{Message}", departmentId,userId,exception.Message);
                throw;
            }
        }
        //Gets all the department.
        public IEnumerable<Department> GetAllDepartment()
        {
            try
            {
                var departments = _context.Set<Department>().Where(nameof => nameof.IsActive).Include("Organisation").ToList();
                return departments;
            }
            catch (Exception exception)
            {
                _logger.LogError("DepartmentRepository: GetAllDepartment() : (Error:{Message}",exception.Message);
                throw;
            }
        }
        //Gets the count of employees under department.
        public int GetCount(int departmentId)
        {
            if (departmentId <= 0) throw new ValidationException("Department Id must be greater than zero");
            var checkEmployee = _context.Set<Employee>().Where(nameof => nameof.IsActive == true && nameof.DepartmentId == departmentId).Count();
            return checkEmployee;
        }
        //Gets Department by organisation Id.
        public IEnumerable<Department> GetDepartmentsByOrganisationId(int organisationId)
        {
            if(organisationId<=0) throw new ValidationException("Organisation Id must be greater than zero");
            try
            {
                var organisationDetails = _context.Set<Department>().Where(nameof => nameof.OrganisationId == organisationId && nameof.IsActive == true).ToList();
                return organisationDetails;
            }
            catch (Exception exception)
            {
                _logger.LogError("DepartmentRepository: GetDepartmentsByOrganisationId({organisationId}) : (Error:{Message}", organisationId,exception.Message);
                throw;
            }

        }

    }
}