using System.Collections.Generic;
using System.Linq;
using A5.Models;
using A5.Data.Repository;
using A5.Service.Interfaces;
using A5.Data.Repository;
using A5.Service.Validations;
using System.ComponentModel.DataAnnotations;
using A5.Data;
using A5.Data.Service.Interfaces;

namespace A5.Service
{
    public class EmployeeService : EntityBaseRepository<Employee>, IEmployeeService
    {
        private readonly AppDbContext _context;
        private readonly MasterRepository _master;
        public EmployeeService(AppDbContext context, MasterRepository master) : base(context)
        {
            _context = context;
            _master = master;
        }

        public IEnumerable<Employee> GetByHR(int id)
        {
            EmployeeServiceValidations.ValidateGetByHr(id);
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.HRId == id && nameof.IsActive == true).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }

        public IEnumerable<Employee> GetByReportingPerson(int id)
        {
            EmployeeServiceValidations.ValidateGetByReportingPerson(id);
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.ReportingPersonId == id).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }

        public IEnumerable<Employee> GetEmployeeByDepartmentId(int id)
        {
            EmployeeServiceValidations.ValidateGetByDepartment(id);
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.DepartmentId == id).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public IEnumerable<Employee> GetReportingPersonByDepartmentId(int id)
        {
            EmployeeServiceValidations.ValidateGetByDepartment(id);
            try
            {
                
                return _context.Set<Employee>().Where(nameof => nameof.DepartmentId == id && nameof.Designation.DesignationName!="Trainee" ).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public IEnumerable<Employee> GetHrByDepartmentId(int id)
        {
            EmployeeServiceValidations.ValidateGetByDepartment(id);
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.DepartmentId == id && nameof.Designation.DesignationName=="hr").ToList();
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
         public IEnumerable<Employee> GetEmployeeByRequesterId(int id)
        {
            EmployeeServiceValidations.ValidateGetByRequester(id);
            try
            {
                return _context.Set<Employee>().Where(nameof => nameof.ReportingPersonId == id).ToList();
            }
            catch(Exception exception)
            {
                throw exception;
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
            catch(Exception exception)
            {
                throw exception;
            }
            
        }
        public IEnumerable<object> GetAllEmployees()
         {
             try
             {
                 var employee = _master.GetAllEmployees();
                    return employee.Select( Employee => new{
                            id = Employee.Id,
                            aceid = Employee.ACEID,
                            firstName = Employee.FirstName,
                            lastName = Employee.LastName,
                            fullName= Employee.FirstName+" "+Employee.LastName,
                            email = Employee.Email,
                            image = Employee.Image,
                            gender = Employee.Gender,
                            dob = Employee.DOB,
                            organisationName = Employee?.Designation?.Department?.Organisation?.OrganisationName,
                            departmentName = Employee?.Designation?.Department?.DepartmentName,
                            designationName = Employee?.Designation?.DesignationName,
                            reportingPersonName = Employee?.ReportingPerson?.FirstName,
                            hRName = Employee?.HR?.FirstName,
                            password = Employee?.Password,
                            isActive = Employee?.IsActive,
                            addedBy = Employee?.AddedBy,
                            addedOn = Employee?.AddedOn,
                            updatedBy = Employee?.UpdatedBy,
                            updatedOn = Employee?.UpdatedOn
                    });
             }
             catch(Exception exception)
             {
                 throw exception;
             }
            
         }
         public object GetEmployeeById(int id)
         {
            EmployeeServiceValidations.GetEmployeeById(id);
             try
             {
                 var employee = _master.GetEmployeeById(id);
                 return  new{
                            id = employee.Id,
                            aceid = employee.ACEID,
                            firstName = employee.FirstName,
                            lastName = employee.LastName,
                            email = employee.Email,
                            image = employee.Image,
                            gender = employee.Gender,
                            dob = employee.DOB,
                            organisationId = employee.OrganisationId,
                            departmentId = employee.DepartmentId,
                            designationId = employee.DesignationId,
                            organisationName = employee.Designation.Department.Organisation.OrganisationName,
                            departmentName = employee.Designation.Department.DepartmentName,
                            designationName = employee.Designation.DesignationName,
                            reportingPersonId=employee.ReportingPersonId,
                            hrId=employee.HRId,
                            reportingPersonName = employee.ReportingPerson.FirstName,
                            hRName = employee.HR.FirstName,
                            password = employee.Password,
                            isActive = employee.IsActive,
                            addedBy = employee.AddedBy,
                            addedOn = employee.AddedOn,
                            updatedBy = employee.UpdatedBy,
                            updatedOn = employee.UpdatedOn
                };
             }
             catch(Exception exception)
             {
                 throw exception;
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
                 //employee.Id=id;
                 employee.Password = passwordString;
                 _context.SaveChanges();
                 result=true;
                 return result;            
                }
                catch(Exception exception)
                {
                  throw exception;
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
                catch(Exception exception)
                {
                    throw exception;
                }
            }

             public Employee GetEmployee(string Email, string Password) 
        {
            if(Email == null || Password ==null) throw new ArgumentNullException("Email or Password cannot be null");
            try
            {
                //var Hasher = PasswordHasherFactory.GetPasswordHasherFactory();
                var User =_master.GetUserDetails().ToList().Find(user => user.Email == Email && user.Password==Password);
                if (User == null) throw new ValidationException("Invalid user");
                return User;
                //return Hasher.VerifyHashedPassword(User, User.Password, Password) == PasswordVerificationResult.Success ? User : throw new InvalidDataException("Password doesn't match");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool CreateEmployee(Employee employee)
        {
            EmployeeServiceValidations employeeServiceValidations=new EmployeeServiceValidations(_context);
            employeeServiceValidations.CreateValidation(employee);
            try{
                return Create(employee);
            }
            catch(Exception exception)
            {
                throw exception;
            }

        }
    
    }
    
    
}