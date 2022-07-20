using A5.Service;
using A5.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace A5.Data.Validations
{
    public  class AwardValidations
    {
        private readonly AppDbContext _context;
        public AwardValidations(AppDbContext context)
        {
            _context=context;
        }
        public bool RequestValidation(Award award,int employeeId)
        {
            if(award.AwardeeId==0) throw new ValidationException("Awardee Id not found");
            if(award.AwardTypeId==0) throw new ValidationException("Award Id Should not be zero");
            if(string.IsNullOrWhiteSpace(award.Reason)) throw new ValidationException("Reason for award should not be null");
            if (!(_context.Awards!.Any(nameof=>nameof.Awardee.ReportingPersonId == employeeId))) throw new ValidationException("Reporting Person Id not found");         
            else return true;

        }
        public static bool ValidateRequestedAward(int employeeId)
        {
            if(employeeId==0) throw new ValidationException("Id is null.Login to get the List of Requested Awards");
            else return true;
        }

        public static bool ValidateAddComment(Comment comment)
        {
            if(string.IsNullOrEmpty(comment.Comments)) throw new ValidationException("Comments should not be null");
            else return true;
        }
        public static bool ValidateGetComments(int awardId)
        {
            if(awardId<=0) throw new ValidationException ("No such awards!!");
            else return true;
        }

        public static bool ApprovalValidation(Award award)
        {
            if(award.ApproverId==null)throw new ValidationException("Approver Id should not be null");
            if(award.StatusId==3)throw new ValidationException("Rejection Reason should not be null");
            else return true;

        }

        
    }
}