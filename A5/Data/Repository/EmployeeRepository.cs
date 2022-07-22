using System.ComponentModel.DataAnnotations;
using A5.Data;
using A5.Data.Repository;
using A5.Data.Repository.Interface;
using A5.Models;
using A5.Data.Validations;
using Microsoft.EntityFrameworkCore;
using A5.Service;

namespace A5.Data.Repository
{
    public class EmployeeRepository : EntityBaseRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EntityBaseRepository<Employee>> _logger;
        private readonly EmployeeValidations _employeeValidations;
        private readonly MailService _mail;


        public EmployeeRepository(AppDbContext context, ILogger<EntityBaseRepository<Employee>> logger,EmployeeValidations employeeValidations) : base(context, logger)
        {
            _context = context;
            _logger = logger;
            _employeeValidations=employeeValidations;
        }
      
        //Gets reporting Person of particular department using department id.
        public IEnumerable<Employee> GetReportingPersonByDepartmentId(int departmentId)
        {
            if (departmentId <= 0) throw new ValidationException("Department Id must be greater than zero");
            try
            {

                var result=_context.Set<Employee>().Where(nameof => nameof.DepartmentId == departmentId && nameof.Designation!.DesignationName != "Trainee").ToList();
                if(result==null) throw new ValidationException("No records found");
                return result;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetReportingPersonByDepartmentId(id : {id}) : (Error:{Message})",departmentId, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetReportingPersonByDepartmentId(id : {id}) : (Error:{Message})",departmentId, exception.Message);
                throw;
            }
        }
        //Gets HR of particular department using Department Id.
        public IEnumerable<Employee> GetHrByDepartmentId(int departmentId)
        {
            if(departmentId<=0) throw new ValidationException("Department Id should not be null or negative");
            try
            {
                var result= _context.Set<Employee>().Where(nameof => nameof.DepartmentId == departmentId && nameof.Designation!.DesignationName == "hr").ToList();
                if(result==null) throw new ValidationException("No records found");
                return result;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetHrByDepartmentId(id : {id} ): (Error:{Message})", departmentId,exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetHrByDepartmentId(id : {id} ): (Error:{Message})", departmentId,exception.Message);
                throw;
            }
        }
        //Gets Employee details of particular requester using Requester Id.
        public IEnumerable<Employee> GetEmployeeByRequesterId(int requesterId)
        {          
           if(requesterId<=0) throw new ValidationException("Requester id should not be null or negative");
            try
            {
                var result= _context.Set<Employee>().Where(nameof => nameof.ReportingPersonId == requesterId && nameof.IsActive == true).ToList();
                if(result==null) throw new ValidationException("No records found");
                return result;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeByRequesterId(Id : {Id}) : (Error:{Message})", requesterId,exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeByRequesterId(Id : {Id}) : (Error:{Message})", requesterId,exception.Message);
                throw;
            }
        }
        //Gets employee details of particular organisation using organisation Id.
        public IEnumerable<Employee> GetEmployeeByOrganisation(int organisationId)
        {
           if(organisationId<=0) throw new ValidationException("Organisation Id should not be null or negative");
            try
            {
                var result = _context.Set<Employee>().Where(nameof => nameof.IsActive == true && nameof.OrganisationId == organisationId).ToList();
                if(result==null) throw new ValidationException("No records found");
                return result;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeByOrganisation(id : {id}) : (Error:{Message})",organisationId, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeByOrganisation(id : {id}) : (Error:{Message})", organisationId,exception.Message);
                throw;
            }

        }
        //Gets all the employees except reporting person and hr is null .
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
                    .OrderByDescending(x => x.Id).ToList();
                return employee;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetAllEmployees() : (Error:{Message})", exception.Message);
                throw;
            }

        }
        

        //Gets Details of Particular employee using employee id.

        public Employee? GetEmployeeById(int employeeId)
        {
           if(employeeId<=0) throw new ValidationException("Id should not be null or negative");
            try
            {
                var employee = _context.Set<Employee>()
                    .Include("Designation.Department.Organisation")
                    .Include("Designation.Department")
                    .Include("Designation")
                    .Include("ReportingPerson")
                    .Include("HR")
                    .Where(nameof => nameof.IsActive == true)
                    .FirstOrDefault(nameof => nameof.Id == employeeId);
                if(employee==null) throw new ValidationException("No records found");
                return employee;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeById(Id: {id}) : (Error:{Message})",employeeId, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeById(Id: {id}) : (Error:{Message})",employeeId, exception.Message);
                throw;
            }
        }
        //Gets Employee using email and password.
        public Employee GetEmployee(string Email, string Password)
        {
            if (Email == null || Password == null) throw new ValidationException("Email or Password cannot be null");
            try
            {
                
                var User = _context.Set<Employee>().Include("Designation").FirstOrDefault(user => user.Email == Email && user.Password == Password);
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
        
        public bool ForgotPassword(string aceId,string emailId){
            try
            { 
                var User = _context.Set<Employee>().FirstOrDefault(user => user.ACEID == aceId && user.Email == emailId);
                if (User == null) throw new ValidationException("User details not found");
                _mail.ForgotAsync(User);
                return true;

            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: ForgotPassword(AceID : {aceId},Email : {emailId}) : (Error:{Message})",aceId,emailId, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: ForgotPassword(AceID : {aceId},Email : {emailId}) : (Error:{Message})", aceId,emailId, exception.Message);
                throw;
            }
        }

        //Creates employee by using employee object.
        public bool CreateEmployee(Employee employee)
        {
           if (employee == null) throw new ValidationException("Employee should not be null");
            _employeeValidations.CreateValidation(employee);
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
            if (employee == null) throw new ValidationException("Employee should not be null");
            _employeeValidations.UpdateValidation(employee);
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
        public bool DisableEmployee(int employeeId,int userId)
        {
             if (employeeId <= 0) throw new ValidationException("Employee id must be greater than zero");
            if (userId <= 0) throw new ValidationException(" User id must be greater than zero");
            _employeeValidations.DisableValidation(userId);
            try
            {
                return Disable(employeeId,userId);
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: DisableEmployee(id:{id},employeeId:{employeeId}) : (Error:{Message})",employeeId,userId, exception.Message);
                throw;
            }

        }
        //Gets employee count using employee id.

        public int HRID(int id)
        {
            
            foreach(var q in _context.Set<Employee>().Where(nameof => nameof.Id == id)){
                var answer = q.HRId;
            }

            var hr = 0;
            
            return hr;
            
        }
        public int GetEmployeeCount(int employeeId)
        {
            if(employeeId<=0) throw new ValidationException("Employee Id should not be zero or negative");
            try
            {

                var checkEmployee = _context.Set<Employee>().Where(nameof => nameof.IsActive == true && nameof.HRId == employeeId || nameof.ReportingPersonId == employeeId).Count();
                return checkEmployee;
            }
            catch (Exception exception)
            {
                _logger.LogError("EmployeeRepository: GetEmployeeCount(id:{id}) : (Error:{Message})", employeeId,exception.Message);
                throw;
            }

        }
        public object ErrorMessage(string ValidationMessage)
        {
            return new { message = ValidationMessage };
        }


    }

}