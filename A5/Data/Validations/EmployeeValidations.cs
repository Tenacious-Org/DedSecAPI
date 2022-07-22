using A5.Models;
using System.ComponentModel.DataAnnotations;
using A5.Data;
using System.Text.RegularExpressions;
namespace A5.Data.Validations
{
    public class EmployeeValidations
    {
        private readonly AppDbContext _context;
        private readonly UserValidations _userValidations;

        public EmployeeValidations(AppDbContext context, UserValidations userValidations)
        {
            _context = context;
            _userValidations = userValidations;
        }
        public bool CreateValidation(Employee employee)
        {
            if (employee.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            _userValidations.AdminValidation(employee.AddedBy);
            bool IsAceIdAlreadyExists = _context.Employees!.Any(nameof => nameof.ACEID == employee.ACEID);
            bool IsEmailAlreadyExists = _context.Employees!.Any(nameof => nameof.Email == employee.Email);
            if (IsAceIdAlreadyExists) throw new ValidationException("Employee Id already exists");
            if (IsEmailAlreadyExists) throw new ValidationException("Email Id already exists"); if (string.IsNullOrWhiteSpace(employee.FirstName)) throw new ValidationException("Employee's first name should not be null or empty");
            CommonValidations(employee);
            return true;
        }
        public bool UpdateValidation(Employee employee)
        {
            if (employee.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            _userValidations.AdminValidation(employee.UpdatedBy);
            Employee ExistingEmployee = _context.Set<Employee>().FirstOrDefault(nameof => nameof.Id == employee.Id);
            if (ExistingEmployee.ACEID != employee.ACEID)
            {
                bool IsAceIdAlreadyExists = _context.Employees!.Any(nameof => nameof.ACEID == employee.ACEID);
                if (IsAceIdAlreadyExists) throw new ValidationException("Ace Id already exists");
            }
            if (ExistingEmployee.Email != employee.Email)
            {
                bool IsEmailAlreadyExists = _context.Employees!.Any(nameof => nameof.Email == employee.Email);
                if (IsEmailAlreadyExists) throw new ValidationException("Email Id already exists");
            }
            CommonValidations(employee);
            return true;

        }
        public bool DisableValidation( int userId)
        {
            if(userId<=0) throw new ValidationException("User Id must be greater than zero");
            _userValidations.AdminValidation(userId);
            return true;
        }


        public bool CredentialsValidation(Login credentials)
        {
            if (string.IsNullOrEmpty(credentials.Password)) throw new ValidationException("Password should not be null");
            if (!Regex.IsMatch(credentials.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$")) throw new ValidationException("Password must be between 8 and 15 characters and atleast contain one uppercase letter, one lowercase letter, one digit and one special character.");
            return true;
        }

        public bool CommonValidations(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.LastName)) throw new ValidationException("Employee's last name should not be null or empty");
            if (!(Regex.IsMatch(employee.FirstName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("First Name should have only alphabets.No special Characters or numbers are allowed");
            if (!(Regex.IsMatch(employee.LastName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("Last Name should have only alphabets.No special Characters or numbers are allowed");
            ValidateDOB(employee.DOB);
            if (string.IsNullOrWhiteSpace(employee.Email)) throw new ValidationException("Email should not be null or empty");
            if(!(Regex.IsMatch(employee.Email,@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))) throw new ValidationException("Email id is not valid to enter into database");
            if (string.IsNullOrWhiteSpace(employee.Gender)) throw new ValidationException("Gender should not be null or empty");
            if (string.IsNullOrWhiteSpace(employee.ImageString)) throw new ValidationException("Image is required");
            if (employee.OrganisationId <= 0) throw new ValidationException("Organisation Id Should not be Zero or less than zero.");
            if (employee.DepartmentId <= 0) throw new ValidationException("Department Id Should not be Zero or less than zero.");
            if (employee.DesignationId <= 0) throw new ValidationException("Designation Id Should not be Zero or less than zero.");
            if (employee.ReportingPersonId <= 0) throw new ValidationException("ReportingPerson Id Should not be Zero or less than zero.");
            if (employee.HRId <= 0) throw new ValidationException("HR Id Should not be Zero or less than zero.");
            if (employee.IsActive == false) throw new ValidationException("Employee should be active when it is created");
            return true;
        }
       public bool ValidateDOB(DateTime DOB)
      {
        int age = 0;  
        int year=DOB.Year;
        int Current_year=DateTime.Today.Year;
        age = Current_year-year;
        if(DOB>=DateTime.Now) throw new ValidationException("Date of birth cannot be a future date");      
        else if(age<=18) throw new ValidationException("Employee is too small to join in this organisation");
        else if(age>=60) throw new ValidationException("Employee got the retirement age already" );
        else return true;
      }
    }
}