using System.ComponentModel.DataAnnotations;
using A5.Data;
using A5.Data.Repository;
using A5.Data.Repository.Interface;
using A5.Models;
using A5.Service.Validations;
using Microsoft.EntityFrameworkCore;

namespace A5.Data.Repository
{
    public class EmployeeRepository : EntityBaseRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EntityBaseRepository<Employee>> _logger;

        public EmployeeRepository(AppDbContext context, ILogger<EntityBaseRepository<Employee>> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Employee> GetByHR(int id)
        {
            EmployeeServiceValidations.ValidateGetByHr(id);
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.HRId == id && nameof.IsActive == true).ToList();
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetByHR(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }

        }

        public IEnumerable<Employee> GetByReportingPerson(int id)
        {
            EmployeeServiceValidations.ValidateGetByReportingPerson(id);
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.ReportingPersonId == id).ToList();
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetByReportingPerson(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }

        }

        public IEnumerable<Employee> GetEmployeeByDepartmentId(int id)
        {
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.DepartmentId == id).ToList();
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeByDepartmentId(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        public IEnumerable<Employee> GetReportingPersonByDepartmentId(int id)
        {
           
            try
            {

                return _context.Set<Employee>().Where(nameof => nameof.DepartmentId == id && nameof.Designation!.DesignationName != "Trainee").ToList();
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetReportingPersonByDepartmentId(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        public IEnumerable<Employee> GetHrByDepartmentId(int id)
        {
           
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.DepartmentId == id && nameof.Designation!.DesignationName == "hr").ToList();
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetHrByDepartmentId(id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        public IEnumerable<Employee> GetEmployeeByRequesterId(int id)
        {
           
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.ReportingPersonId == id && nameof.IsActive == true).ToList();
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeByRequesterId(Designation) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }
        public IEnumerable<Employee> GetEmployeeByOrganisation(int id)
        {
           
            try
            {
                var result = _context.Set<Employee>().Where(nameof => nameof.IsActive == true && nameof.OrganisationId == id).ToList();
                return result;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeByOrganisation(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }

        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                var employee = _context.Set<Employee>()
                    .Include(e => e.Designation!.Department!.Organisation)
                    .Include(e => e.Designation!.Department)
                    .Include("Designation")
                    .Include("ReportingPerson")
                    .Include("HR")
                    .Where(nameof => nameof.IsActive == true && nameof.ReportingPersonId != null && nameof.HRId != null)
                    .ToList();
                return employee;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                _logger.LogInformation("EmployeeRepository: GetAllEmployees() : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }

        }

        public IEnumerable<Employee> GetUserDetails()
        {
            try
            {
                var employee = _context.Set<Employee>()
                    .Include("Designation.Department.Organisation")
                    .Include("Designation.Department")
                    .Include("Designation")
                    .Include("ReportingPerson")
                    .Include("HR")
                    .Where(nameof => nameof.IsActive == true)
                    .ToList();
                return employee;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                _logger.LogInformation("EmployeeRepository: GetUserDetails() : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }


        public Employee? GetEmployeeById(int id)
        {
            EmployeeServiceValidations.ValidateGetById(id);
            try
            {
                var employee = _context.Set<Employee>()
                    .Include("Designation.Department.Organisation")
                    .Include("Designation.Department")
                    .Include("Designation")
                    .Include("ReportingPerson")
                    .Include("HR")
                    .Where(nameof => nameof.IsActive == true)
                    .FirstOrDefault(nameof => nameof.Id == id);
                return employee;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                _logger.LogInformation("EmployeeRepository: GetEmployeeById(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }

        public Employee GetEmployee(string Email, string Password)
        {
            if (Email == null || Password == null) throw new ValidationException("Email or Password cannot be null");
            try
            {
                
                var User = GetUserDetails().FirstOrDefault(user => user.Email == Email && user.Password == Password);
                if (User == null) throw new ValidationException("Invalid user");
                return User;

            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployee(string Email,string Password) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }

        public bool CreateEmployee(Employee employee)
        {
            EmployeeServiceValidations.CreateValidation(employee);
            bool IsIdAlreadyExists=_context.Employees!.Any(nameof=>nameof.ACEID==employee.ACEID);
            if(IsIdAlreadyExists) throw new ValidationException("Employee Id already exists");
            bool IsEmailAlreadyExists=_context.Employees!.Any(nameof=>nameof.Email==employee.Email);
            if(IsEmailAlreadyExists) throw new ValidationException("Email Id already exists");
            try
            {
                return Create(employee);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: CreateEmployee(Employee employee) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }

        }
        public bool UpdateEmployee(Employee employee)
        {
            EmployeeServiceValidations.UpdateValidation(employee);
            bool IsIdAlreadyExists=_context.Employees!.Any(nameof=>nameof.ACEID==employee.ACEID);
            if(IsIdAlreadyExists) throw new ValidationException("Employee Id already exists");
            bool IsEmailAlreadyExists=_context.Employees!.Any(nameof=>nameof.Email==employee.Email);
            if(IsEmailAlreadyExists) throw new ValidationException("Email Id already exists");
            try
            {
                return Update(employee);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: UpdateEmployee(Employee employee) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }

        }
        public bool DisableEmployee(int id,int employeeId)
        {
            EmployeeServiceValidations.DisableValidation(id);
            try
            {
                return Disable(id,employeeId);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: UpdateEmployee(Employee employee) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }

        }

        public int HRID(int id)
        {
            
            foreach(var q in _context.Set<Employee>().Where(nameof => nameof.Id == id)){
                var answer = q.HRId;
            }

            var hr = 0;
            
            return hr;
            
        }
        public int GetEmployeeCount(int id)
        {
            try
            {

                var checkEmployee = _context.Set<Employee>().Where(nameof => nameof.IsActive == true && nameof.HRId == id || nameof.ReportingPersonId == id).Count();
                return checkEmployee;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeCount(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }

        }
        public object ErrorMessage(string ValidationMessage)
        {
            return new { message = ValidationMessage };
        }


    }

}