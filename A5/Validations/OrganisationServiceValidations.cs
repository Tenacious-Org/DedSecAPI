using A5.Models;
using A5.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace A5.Validations
{
    public class OrganisationServiceValidations
    {
        private readonly AppDbContext _context;
        public OrganisationServiceValidations(AppDbContext context)
        {
            _context=context;
        }
        public bool CreateValidation(Organisation organisation)
        {           
            if(String.IsNullOrEmpty(organisation.OrganisationName)) throw new ValidationException("Organisation Name should not be null or Empty.");
            if(_context.Organisations.Any(nameof=>nameof.OrganisationName==organisation.OrganisationName)) throw new ValidationException("organisation name already exists");           
            if(!( Regex.IsMatch(organisation.OrganisationName, @"^[a-zA-Z]+$"))) throw new ValidationException("Organisation Name should have only alphabets.No special Characters or numbers are allowed");
            if(organisation.IsActive == false) throw new ValidationException("Organisation should be Active when it is created.");
            if(organisation.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        public bool UpdateValidation(Organisation organisation,int id)
        {
            if(id == 0) throw new ValidationException("Enter the id for which Organisation to be updated.");
            if(id!=organisation.Id) throw new ValidationException("Organisation Id nout found!");
            if(string.IsNullOrEmpty(organisation.OrganisationName)) throw new ValidationException("Organisation name should not be null or empty");
            if(_context.Organisations.Any(nameof=>nameof.OrganisationName==organisation.OrganisationName)) throw new ValidationException("organisation name already exists");           
            if(!( Regex.IsMatch(organisation.OrganisationName, @"^[a-zA-Z]+$"))) throw new ValidationException("Namse should have only alphabets.No special Characters or numbers are allowed");
            if(organisation.IsActive == false) throw new ValidationException("To update organisation it should be active");
            if(organisation.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            if(organisation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
         public bool DisableValidation(int id)
        {
            Organisation organisation=new Organisation();
            if(id <= 0) throw new ValidationException("Organisation Id must be greater than Zero.");
            if(id!=organisation.Id) throw new ValidationException("Organisation Id nout found!");
            if(organisation.IsActive == false) throw new ValidationException("Organisation is already disabled");
            if(organisation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
         public bool ValidateGetById(int id)
        {
            Organisation organisation = new Organisation();
            if(id == 0) throw new ValidationException("Organisation Id should not be null.");
            if(id!=organisation.Id) throw new ValidationException("Organisation Id nout found!");
            else return true;
        }

    }
}