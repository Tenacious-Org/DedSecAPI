using System.ComponentModel.DataAnnotations;
using A5.Data;
using A5.Data.Repository;
using A5.Data.Repository.Interface;
using A5.Models;
using A5.Service.Validations;
using Microsoft.EntityFrameworkCore;

namespace A5.Data.Repository
{
    public class EmployeeRepository : EntityBaseRepository<Employee>,IEmployeeRepository
    {
        private readonly AppDbContext _context;
         private readonly ILogger<EntityBaseRepository<Employee>> _logger;

        public EmployeeRepository(AppDbContext context,ILogger<EntityBaseRepository<Employee>> logger) : base(context,logger)
        {
            _context = context;
            _logger=logger;
        }

        public IEnumerable<Employee> GetByHR(int id)
        {
            EmployeeServiceValidations.ValidateGetByHr(id);
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.HRId == id && nameof.IsActive == true).ToList();
            }
            catch(ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetByHR(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
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
            catch(ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetByReportingPerson(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
            
        }

        public IEnumerable<Employee> GetEmployeeByDepartmentId(int id)
        {
            EmployeeServiceValidations.ValidateGetByDepartment(id);
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.DepartmentId == id).ToList();
            }
           catch(ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetEmployeeByDepartmentId(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        public IEnumerable<Employee> GetReportingPersonByDepartmentId(int id)
        {
            EmployeeServiceValidations.ValidateGetByDepartment(id);
            try
            {
                
                return _context.Set<Employee>().Where(nameof => nameof.DepartmentId == id && nameof.Designation!.DesignationName!="Trainee" ).ToList();
            }
             catch(ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetReportingPersonByDepartmentId(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        public IEnumerable<Employee> GetHrByDepartmentId(int id)
        {
            EmployeeServiceValidations.ValidateGetByDepartment(id);
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.DepartmentId == id && nameof.Designation!.DesignationName=="hr").ToList();
            }
            catch(ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetHrByDepartmentId(id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
         public IEnumerable<Employee> GetEmployeeByRequesterId(int id)
        {
            EmployeeServiceValidations.ValidateGetByRequester(id);
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.ReportingPersonId == id && nameof.IsActive==true).ToList();
            }
            catch(ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetEmployeeByRequesterId(Designation) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        public IEnumerable<Employee> GetEmployeeByOrganisation(int id)
        {
            EmployeeServiceValidations.ValidateGetByOrganisation(id);
            try
            {
                var result = _context.Set<Employee>().Where(nameof =>nameof.IsActive == true && nameof.OrganisationId == id).ToList();
                return result;
            }
            catch(ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetEmployeeByOrganisation(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
            
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                var employee = _context.Set<Employee>()
                    .Include(e=>e.Designation!.Department!.Organisation)
                    .Include("Designation.Department")
                    .Include("Designation")
                    .Include("ReportingPerson")
                    .Include("HR")
                    .Where(nameof => nameof.IsActive == true && nameof.ReportingPersonId!=null&&nameof.HRId!=null)
                    .ToList();
                return employee;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Master Repository: GetAllEmployees() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
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
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Master Repository: GetUserDetails() : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
      

        public Employee? GetEmployeeById(int id)
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
                    .FirstOrDefault(nameof =>nameof.Id == id);
                return employee;
            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Master Repository: GetEmployeeById(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

         public bool GeneratePassword(Employee employee,int id)
         {
         
           bool result=false;
            try{
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
                 result=true;
                 return result;            
                }
                catch(ValidationException exception)
            {
                _logger.LogError("EmployeeService: GeneratePassword(Employee employee,int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
            }
            public bool ChangePassword(Employee employee,int id,String Email)
            {
                bool result=false;
                try{
                    
                      EmployeeServiceValidations employeeServiceValidations=new EmployeeServiceValidations(_context);
                      employeeServiceValidations.PasswordValidation(employee,id,Email);
                      _context.Set<Employee>().Update(employee);
                      _context.SaveChanges();
                      result=true;
                       return result;
                }
                catch(ValidationException exception)
            {
                _logger.LogError("EmployeeService: ChangePassword(Employee employee,int id,String Email) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
            }

            public Employee GetEmployee(string Email, string Password) 
            {
            if(Email == null || Password ==null) throw new ValidationException("Email or Password cannot be null");
            try
            {
                var User =GetUserDetails().FirstOrDefault(user => user.Email == Email && user.Password==Password);
                if (User == null) throw new ValidationException("Invalid user");
                return User;
               
            }
            catch(ValidationException exception)
            {
                _logger.LogError("EmployeeService: GetEmployee(string Email,string Password) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

        public bool CreateEmployee(Employee employee)
        {
            
            try{
                return Create(employee);
            }
             catch(ValidationException exception)
            {
                _logger.LogError("EmployeeService: CreateEmployee(Employee employee) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }

        }
        public bool UpdateEmployee(Employee employee)
        {
            try{
                return Update(employee);
            }
             catch(ValidationException exception)
            {
                _logger.LogError("EmployeeService: UpdateEmployee(Employee employee) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }

        }
        public bool DisableEmployee(int id)
        {
            try{
                return Disable(id);          
            }
             catch(ValidationException exception)
            {
                _logger.LogError("EmployeeService: UpdateEmployee(Employee employee) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }

        }
        public int GetEmployeeCount(int id){
            try
            {

            var checkEmployee = _context.Set<Employee>().Where(nameof =>nameof.IsActive == true && nameof.HRId== id || nameof.ReportingPersonId== id  ).Count();
            return checkEmployee;
                }
            catch (ValidationException exception)
            {
                _logger.LogError("EmployeeService: ChangePassword(Employee employee,int id,String Email) : (Error:{Message}", exception.Message);
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
            return new{message=ValidationMessage};
        }

    
    }
    
}