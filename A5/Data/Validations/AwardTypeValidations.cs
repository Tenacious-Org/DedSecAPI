using A5.Models;
using A5.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace A5.Data.Validations
{
    public class AwardTypeValidations
    {
        private readonly AppDbContext _context;
        private readonly UserValidations _userValidations;
        public AwardTypeValidations(AppDbContext context, UserValidations userValidations)
        {
            _context = context;
            _userValidations = userValidations;
        }
        public bool CreateValidation(AwardType awardType)
        {
            if (awardType.AddedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            _userValidations.AdminValidation(awardType.AddedBy);
            CommonValidations(awardType);
            return true;
        }
        public bool UpdateValidation(AwardType awardType)
        {
            if (awardType.UpdatedBy <= 0) throw new ValidationException("User Id Should not be Zero or less than zero.");
            _userValidations.AdminValidation(awardType.UpdatedBy);
            CommonValidations(awardType);
            return true;
        }

        public bool DisableValidation(int userId)
        {
            if (userId <= 0) throw new ValidationException("User Id must be greater than zero");
            _userValidations.AdminValidation(userId);
            return true;
        }

        public bool CommonValidations(AwardType awardType)
        {
            if (String.IsNullOrWhiteSpace(awardType.AwardName)) throw new ValidationException("Award Name should not be null or Empty.");
            if (!(Regex.IsMatch(awardType.AwardName, @"^[a-zA-Z\s]+$"))) throw new ValidationException("Award Name should have only alphabets.No special Characters or numbers are allowed");
            if(_context.AwardTypes!.Any(nameof=>nameof.AwardName==awardType.AwardName && nameof.Id != awardType.Id)) throw new ValidationException("Award Name already exists");
            if (String.IsNullOrWhiteSpace(awardType.AwardDescription)) throw new ValidationException("Award Description should not be null or Empty.");
            if (awardType.IsActive == false) throw new ValidationException("Award should be Active when it is created.");
            return true;
        }


    }
}