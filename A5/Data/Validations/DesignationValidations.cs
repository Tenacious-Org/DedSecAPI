using A5.Data;
using A5.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace A5.Data.Validations
{
    public static class DesignationValidations
    {
        

        public static bool CreateValidation(Designation designation)
        {           
            if(String.IsNullOrWhiteSpace(designation.DesignationName)) throw new ValidationException("Designation Name should not be null or Empty.");
            if(!( Regex.IsMatch(designation.DesignationName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("Designation Name should have only alphabets.No special Characters or numbers are allowed");
            if(designation.IsActive == false) throw new ValidationException("Designation should be Active when it is created.");
            if(designation.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        public static bool UpdateValidation(Designation designation)
        {
            if(string.IsNullOrWhiteSpace(designation.DesignationName)) throw new ValidationException("Designation name should not be null or empty");
            if(!( Regex.IsMatch(designation.DesignationName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("Designation Name should have only alphabets.No special Characters or numbers are allowed");
            if(designation.IsActive == false) throw new ValidationException("To update designation it should be active");
            if(designation.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            if(designation.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
         public static bool ValidateGetById(int id)
        {
            Designation designation = new Designation();
            if((id == 0)) throw new ValidationException("Designation Id should not be null.");
            else return true;
        }
         
        
        public static bool ValidateGetByDepartment(int id)
        {
            if(id==0) throw new ValidationException("Organisation should not be null");
            else return true;

        }
    }
}