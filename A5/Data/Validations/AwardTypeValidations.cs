using A5.Models;
using A5.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace A5.Data.Validations
{
    public static class AwardTypeValidations
    {

        public static bool CreateValidation(AwardType awardType)
        {
            if (String.IsNullOrWhiteSpace(awardType.AwardName)) throw new ValidationException("Award Name should not be null or Empty.");
            if (!(Regex.IsMatch(awardType.AwardName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("Award Name should have only alphabets.No special Characters or numbers are allowed");
            if (String.IsNullOrWhiteSpace(awardType.AwardDescription)) throw new ValidationException("Award Description should not be null or Empty.");
            if (awardType.IsActive == false) throw new ValidationException("Award should be Active when it is created.");
            if (awardType.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
        public static bool UpdateValidation(AwardType awardType)
        {
            if (string.IsNullOrWhiteSpace(awardType.AwardName)) throw new ValidationException("Award name should not be null or empty");
            if (!(Regex.IsMatch(awardType.AwardName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("Award Name should have only alphabets.No special Characters or numbers are allowed");
            if (String.IsNullOrWhiteSpace(awardType.AwardDescription)) throw new ValidationException("Award Description should not be null or Empty.");
            if (awardType.IsActive == false) throw new ValidationException("To update award details Award should be active");
            if (awardType.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            else return true;
        }
    

    }
}