using A5.Data;
using A5.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using A5.Data.Validations;

namespace A5.Data.Validations
{
    public class DesignationValidations
    {
        private readonly AppDbContext _context;
        private readonly UserValidations _userValidations;
        public DesignationValidations(AppDbContext context, UserValidations userValidations)
        {
            _context = context;
            _userValidations = userValidations;
        }

        public bool CreateValidation(Designation designation)
        {
            if (designation.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            _userValidations.AdminValidation(designation.AddedBy);
            CommonValidations(designation);
            if (_context.Designations!.Any(nameof => nameof.DesignationName == designation.DesignationName && nameof.DepartmentId == designation.DepartmentId)) throw new ValidationException("Designation Name already exists");
            return true;
        }
        public bool UpdateValidation(Designation designation)
        {
            if (designation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            _userValidations.AdminValidation(designation.UpdatedBy);
            CommonValidations(designation);
            return true;
        }

        public bool DisableValidation(int userId)
        {
            if (userId <= 0) throw new ValidationException("User Id must be greater than zero");
            _userValidations.AdminValidation(userId);
            return true;
        }
        public bool CommonValidations(Designation designation)
        {
            if (String.IsNullOrWhiteSpace(designation.DesignationName)) throw new ValidationException("Designation Name should not be null or Empty.");
            if (!(Regex.IsMatch(designation.DesignationName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("Designation Name should have only alphabets.No special Characters or numbers are allowed");
            if (designation.DepartmentId <= 0) throw new ValidationException("Department Id must be greater than zero");
            if (designation.RoleId <= 0) throw new ValidationException("Role Id must be greater than zero");
            if (designation.IsActive == false) throw new ValidationException("To update designation it should be active");
            else return true;
        }

    }
}