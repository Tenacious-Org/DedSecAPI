using A5.Models;
using A5.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace A5.Service.Validations
{
    public static class AwardTypeValidations
    {
       
         public static bool CreateValidation(AwardType awardType)
        {             
            if(String.IsNullOrWhiteSpace(awardType.AwardName)) throw new ValidationException("Award Name should not be null or Empty.");
            if(!( Regex.IsMatch(awardType.AwardName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("Award Name should have only alphabets.No special Characters or numbers are allowed");
            if(awardType.IsActive == false) throw new ValidationException("Award should be Active when it is created.");
            if(awardType.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero."); 
            else return true;
        }
         public static bool UpdateValidation(AwardType awardType)
        {
            if(string.IsNullOrWhiteSpace(awardType.AwardName)) throw new ValidationException("Award name should not be null or empty");
            if(!( Regex.IsMatch(awardType.AwardName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("Award Name should have only alphabets.No special Characters or numbers are allowed");
            if(awardType.IsActive == false) throw new ValidationException("To update award details Award should be active");
            if(awardType.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            if(awardType.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        public static bool DisableValidation(int id)
        {
            AwardType awardType=new AwardType();
            if(id==0) throw new ValidationException ("Enter the id to update");         
            if(awardType.IsActive==false) throw new ValidationException("AwardType is already disabled");
            else if(awardType.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        public static bool ValidateGetById(int id)
        {
            AwardType awardType=new AwardType();
            if(id==0) throw new ValidationException("Award Id should not be null.");
            else return true;
        }
        
    }
}