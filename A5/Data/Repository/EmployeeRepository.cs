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
        //Gets Employee of particular HR by using HR id.
        public IEnumerable<Employee> GetByHR(int id)
        {
            if(id<=0) throw new ValidationException("Id should not be null or negative");
            try
            {
                var result=_context.Set<Employee>().Where(nameof => nameof.HRId == id && nameof.IsActive == true).ToList();
                if(result==null) throw new ValidationException("No records found");
                return result;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetByHR(id : {id}) : (Error:{Message})",id, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetByHR(id : {id}) : (Error:{Message})",id, exception.Message);
                throw;
            }

        }
       //Gets Employee of particular reporting person by using reporting person id.
        public IEnumerable<Employee> GetByReportingPerson(int id)
        {
            if(id<=0) throw new ValidationException("Id should not be null or negative");
            try
            {
                var result= _context.Set<Employee>().Where(nameof => nameof.ReportingPersonId == id).ToList();
                if(result==null) throw new ValidationException("No records found");
                return result;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetByReportingPerson(id : {id}) : (Error:{Message})", id,exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetByReportingPerson(id : {id}) : (Error:{Message})", id,exception.Message);
                throw;
            }

        }
        //Gets Employees of particular department using Department Id.
        public IEnumerable<Employee> GetEmployeeByDepartmentId(int id)
        {
           if(id<=0) throw new ValidationException("id should not be  null or negative");
            try
            {
                var result= _context.Set<Employee>().Where(nameof => nameof.DepartmentId == id).ToList();
                if(result==null) throw new ValidationException("No records found");
                return result;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeByDepartmentId(int id) : (Error:{Message})", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeByDepartmentId(int id) : (Error:{Message})", exception.Message);
                throw;
            }
        }
        //Gets reporting Person of particular department using department id.
        public IEnumerable<Employee> GetReportingPersonByDepartmentId(int id)
        {
            if(id<=0) throw new ValidationException("Department Id should not be null or negative");
            try
            {

                var result=_context.Set<Employee>().Where(nameof => nameof.DepartmentId == id && nameof.Designation!.DesignationName != "Trainee").ToList();
                if(result==null) throw new ValidationException("No records found");
                return result;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetReportingPersonByDepartmentId(id : {id}) : (Error:{Message})",id, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetReportingPersonByDepartmentId(id : {id}) : (Error:{Message})",id, exception.Message);
                throw;
            }
        }
        //Gets HR of particular department using Department Id.
        public IEnumerable<Employee> GetHrByDepartmentId(int id)
        {
            if(id<=0) throw new ValidationException("Department Id should not be null or negative");
            try
            {
                var result= _context.Set<Employee>().Where(nameof => nameof.DepartmentId == id && nameof.Designation!.DesignationName == "hr").ToList();
                if(result==null) throw new ValidationException("No records found");
                return result;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetHrByDepartmentId(id : {id} ): (Error:{Message})", id,exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetHrByDepartmentId(id : {id} ): (Error:{Message})", id,exception.Message);
                throw;
            }
        }
        //Gets Employee details of particular requester using Requester Id.
        public IEnumerable<Employee> GetEmployeeByRequesterId(int id)
        {          
           if(id<=0) throw new ValidationException("Requester id should not be null or negative");
            try
            {
                var result= _context.Set<Employee>().Where(nameof => nameof.ReportingPersonId == id && nameof.IsActive == true).ToList();
                if(result==null) throw new ValidationException("No records found");
                return result;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeByRequesterId(Id : {Id}) : (Error:{Message})", id,exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeByRequesterId(Id : {Id}) : (Error:{Message})", id,exception.Message);
                throw;
            }
        }
        //Gets employee details of particular organisation using organisation Id.
        public IEnumerable<Employee> GetEmployeeByOrganisation(int id)
        {
           if(id<=0) throw new ValidationException("Organisation Id should not be null or negative");
            try
            {
                var result = _context.Set<Employee>().Where(nameof => nameof.IsActive == true && nameof.OrganisationId == id).ToList();
                if(result==null) throw new ValidationException("No records found");
                return result;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeByOrganisation(id : {id}) : (Error:{Message})",id, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeByOrganisation(id : {id}) : (Error:{Message})", id,exception.Message);
                throw;
            }

        }
        //Gets all the employees.
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
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetAllEmployees() : (Error:{Message})", exception.Message);
                throw;
            }

        }
        //Get employee details of first 3 designations.
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
            catch (Exception exception)
            {
                 _logger.LogError("EmployeeRepository: GetUserDetails() : (Error:{Message})", exception.Message);
                throw;
            }
        }

        //Gets Details of Particular employee using employee id.

        public Employee? GetEmployeeById(int id)
        {
           if(id<=0) throw new ValidationException("Id should not be null or negative");
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
                if(employee==null) throw new ValidationException("No records found");
                return employee;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeById(Id: {id}) : (Error:{Message})",id, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeById(Id: {id}) : (Error:{Message})",id, exception.Message);
                throw;
            }
        }
        //Gets Employee using email and password.
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
                _logger.LogError("EmployeeRepository: GetEmployee(Email : {Email},Password : {Password}) : (Error:{Message})",Email,Password, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployee(Email : {Email},Password : {Password}) : (Error:{Message})", Email,Password, exception.Message);
                throw;
            }
        }
        //Creates employee by using employee object.
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
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: CreateEmployee(Employee employee) : (Error:{Message})", exception.Message);
                throw;
            }

        }
        //Updates employee by using Employee object.
        public bool UpdateEmployee(Employee employee)
        {
            EmployeeServiceValidations.UpdateValidation(employee);
            try
            {
                return Update(employee);
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: UpdateEmployee(Employee employee) : (Error:{Message})", exception.Message);
                throw;
            }

        }
        //Disable employee using employee id and current user id.
        public bool DisableEmployee(int id,int employeeId)
        {
            EmployeeServiceValidations.DisableValidation(id);
            if(id<=0) throw new ValidationException("Id should not be null or negative");
            if(employeeId<=0) throw new ValidationException("Employee id should not be null or negative");
            try
            {
                return Disable(id,employeeId);
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: DisableEmployee(id:{id},employeeId:{employeeId}) : (Error:{Message})",id,employeeId, exception.Message);
                throw;
            }

        }
        //Gets employee count using employee id.
        public int GetEmployeeCount(int id)
        {
            if(id<=0) throw new ValidationException("Employee Id should not be null or negative");
            try
            {

                var checkEmployee = _context.Set<Employee>().Where(nameof => nameof.IsActive == true && nameof.HRId == id || nameof.ReportingPersonId == id).Count();
                return checkEmployee;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeCount(id:{id}) : (Error:{Message})", id,exception.Message);
                throw;
            }

        }
        public object ErrorMessage(string ValidationMessage)
        {
            return new { message = ValidationMessage };
        }


    }

}