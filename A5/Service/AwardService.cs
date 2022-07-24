using A5.Data.Repository;
using A5.Service.Interfaces;
using A5.Models;
using A5.Data.Validations;
using System.ComponentModel.DataAnnotations;
using A5.Data.Repository.Interface;

namespace A5.Service
{
    public class AwardService : IAwardService
    {
        private readonly AwardRepository _award;
        private readonly ILogger<IAwardService> _logger;
        private readonly AwardValidations _awardValidations;
        private readonly IEmployeeRepository _employeeRepository;
        public AwardService(AwardRepository awardRepository,ILogger<IAwardService> logger,AwardValidations awardValidations,IEmployeeRepository employeeRepository)
        {
            _award = awardRepository;
            _logger = logger;
            _awardValidations=awardValidations;
            _employeeRepository=employeeRepository;
        }

        //to raise award request using award object and employee id(requester)
        public bool RaiseRequest(Award award,int userId)
        {
            if(award==null) throw new ValidationException("award should not be null");
            if(userId<=0) throw new ValidationException("user id must be greater than zero");
           _awardValidations.RequestValidation(award,userId);
            try
            {
                 var employee = _employeeRepository.GetEmployeeById(userId);
                if (employee == null) throw new ValidationException("Requester Details Not Found");
                 var AwardeeId = award.AwardeeId;
                award.RequesterId = employee!.Id;
                award.ApproverId = employee.ReportingPersonId;
                award.PublisherId = _award.GetHRID(AwardeeId);
                award.StatusId = 1;
                award.AddedBy = userId;
                award.AddedOn = DateTime.Now;
                return _award.RaiseAwardRequest(award);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardService: RaiseRequest(Award award,int employeeId) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {

                _logger.LogError("AwardService: RaiseRequest(Award award,int employeeId) : (Error:{Message}", exception.Message);
                throw;
            }
        }

        //to approve or reject the request raised by the requester
        public bool Approval(Award award)
        {
            if(award==null) throw new ValidationException("award should not be null");
            _awardValidations.ApprovalValidation(award);
            try
            {
                return _award.ApproveRequest(award);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardService: Approval(Award award) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
               _logger.LogError("AwardService: Approval(Award award) : (Error:{Message}", exception.Message);
                throw;
            }
        }

        //gets award id by using award id
        public object GetAwardById(int awardId)
        {
            if(awardId==0) throw new ValidationException("Award Id should not be null");
            try
            {
                var award = _award.GetAwardById(awardId);
                return GetAwardObject(award!);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardService: GetAwardById(int awardId) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardService: GetAwardById(int awardId) : (Error:{Message}", exception.Message);
                throw;
            }
        }

        //to add new comment by the user using comment object and employee id
        public bool AddComment(Comment comment)
        {
            _awardValidations.ValidateAddComment(comment);
            try
            {
                return _award.AddComments(comment);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardService: AddComment(Comment comment) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardService: AddComment(Comment comment) : (Error:{Message}", exception.Message);
                throw;
            }
        }

       //Gets all awards list using page id and employee id
        public IEnumerable<object> GetAwardsList(int? pageId, int? employeeId)
        {

            try
            {
                var awards = _award.GetAllAwardsList(pageId,employeeId );
                return awards.Select(award => GetAwardObject(award));
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardService: GetAwardsList(int ? pageId,int ? employeeId) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
               _logger.LogError("AwardService: GetAwardsList(int ? pageId,int ? employeeId) : (Error:{Message}", exception.Message);
                throw;
            }
        }

        //gets(view) comment using award id
        public IEnumerable<object> GetComments(int awardId)
        {
            if(awardId==0) throw new ValidationException("Award is should not be null");
            try
            {
                var comments = _award.GetComments(awardId);
                return comments.Select(Comment => new
                {
                    id = Comment.Id,
                    comments = Comment.Comments,
                    gender = Comment?.Employees?.Gender,
                    employeeName = Comment?.Employees?.FirstName,
                    employeeImage = Comment?.Employees?.Image,
                    commentedOn = Comment?.CommentedOn
                }).OrderByDescending(nameof => nameof.id);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardService: GetComments(int awardId) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardService: GetComments(int awardId) : (Error:{Message}", exception.Message);
                throw;
            }
        }

        //Get Award table details with their respective Names.
        private object GetAwardObject(Award award)
        {
            return new
            {
                id = award?.Id,
                requesterId = award?.RequesterId,
                awardeeId = award?.AwardeeId,
                awardTypeId = award?.AwardTypeId,
                approverId = award?.ApproverId,
                publisherId = award?.PublisherId,
                reason = award?.Reason,
                rejectedReason = award?.RejectedReason,
                couponCode = award?.CouponCode,
                statusId = award?.StatusId,
                addedBy = award?.AddedBy,
                addedOn = award?.AddedOn,
                updatedBy = award?.UpdatedBy,
                updatedOn = award?.UpdatedOn,
                aceId = award?.Awardee?.ACEID,
                awardeeName = award?.Awardee?.FirstName + " " + award?.Awardee?.LastName,
                awardeeImage = award?.Awardee?.Image,
                gender = award?.Awardee?.Gender,
                requesterName = award?.Awardee?.ReportingPerson?.FirstName,
                approverName = award?.Awardee?.ReportingPerson?.ReportingPerson?.FirstName,
                hRName = award?.Awardee?.HR?.FirstName,
                status = award?.Status?.StatusName,
                awardName = award?.AwardType?.AwardName,
                awardImage = award?.AwardType?.Image,
                designation = award?.Awardee?.Designation?.DesignationName,
                departmentId = award?.Awardee?.Designation?.Department?.Id,
                department = award?.Awardee?.Designation?.Department?.DepartmentName,
                organisation = award?.Awardee?.Designation?.Department?.Organisation?.OrganisationName,
                organisationId = award?.Awardee?.Designation?.Department?.Organisation?.Id
            };
        }


        public object ErrorMessage(string ValidationMessage)
        {
            return new { message = ValidationMessage };
        }
    }
}