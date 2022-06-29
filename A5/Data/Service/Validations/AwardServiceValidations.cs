using A5.Data.Service;
using A5.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace A5.Data.Service.Validations
{
    public class AwardServiceValidations
    {
        public static void RequestValidation(Award award,int id)
        {
            if(award.AwardeeId==0) throw new ValidationException("Awardee should not be null");
            if(award.AwardTypeId==0) throw new ValidationException("Award Type Should not be null");
            if(string.IsNullOrEmpty(award.Reason)) throw new ValidationException("Reason for award should not be null");
           
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
            if(comment==null) throw new ValidationException("Comment hshould not be null");
            if(string.IsNullOrEmpty(comment.Comments)) throw new ValidationException("Comments should not be null");
        }
        public static void ValidateGetComments(int awardId)
        {
            if(awardId==0) throw new ValidationException ("No such awards!!");
        }
        public static void ValidateGetAwardsList(int ? pageId,int ? employeeId)
        {
            if(!(pageId==1 || pageId==2 || pageId==3 || pageId==4)) throw new ValidationException("Page Id should be 1 or 2 or 3 or 4");
        }
    }
}