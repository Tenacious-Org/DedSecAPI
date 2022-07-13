using A5.Models;
using A5.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace A5.Service.Validations
{
    public static class OrganisationServiceValidations
    {

       public static bool CreateValidation(Organisation organisation)
        {        
            if(organisation==null) throw new ValidationException("Organisation should not be null");   
            if(String.IsNullOrWhiteSpace(organisation.OrganisationName)) throw new ValidationException("Organisation Name should not be null or Empty.");
            if(!( Regex.IsMatch(organisation.OrganisationName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("Organisation Name should have only alphabets.No special Characters or numbers are allowed");
            if(organisation.IsActive == false) throw new ValidationException("Organisation should be Active when it is created.");
            if(organisation.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        public static bool UpdateValidation(Organisation organisation)
        {

            if(string.IsNullOrWhiteSpace(organisation.OrganisationName)) throw new ValidationException("Organisation name should not be null or empty");          
            if(!( Regex.IsMatch(organisation.OrganisationName, @"^[A-z][A-z|\.|\s]+$"))) throw new ValidationException("Namse should have only alphabets.No special Characters or numbers are allowed");
            if(organisation.IsActive == false) throw new ValidationException("To update organisation it should be active");
            if(organisation.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            if(organisation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
         public static bool DisableValidation(int id)
        {
            if(id <= 0) throw new ValidationException("Organisation Id must be greater than Zero.");
            else return true;
        }
         public static bool ValidateGetById(int id)
        {
            
            if(id == 0) throw new ValidationException("Organisation Id should not be null.");
            else if(id<0) throw new ValidationException("Negative values are not accepted");
            else return true;
        }

    }
}