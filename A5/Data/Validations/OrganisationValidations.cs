using A5.Models;
using A5.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace A5.Data.Validations
{
    public class OrganisationValidations
    {
        private readonly AppDbContext _context;
        private readonly UserValidations _userValidations;

        public OrganisationValidations(AppDbContext context, UserValidations userValidations)
        {
            _context = context;
            _userValidations = userValidations;
        }

        public bool CreateValidation(Organisation organisation)
        {
            if (organisation == null) throw new ValidationException("Organisation should not be null");
            if (organisation.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            _userValidations.AdminValidation(organisation.AddedBy);
            CommonValidations(organisation);
            return true;
        }
        public bool UpdateValidation(Organisation organisation)
        {
            if (organisation == null) throw new ValidationException("Organisation should not be null");
            if (organisation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            _userValidations.AdminValidation(organisation.UpdatedBy);
            CommonValidations(organisation);
            return true;
        }

        public bool DisableValidation(int userId)
        {
            if (userId <= 0) throw new ValidationException("User Id must be greater than zero");
            _userValidations.AdminValidation(userId);
            return true;
        }

        public bool CommonValidations(Organisation organisation)
        {
            if (String.IsNullOrWhiteSpace(organisation.OrganisationName)) throw new ValidationException("Organisation Name should not be null or Empty.");
            if (!(Regex.IsMatch(organisation.OrganisationName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("Organisation Name should have only alphabets.No special Characters or numbers are allowed");
            if (organisation.IsActive == false) throw new ValidationException("Organisation should be Active when it is created.");
            return true;
        }



    }
}