using A5.Data.Repository;
using A5.Data.Repository.Interface;
using A5.Service.Interfaces;
using A5.Models;
using A5.Service.Validations;
using System.ComponentModel.DataAnnotations;
using A5.Data;
using System.Linq;

namespace A5.Service
{
    public class AwardService:IAwardService
    {
       
        private readonly AwardRepository _award;
        private readonly ILogger<IAwardService> _logger;
        public AwardService(AwardRepository awardRepository,ILogger<IAwardService> logger)
        {         
            _award=awardRepository;
            _logger=logger;
           
        }
        public bool RaiseRequest(Award award,int employeeId)
        {
             
            if(!AwardServiceValidations.RequestValidation(award,employeeId)) throw new ValidationException("Invalid data");
            try{
               return _award.RaiseAwardRequest(award,employeeId);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("AwardService: RaiseRequest(Award award,int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

        public bool Approval(Award award,int employeeId)
        {
          
            try{
                return  _award.ApproveRequest(award,employeeId);           
            }
            catch(ValidationException exception)
            {
                _logger.LogError("AwardService: Approval(Award award,int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
        
        
        public object GetAwardById(int id)
        {
            if(!AwardServiceValidations.ValidateGetAwardById(id)) throw new ValidationException("Invalid data");
            try{
                var award= _award.GetAwardById(id);
                return new{
                    id = award?.Id,
                    requesterId=award?.RequesterId,
                    awardeeId=award?.AwardeeId,
                    awardTypeId=award?.AwardTypeId,
                    approverId=award?.ApproverId,
                    hRId=award?.HRId,
                    reason=award?.Reason,
                    rejectedReason=award?.RejectedReason,
                    couponCode=award?.CouponCode,
                    statusId=award?.StatusId,
                    addedBy=award?.AddedBy,
                    addedOn=award?.AddedOn,
                    updatedBy=award?.UpdatedBy,
                    updatedOn=award?.UpdatedOn,
                    aceId=award?.Awardee?.ACEID,
                    awardeeName = award?.Awardee?.FirstName + " "+ award?.Awardee?.LastName  ,
                    awardeeImage=award?.Awardee?.Image,
                    gender=award?.Awardee?.Gender,
                    requesterName = award?.Awardee?.ReportingPerson?.FirstName,
                    approverName = award?.Awardee?.ReportingPerson?.ReportingPerson?.FirstName,
                    hRName = award?.Awardee?.HR?.FirstName,
                    status=award?.Status?.StatusName,
                    award=award?.AwardType?.AwardName,
                    awardImage=award?.AwardType?.Image,
                    designation=award?.Awardee?.Designation?.DesignationName,
                    department=award?.Awardee?.Designation?.Department?.DepartmentName,
                    organisation=award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName
                };
            }
           
           catch(ValidationException exception)
            {
                _logger.LogError("AwardService: GetAwardById(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }


        public bool AddComment(Comment comment)
        {
            if(!AwardServiceValidations.ValidateAddComment(comment)) throw new ValidationException("Invalid data");
            try{
                  return _award.AddComments(comment);
            }
           catch(ValidationException exception)
            {
                _logger.LogError("AwardService: AddComment(Comment comment) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
      

        public IEnumerable<object> GetAwardsList(int ? pageId,int ? employeeId)
        {
            try
            {
                var awards = _award.GetAllAwardsList(pageId,employeeId);
                return awards.Select( Award => new{
                    id = Award.Id,
                    awardeeName = Award?.Awardee?.FirstName,
                    awardeeImage=Award?.Awardee?.Image,
                    requesterName = Award?.Awardee?.ReportingPerson?.FirstName,
                    approverName = Award?.Awardee?.ReportingPerson?.ReportingPerson?.FirstName,
                    hRName = Award?.Awardee?.HR?.FirstName,
                    statusId=Award?.StatusId,
                    status=Award?.Status?.StatusName,
                    awardName=Award?.AwardType?.AwardName,
                    awardTypeId=Award?.AwardType?.Id,
                    awardImage=Award?.AwardType?.Image,
                    reason=Award?.Reason,
                    rejectedReason=Award?.RejectedReason,
                    organisationId=Award?.Awardee?.Designation?.Department?.Organisation?.Id,
                    departmentId=Award?.Awardee?.Designation?.Department?.Id,
                    designation=Award?.Awardee?.Designation?.DesignationName,
                    department=Award?.Awardee?.Designation?.Department?.DepartmentName,
                    organisation=Award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName ,
                    updatedOn=Award?.UpdatedOn
                });
            }
           catch(ValidationException exception)
            {
                _logger.LogError("AwardService: GetAwardsList(int ? pageId,int ? employeeId) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

        public IEnumerable<object> GetComments(int awardId)
        {
            if(!AwardServiceValidations.ValidateGetComments(awardId)) throw new ValidationException("Invalid data");
            try{
                 var comments=_award.GetComments(awardId);
                   return comments.Select( Comment =>  new{
                   id=Comment.Id,
                   comments=Comment.Comments,
                   gender=Comment?.Employees?.Gender,
                   employeeName=Comment?.Employees?.FirstName,
                   employeeImage=Comment?.Employees?.Image,
                   commentedOn=Comment?.CommentedOn
               }).OrderByDescending(nameof=>nameof.id);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("AwardService: GetComments(int awardId) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }

        public object ErrorMessage(string ValidationMessage)
        {
            return new{message=ValidationMessage};
        }

    }
}