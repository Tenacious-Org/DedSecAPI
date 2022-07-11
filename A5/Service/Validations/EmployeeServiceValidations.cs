using A5.Models;
using System.ComponentModel.DataAnnotations;
using A5.Data;
using System.Text.RegularExpressions;
namespace A5.Service.Validations
{
    public class EmployeeServiceValidations
    {
        private readonly AppDbContext _context;
        public EmployeeServiceValidations(AppDbContext context)
        {
            _context=context;
        }
         public bool CreateValidation(Employee employee)
        {
             if(!ValidateAceId(employee.ACEID)) throw new ValidationException("ID should begin with ACE"); 
             if(string.IsNullOrEmpty(employee.FirstName)) throw new ValidationException("Employee's first name should not be null or empty");
             if(string.IsNullOrEmpty(employee.LastName)) throw new ValidationException("Employee's last name should not be null or empty");
            // if(_context.Employees.Any(nameof=>nameof.FirstName==employee.FirstName)) throw new ValidationException("First name already exists");                   
             if(!( Regex.IsMatch(employee.FirstName, @"^[a-zA-Z]+$"))) throw new ValidationException("First Name should have only alphabets.No special Characters or numbers are allowed");
             if(!( Regex.IsMatch(employee.LastName, @"^[a-zA-Z]+$"))) throw new ValidationException("Last Name should have only alphabets.No special Characters or numbers are allowed");   
            // if(ValidateDOB(employee.DOB)) throw new ValidationException("Invalid Date Of Birth!");
             if(employee.IsActive==false) throw new ValidationException("Employee should be active when it is created");
             if(employee.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
             else return true;
        }
        public bool UpdateValidation(Employee employee)
        {
           
            if(string.IsNullOrEmpty(employee.FirstName)) throw new ValidationException("Employee name should not be null or empty");
            if(string.IsNullOrEmpty(employee.LastName)) throw new ValidationException("Employee name should not be null or empty");
            if(_context.Employees.Any(nameof=>nameof.FirstName==employee.FirstName)) throw new ValidationException("First name already exists");           
            if(!( Regex.IsMatch(employee.FirstName, @"^[a-zA-Z]+$"))) throw new ValidationException("First Name should have only alphabets.No special Characters or numbers are allowed");
            if(employee.IsActive==false) throw new ValidationException("Employee should be active when it is created");
            if(employee.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            if(employee.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        public bool ValidateGetById(int id)
        {
            Employee employee = new Employee();
            if(id==0) throw new ValidationException("Employee Id should not be null.");
            return true;
        }
        public bool DisableValidation(int id)
        {
            Employee employee = new Employee();
            if(id==0) throw new ValidationException("Employee Id should not be null.");
            if(employee.IsActive == false) throw new ValidationException("Employee is already disabled");
            if(employee.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        
        public static void ValidateGetByHr(int id)
        {
            if(id==0) throw new ValidationException("HR Id should not be null");
        }
        public static void ValidateGetByReportingPerson(int id)
        {
            if(id==0) throw new ValidationException("Reporting Person Id should not be null");
        }
        public static void ValidateGetByDepartment(int id)
        {
            if(id==0) throw new ValidationException("Department Id should not be null");
        }
       public static void ValidateGetByRequester(int id)
       {
            if(id==0) throw new ValidationException("Requester ID should not be null");
       }
       public static void ValidateGetByOrganisation(int id)
       {
           if(id==0) throw new ValidationException("Organisation id should not be null");
       }
       public static void GetEmployeeById(int id)
       {
        if(id==0) throw new ValidationException("Employee id should not be null");
       }
                  

      public bool  PasswordValidation(Employee employee,int id,string Email)
      {
        if(!(_context.Employees.Any(nameof=>nameof.Email==employee.Email))) throw new ValidationException("Email not found");
        if(string.IsNullOrEmpty(employee.Password)) throw new ValidationException("Password should not be null");
        if (!Regex.IsMatch(employee.Password,@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$")) throw new ValidationException ("Password must be between 8 and 15 characters and atleast contain one uppercase letter, one lowercase letter, one digit and one special character.");
        else return true;
      }
    //   public bool ValidateDOB(DateTime DOB)
    //   {
    //     int age = 0;  
    //     int year=DOB.Year;
    //     int Current_year=DateTime.Today.Year;
    //     age = Current_year-year;
    //     if(DOB>=DateTime.Now) return false;      
    //     else if(age<18 || age>60) return false;
    //     else return true;
    //   }
    public bool ValidateAceId(string ACEID)
    {
        char[] charArray=ACEID.ToCharArray();
        if(!((charArray[0]=='A' && charArray[1]=='C' && charArray[2]=='E'  ))) return false;
        else return true;
    }
    }
}