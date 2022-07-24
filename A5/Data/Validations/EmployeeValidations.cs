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
            CommonValidations(employee);
            return true;
        }
        public bool UpdateValidation(Employee employee)
        {
            if (employee.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            _userValidations.AdminValidation(employee.UpdatedBy);
            CommonValidations(employee);
            return true;

        }
        public bool DisableValidation(int userId)
        {
            if (userId <= 0) throw new ValidationException("User Id must be greater than zero");
            _userValidations.AdminValidation(userId);
            return true;
        }


        public bool CredentialsValidation(Login credentials)
        {
            if (string.IsNullOrEmpty(credentials.Password)) throw new ValidationException("Password should not be null");
            if (!(Regex.IsMatch(credentials.Email, @"^[^@\s]+@[^@\s]+\.(com|org|in)$"))) throw new ValidationException("Email id is not valid.");
            if (!Regex.IsMatch(credentials.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$")) throw new ValidationException("Password must be between 8 and 15 characters and atleast contain one uppercase letter, one lowercase letter, one digit and one special character.");
            return true;
        }

        public bool CommonValidations(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.FirstName)) throw new ValidationException("Employee's first name should not be null or empty");
            if (string.IsNullOrWhiteSpace(employee.LastName)) throw new ValidationException("Employee's last name should not be null or empty");
            if (!(Regex.IsMatch(employee.FirstName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("First name should have only alphabets.No special Characters or numbers are allowed");
            if (!(Regex.IsMatch(employee.LastName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("Last name should have only alphabets.No special Characters or numbers are allowed");
            ValidateDOB(employee.DOB);
            if (string.IsNullOrWhiteSpace(employee.Email)) throw new ValidationException("Email should not be null or empty");
            if (!(Regex.IsMatch(employee.Email, @"^[^@\s]+@[^@\s]+\.(com|org|in)$"))) throw new ValidationException("Email id is not valid.");
            if (_context.Employees!.Any(nameof => nameof.ACEID == employee.ACEID && nameof.Id != employee.Id)) throw new ValidationException("ACE Id already exists");
            if (_context.Employees!.Any(nameof => nameof.Email == employee.Email && nameof.Id != employee.Id)) throw new ValidationException("Email Id already exists");
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
            int year = DOB.Year;
            int Current_year = DateTime.Today.Year;
            age = Current_year - year;
            if (DOB >= DateTime.Now) throw new ValidationException("Date of birth cannot be a future date");
            else if (age <= 18) throw new ValidationException("Age must be greater than 18");
            else if (age >= 60) throw new ValidationException("Age must be les than 60");
            else return true;
        }
    }
}