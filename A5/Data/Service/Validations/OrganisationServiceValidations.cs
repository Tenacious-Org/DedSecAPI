using A5.Models;
using A5.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace A5.Data.Service.Validations
{
    public class OrganisationServiceValidations
    {
        private readonly AppDbContext _context;
        public OrganisationServiceValidations(AppDbContext context)
        {
            _context=context;
        }

      

        public static bool CreateValidation(Organisation organisation)
        {        
            if(organisation==null) throw new ValidationException("Organisation should not be null");   
            if(String.IsNullOrEmpty(organisation.OrganisationName)) throw new ValidationException("Organisation Name should not be null or Empty.");
            //if(_context.Organisations.Any(nameof=>nameof.OrganisationName==organisation.OrganisationName)) throw new ValidationException("organisation name already exists");           
            if(!( Regex.IsMatch(organisation.OrganisationName, @"^[a-zA-Z]+$"))) throw new ValidationException("Organisation Name should have only alphabets.No special Characters or numbers are allowed");
            if(organisation.IsActive == false) throw new ValidationException("Organisation should be Active when it is created.");
            if(organisation.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        public static bool UpdateValidation(Organisation organisation)
        {

            if(string.IsNullOrEmpty(organisation.OrganisationName)) throw new ValidationException("Organisation name should not be null or empty");          
            if(!( Regex.IsMatch(organisation.OrganisationName, @"^[a-zA-Z]+$"))) throw new ValidationException("Namse should have only alphabets.No special Characters or numbers are allowed");
            if(organisation.IsActive == false) throw new ValidationException("To update organisation it should be active");
            if(organisation.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            if(organisation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
         public static bool DisableValidation(int id)
        {
            //Organisation organisation=new Organisation();
            if(id <= 0) throw new ValidationException("Organisation Id must be greater than Zero.");
            // if(organisation.IsActive == false) throw new ValidationException("Organisation is already disabled");
            // if(organisation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
         public static bool ValidateGetById(int id)
        {
            
            if(id == 0) throw new ValidationException("Organisation Id should not be null.");
            else return true;
        }
        // public int GetCount(int id)
        // {
        //      var checkEmployee = _context.Set<Employee>().Where(nameof =>nameof.IsActive == true && nameof.OrganisationId == id).ToList().Count();
        //      return checkEmployee;
        // }

    }
}