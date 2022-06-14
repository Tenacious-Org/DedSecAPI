using A5.Data.Service;
using A5.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace A5.Validations
{
    public class AwardServiceValidations
    {
        public static void RequestValidation(Award award,int id)
        {
            if(award.AwardeeId==0) throw new ValidationException("Awardee should not be null");
            if(award.AwardTypeId==0) throw new ValidationException("Award Type Should not be null");
            if(award.Reason==null) throw new ValidationException("Reason for award should not be null");
           
        }
        public static void ValidateRequestedAward(int employeeId)
        {
            if(employeeId==0) throw new ValidationException("Id is null.Login to get the List of Requested Awards");
        }
        public static void ValidateGetAwardById(int id)
        {
            if(id==0) throw new ValidationException("No Award exists in that ID.Enter the correct id");
        }
        public static void ValidateAddComment(Comment comment)
        {
            if(comment.Comments==null) throw new ValidationException("Comments should not be null");
        }
    }
}