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
            if(!EmployeeServiceValidations.ValidateGetByHr(id)) throw new ValidationException("Invalid data");
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
            if(!EmployeeServiceValidations.ValidateGetByReportingPerson(id)) throw new ValidationException("Invalid data");
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
            if(!EmployeeServiceValidations.ValidateGetByDepartment(id)) throw new ValidationException("Invalid data");
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
            if(!EmployeeServiceValidations.ValidateGetByDepartment(id)) throw new ValidationException("Invalid data");
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
            if(!EmployeeServiceValidations.ValidateGetByDepartment(id)) throw new ValidationException("Invalid data");
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
            if(!EmployeeServiceValidations.ValidateGetByRequester(id)) throw new ValidationException("Invalid data");
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
            if(!EmployeeServiceValidations.ValidateGetByOrganisation(id)) throw new ValidationException("Invalid data");
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
            if(!EmployeeServiceValidations.ValidateGetById(id)) throw new ValidationException("Invalid data");
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

        public bool GeneratePassword(Employee employee, int id)
        {

            bool result = false;
            try
            {
                _context.Set<Employee>().Update(employee);
                string allowedChars = "";
                allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
                allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
                allowedChars += "1,2,3,4,5,6,7,8,9,0,!,@,#,$,%,&,?";
                char[] sep = { ',' };
                string[] arr = allowedChars.Split(sep);
                string passwordString = "";
                string temp = "";
                Random rand = new Random();
                for (int i = 0; i < 8; i++)
                {
                    temp = arr[rand.Next(0, arr.Length)];
                    passwordString += temp;
                }
                employee.Password = passwordString;
                _context.SaveChanges();
                result = true;
                return result;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeRepository: GeneratePassword(Employee employee,int id) : (Error:{Message}", exception.Message);
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
            if(!EmployeeServiceValidations.CreateValidation(employee)) throw new ValidationException("Invalid data");
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
            if(!EmployeeServiceValidations.UpdateValidation(employee)) throw new ValidationException("Invalid data");
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
            if(!EmployeeServiceValidations.DisableValidation(id)) throw new ValidationException("Invalid data");
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
        public int GetEmployeeCount(int id)
        {
            if(!EmployeeServiceValidations.ValidateById(id)) throw new ValidationException("Invalid data");
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