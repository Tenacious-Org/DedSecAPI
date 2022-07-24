using A5.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using A5.Data;

namespace A5.Data.Validations
{
    public class DepartmentValidations
    {
        private readonly AppDbContext _context;
        private readonly UserValidations _userValidations;

        public DepartmentValidations(AppDbContext context, UserValidations userValidations)
        {
            _context = context;
            _userValidations = userValidations;
        }

        public bool CreateValidation(Department department)
        {
            if (department.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            _userValidations.AdminValidation(department.AddedBy);
            CommonValidations(department);
            return true;
        }

        public bool UpdateValidation(Department department)
        {
            if (department.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            _userValidations.AdminValidation(department.UpdatedBy);
            CommonValidations(department);
            return true;
        }

        public bool DisableValidation(int userId)
        {
            if (userId <= 0) throw new ValidationException("User Id must be greater than zero");
            _userValidations.AdminValidation(userId);
            return true;
        }

        public bool CommonValidations(Department department)
        {
            if (string.IsNullOrWhiteSpace(department.DepartmentName)) throw new ValidationException("Department Name should not be null or Empty.");
            if (!(Regex.IsMatch(department.DepartmentName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("Department Name should have only alphabets.No special Characters or numbers are allowed");
            if (_context.Departments!.Any(nameof => nameof.DepartmentName == department.DepartmentName && nameof.OrganisationId == department.OrganisationId && nameof.Id != department.Id)) throw new ValidationException("Department Name already exists");
            if (department.OrganisationId <= 0) throw new ValidationException("Organisation Id Should not be Zero or less than zero.");
            if (department.IsActive == false) throw new ValidationException("Department should be Active when it is created.");
            else return true;
        }



    }
}