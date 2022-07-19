using A5.Service;
using A5.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace A5.Service.Validations
{
    public static class AwardServiceValidations
    {
        public static bool RequestValidation(Award award)
        {
            if(award.AwardeeId==0) throw new ValidationException("Awardee not found");
            if(award.AwardTypeId==0) throw new ValidationException("Award Type Should not be null");
            if(string.IsNullOrWhiteSpace(award.Reason)) throw new ValidationException("Reason for award should not be null");
            else return true;
           
        }
        public static bool ValidateRequestedAward(int employeeId)
        {
            if(employeeId==0) throw new ValidationException("Id is null.Login to get the List of Requested Awards");
            else return true;
        }

        public static bool ValidateAddComment(Comment comment)
        {
            if(comment==null) throw new ValidationException("Comment hshould not be null");
            if(string.IsNullOrEmpty(comment.Comments)) throw new ValidationException("Comments should not be null");
            else return true;
        }
        public static bool ValidateGetComments(int awardId)
        {
            if(awardId<=0) throw new ValidationException ("No such awards!!");
            else return true;
        }
        
    }
}