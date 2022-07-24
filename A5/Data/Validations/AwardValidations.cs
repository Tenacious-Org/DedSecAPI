using A5.Service;
using A5.Models;
using System.ComponentModel.DataAnnotations;
namespace A5.Data.Validations
{
    public class AwardValidations
    {
        private readonly AppDbContext _context;
        public AwardValidations(AppDbContext context)
        {
            _context = context;
        }
        public bool RequestValidation(Award award, int userId)
        {
            if (award.AwardeeId <= 0) throw new ValidationException("Awardee Id must be greater than zero");
            var awardee = _context.Set<Employee>().FirstOrDefault(nameof => nameof.Id == award.AwardeeId);
            if (awardee?.ReportingPersonId != userId) throw new ValidationException("Reporting person Id not found");
            if (awardee.IsActive == false) throw new ValidationException("This Awardee is inactive. So unable to raise request ");
            if (award.AwardTypeId <= 0) throw new ValidationException("Award Type Id must be greater than zero");
            if (string.IsNullOrWhiteSpace(award.Reason)) throw new ValidationException("Reason for award should not be null");
            return true;
        }
        public bool ValidateAddComment(Comment comment)
        {
            if (string.IsNullOrWhiteSpace(comment.Comments)) throw new ValidationException("Comments should not be null");
            if (_context.Employees!.Any(nameof => nameof.Id != comment.EmployeeId)) throw new ValidationException("Invalid User , no access to comment");
            return true;
        }
        public bool ApprovalValidation(Award award)
        {
            if (award.UpdatedBy <= 0) throw new ValidationException("user id must be greater than zero");
            if (award.StatusId <= 1) throw new ValidationException("Status id must be greater than 1");
            if (award.StatusId == 2 || award.StatusId == 3)
            {
                if (award.ApproverId != award.UpdatedBy) throw new ValidationException("Approver Id not matched");
                var requester = _context.Set<Employee>().FirstOrDefault(nameof => nameof.Id == award.RequesterId);
                if (requester?.ReportingPersonId != award.UpdatedBy) throw new ValidationException("Approver Id not found");
                if (award.StatusId == 3 && String.IsNullOrWhiteSpace(award.RejectedReason)) throw new ValidationException("Rejection reason cannot be null");
            }
            if (award.StatusId == 4)
            {
                if (award.PublisherId != award.UpdatedBy) throw new ValidationException("Publisher Id not matched");
                var awardee = _context.Set<Employee>().FirstOrDefault(nameof => nameof.Id == award.AwardeeId);
                if (awardee?.HRId != award.UpdatedBy) throw new ValidationException("Publisher Id not found");
                if (String.IsNullOrWhiteSpace(award.CouponCode)) throw new ValidationException("Coupon code should not be null");
                if ((_context.Awards!.Any(nameof => nameof.CouponCode == award.CouponCode))) throw new ValidationException("Coupon code already exists");
            }
            return true;

        }


    }
}