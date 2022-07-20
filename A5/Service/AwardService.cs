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
        
        //to raise award request using award obejct and employee id
        public bool RaiseRequest(Award award,int employeeId)
        {
           _awardValidations.RequestValidation(award,employeeId);
            try
            {
                 var employee = _employeeRepository.GetEmployeeById(employeeId);
                if (employee == null) throw new ValidationException("Requester Details Not Found"); 
                 var AwardeeId = award.AwardeeId;
                award.RequesterId = employee!.Id;
                award.ApproverId = employee.ReportingPersonId;
                award.HRId = _award.GetHRID(AwardeeId);
                award.StatusId = 1;
                award.AddedBy = employeeId;
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

        //approves the request raised
        public bool Approval(Award award)
        {
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
        
        //adds new comment using comment object and employee id
        public bool AddComment(Comment comment, int employeeId)
        {
            _awardValidations.ValidateAddComment(comment,employeeId);
            try
            {
                return _award.AddComments(comment, employeeId);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardService: AddComment(Comment comment,int employeeId) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardService: AddComment(Comment comment,int employeeId) : (Error:{Message}", exception.Message);
                throw;
            }
        }
       
       //Gets award list using page id and employee id
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
        
        //gets comment using award id
        public IEnumerable<object> GetComments(int awardId)
        {
            AwardValidations.ValidateGetComments(awardId);
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
        
        //Get Award Table Details To With their respective Names.
        private object GetAwardObject(Award award)
        {
            return new
            {
                id = award?.Id,
                requesterId = award?.RequesterId,
                awardeeId = award?.AwardeeId,
                awardTypeId = award?.AwardTypeId,
                approverId = award?.ApproverId,
                hRId = award?.HRId,
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