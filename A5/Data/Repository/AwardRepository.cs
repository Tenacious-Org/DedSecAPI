using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using A5.Models;
using Microsoft.EntityFrameworkCore;
using A5.Service.Interfaces;
using A5.Data.Repository.Interface;
using A5.Service.Validations;
using A5.Service;

namespace A5.Data.Repository
{
    public class AwardRepository : IAwardRepository
    {
        private readonly AppDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<IAwardService> _logger;
        private readonly MailService _mail;

        public AwardRepository(AppDbContext context, IEmployeeRepository employeeRepository, ILogger<IAwardService> logger, MailService mail)
        {
            _context = context;
            _employeeRepository = employeeRepository;
            _logger = logger;
            _mail = mail;

        }
        
        //raises award request by using award object and employee
        public bool RaiseAwardRequest(Award award, int employeeId)
        {
            AwardServiceValidations.RequestValidation(award);
            try
            {
                var employee = _employeeRepository.GetEmployeeById(employeeId);
                if (employee == null) throw new ValidationException("Requester Details Not Found");
                _context.Set<Award>().Add(award);
                var aid = award.AwardeeId;
                award.RequesterId = employee!.Id;
                award.ApproverId = employee.ReportingPersonId;
                award.HRId = GetHRID(aid);
                award.StatusId = 1;
                award.AddedBy = employeeId;
                award.AddedOn = DateTime.Now;
                _context.SaveChanges();
                if (award.StatusId == 1)
                {
                    var awardee = GetAwardById(award.Id);
                    _mail?.RequesterAsync(awardee);
                }
                return true;

            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : RaiseAwardRequest(Award award,int employeeId) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardRepository : RaiseAwardRequest(Award award,int employeeId) : (Error:{Message}", exception.Message);
                return false;
            }
        }
        
        //Gets HR id by using awardee id
        public int GetHRID(int awardeeId)
        {
            var list = _context.Set<Employee>().FirstOrDefault(nameof => nameof.Id == awardeeId)!.HRId;
            
            return (int)list; 
        }
        
        //approves the request raised by using award object
        public bool ApproveRequest(Award award)
        {
            AwardServiceValidations.ApprovalValidation(award);
            try
            {
                if (award.StatusId == 4)
                {
                    bool IsIdAlreadyExists = _context.Awards!.Any(nameof => nameof.CouponCode == award.CouponCode);
                    if (IsIdAlreadyExists) throw new ValidationException("CouponCode already redeemed");
                }
                _context.Set<Award>().Update(award);
                award.UpdatedOn = DateTime.Now;
                _context.SaveChanges();
                if (award.StatusId == 4)
                {
                    var awardee = GetAwardById(award.Id);
                    _mail?.ExampleAsync(awardee);

                }
                else if (award.StatusId == 3)
                {
                    var awardee = GetAwardById(award.Id);
                    _mail?.RejectedAsync(awardee!);
                }
                return true;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : ApproveRequest(Award award) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                
                _logger.LogError("AwardRepository : ApproveRequest(Award award) : (Error:{Message}", exception.Message);
                return false;
            }
        }

        //Gets award by using award id
        public Award? GetAwardById(int awardId)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .FirstOrDefault(nameof => nameof.Id == awardId);
                return award != null ? award : throw new ValidationException($"There is no matching records found for award Id -{awardId} , enter the correct award Id");
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAwardById(int awardId) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
               _logger.LogError("AwardRepository : GetAwardById(int awardId) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        
        //Adds the comment by using comment object and employee id
        public bool AddComments(Comment comment, int employeeId)
        {
            AwardServiceValidations.ValidateAddComment(comment);
            try
            {
                _context.Set<Comment>().Add(comment);
                comment.EmployeeId = employeeId;
                comment.CommentedOn = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : AddComments(Comment comment, int employeeId) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardRepository : AddComments(Comment comment, int employeeId) : (Error:{Message}", exception.Message);
                return false;
            }
        }

        //returns the comments by using award id
        public IEnumerable<Comment> GetComments(int awardId)
        {
            AwardServiceValidations.ValidateGetComments(awardId);
            try
            {
                var comments = _context.Set<Comment>()
                     .Include("Employees")
                     .Include("Awards")
                     .Where(nameof => nameof.AwardId == awardId)
                     .ToList();
                return comments;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetComments(int awardId) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardRepository : GetComments(int awardId) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        
        //returns list of all awards by using page id and employee id
        public IEnumerable<Award> GetAllAwardsList(int? pageId, int? employeeId)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .ToList();
                if (pageId == 0 && employeeId == 0)
                    award = award.Where(nameof => nameof.StatusId == 4).ToList();
                else if (pageId == 1 && employeeId != 0)
                    award = award.Where(nameof => nameof.StatusId == 4 && nameof.AwardeeId == employeeId).ToList();
                else if (pageId == 2 && employeeId != 0)
                    award = award.Where(nameof => nameof.RequesterId == employeeId).OrderBy(nameof => nameof.StatusId).ToList();
                else if (pageId == 3 && employeeId != 0)
                    award = award.Where(nameof => nameof.ApproverId == employeeId).OrderBy(nameof => nameof.StatusId).ToList();
                else if (pageId == 4 && employeeId != 0)
                    award = award.Where(nameof => nameof.HRId == employeeId && (nameof.StatusId == 2 || nameof.StatusId == 4)).OrderBy(nameof => nameof.StatusId).ToList();         
                return award != null ? award : throw new ValidationException("No records Found");

            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllAwardsList(int? pageId, int? employeeId) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardRepository : GetAllAwardsList(int? pageId, int? employeeId) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        
        //returns the list of all awards
        public IEnumerable<Award> GetAllAwardees()
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof => nameof.StatusId == 4)
                    .ToList();


                return award;
            }
            catch (Exception exception)
            {
               _logger.LogError("AwardRepository : GetAllAwardees(): (Error:{Message}", exception.Message);
                throw;
            }
        }

        //Gets list of awards filtered by organisation wise using organisation id
        public IEnumerable<Award> GetAllByOrganisationWise(int organisationId)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.OrganisationId ==  organisationId && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllByOrganisationWise(int organisationId) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardRepository : GetAllByOrganisationWise(int organisationId) : (Error:{Message}", exception.Message);
                throw;
            }
        }

        //Gets list of awards filtered by awards wise using award id
        public IEnumerable<Award> GetAllAwardWise(int awardId)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof => nameof.AwardTypeId == awardId && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllAwardWise(int awardId) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardRepository : GetAllAwardWise(int awardId) : (Error:{Message}", exception.Message);
                throw;
            }
        }

        
        //Gets list of awards filtered by organisation and award using organisation id and award id
        public IEnumerable<Award> GetAllOrganisationAndAward(int organisationId, int awardId)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId && nameof.AwardTypeId == awardId && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllOrganisationAndAward(int organisationId, int awardId)  : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("AwardRepository : GetAllOrganisationAndAward(int organisationId, int awardId)  : (Error:{Message}",exception.Message);
              throw;
            }
        }
        
         //Gets list of awards filtered by organisation and department using organisation id and department id
        public IEnumerable<Award> GetAllOrganisationAndDepartment(int organisationId, int departmentId)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId && nameof.Awardee.Designation.Department.Id == departmentId && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository :GetAllOrganisationAndDepartment(int organisationId, int departmentId): (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardRepository :GetAllOrganisationAndDepartment(int organisationId, int departmentId): (Error:{Message}", exception.Message);
                throw;
            }
        }

         //Gets list of awards filtered by organisation and award using organisation id, department id and award id
        public IEnumerable<Award> GetAllOrganisationDepartmentAndAwardwise(int organisationId, int departmentId, int awardId)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof =>    nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId 
                                     && nameof.Awardee.Designation.Department.Id == departmentId
                                     && nameof.AwardTypeId == awardId 
                                     && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllOrganisationDepartmentAndAwardwise(int organisationId, int departmentId, int awardId) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardRepository : GetAllOrganisationDepartmentAndAwardwise(int organisationId, int departmentId, int awardId) : (Error:{Message}", exception.Message);
                throw;
            }
        }

         //Gets list of awards filtered by organisation, department and from date using organisation id and award id and from date
        public IEnumerable<Award> GetAllOrganisationDepartmentAndFromDateWise(int organisationId, int departmentId, DateTime start)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof =>    nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId 
                                     && nameof.Awardee.Designation.Department.Id == departmentId
                                     && nameof.UpdatedOn >= start.Date
                                     && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllOrganisationDepartmentAndFromDateWise(int organisationId, int departmentId, DateTime start) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
             
               _logger.LogError("AwardRepository : GetAllOrganisationDepartmentAndFromDateWise(int organisationId, int departmentId, DateTime start) : (Error:{Message}",exception.Message);
              throw;
            }
        }

        //Gets list of awards filtered by organisation, award and from date using organisation id and award id and from date
        public IEnumerable<Award> GetAllOrganisationAwardAndFromDateWise(int organisationId, int awardId, DateTime start)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof =>    nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId 
                                     && nameof.AwardTypeId == awardId
                                     && nameof.UpdatedOn >= start.Date
                                     && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllOrganisationAwardAndFromDateWise(int organisationId, int awardId, DateTime start): (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("AwardRepository : GetAllOrganisationAwardAndFromDateWise(int organisationId, int awardId, DateTime start): (Error:{Message}",exception.Message);
              throw;
            }
        }

       
       //Gets list of awards filtered by organisation, department and To date using organisation id and department id and To date
        public IEnumerable<Award> GetAllOrganisationDepartmentAndToDateWise(int organisationId, int departmentId, DateTime end)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof =>    nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId 
                                     && nameof.Awardee.Designation.Department.Id == departmentId
                                     && nameof.UpdatedOn <= end.Date
                                     && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllOrganisationDepartmentAndToDateWise(int organisationId, int departmentId, DateTime end) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
               _logger.LogError("AwardRepository : GetAllOrganisationDepartmentAndToDateWise(int organisationId, int departmentId, DateTime end) : (Error:{Message}",exception.Message);
              throw;
            }
        }

        //Gets list of awards filtered by organisation, award and To date using organisation id and award id and To date
        public IEnumerable<Award> GetAllOrganisationAwardAndToDateWise(int organisationId, int awardId, DateTime end)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof =>    nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId 
                                     && nameof.AwardTypeId == awardId
                                     && nameof.UpdatedOn <= end.Date
                                     && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository :GetAllOrganisationAwardAndToDateWise(int organisationId, int awardId, DateTime end) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("AwardRepository :GetAllOrganisationAwardAndToDateWise(int organisationId, int awardId, DateTime end) : (Error:{Message}",exception.Message);
              throw;
            }
        }

        //Gets list of awards filtered by organisation and from date using organisation id and from date
        public IEnumerable<Award> GetAllFilteredOrganisationAndFromDateWise(int organisationId, DateTime start)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.StatusId == 4
                                  && nameof.UpdatedOn >= start.Date)
                    .ToList();
                return award;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository :GetAllFilteredOrganisationAndFromDateWise(int organisationId, DateTime start) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("AwardRepository :GetAllFilteredOrganisationAndFromDateWise(int organisationId, DateTime start) : (Error:{Message}",exception.Message);
              throw;
            }
        }

        //Gets all awards filtered by organisation and To date using organisation id and To date
        public IEnumerable<Award> GetAllFilteredOrganisationAndToDateWise(int organisationId, DateTime end)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.StatusId == 4
                                  && nameof.UpdatedOn <= end.Date)
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllFilteredOrganisationAndToDateWise(int organisationId, DateTime end) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
               _logger.LogError("AwardRepository : GetAllFilteredOrganisationAndToDateWise(int organisationId, DateTime end) : (Error:{Message}", exception.Message);
                throw;
            }
        }

        //Gets all awards filtered by date wise using organisation id,department id, award id,from date and to date
        public IEnumerable<Award> GetAllFilteredDateWise(int organisationId, int departmentId, int awardId, DateTime start, DateTime end)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.Awardee.Designation.Department.Id ==departmentId 
                                  && nameof.AwardTypeId == awardId
                                  && nameof.StatusId == 4
                                  && (nameof.UpdatedOn >= start.Date && nameof.UpdatedOn <= end.Date))
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllFilteredDateWise(int orgianisationId, int departmentId, int awardId, DateTime start, DateTime end) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardRepository : GetAllFilteredDateWise(int orgianisationId, int departmentId, int awardId, DateTime start, DateTime end) : (Error:{Message}", exception.Message);
                throw;
            }
        }

        //Gets all awards list filtered by organisation,department and date wise using organisation id, department id, from date and To date
        public IEnumerable<Award> GetAllOrganisationDepartmentDateWise(int organisationId, int departmentId, DateTime start, DateTime end)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.Awardee.Designation.Department.Id == departmentId
                                  && nameof.StatusId == 4
                                  && (nameof.UpdatedOn >= start.Date && nameof.UpdatedOn <= end.Date))
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllOrganisationDepartmentDateWise(int organisationId, int departmentId, DateTime start, DateTime end): (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardRepository : GetAllOrganisationDepartmentDateWise(int organisationId, int departmentId, DateTime start, DateTime end): (Error:{Message}", exception.Message);
                throw;
            }
        }

        
        //gets all list of awards by organisation and date wise using organisation id,award id, from date and To date
        public IEnumerable<Award> GetAllOrganisationAwardDateWise(int organisationId, int awardId, DateTime start, DateTime end)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.AwardTypeId == awardId
                                  && nameof.StatusId == 4
                                  && (nameof.UpdatedOn >= start.Date && nameof.UpdatedOn <= end.Date))
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllOrganisationAwarddDateWise(int organisationId, int awardId, DateTime start, DateTime end) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
               _logger.LogError("AwardRepository : GetAllOrganisationAwarddDateWise(int organisationId, int awardId, DateTime start, DateTime end) : (Error:{Message}", exception.Message);
                throw;
            }
        }

         public IEnumerable<Award> GetAllDateWise(DateTime start, DateTime end)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof =>  nameof.StatusId == 4
                                  && (nameof.UpdatedOn >= start.Date && nameof.UpdatedOn <= end.Date))
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository :GetAllDateWise(DateTime start, DateTime end) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardRepository :GetAllDateWise(DateTime start, DateTime end) : (Error:{Message}", exception.Message);
                throw;
            }
        }

        //Gets all list of awards by date wise using from date
        public IEnumerable<Award> GetAllFilteredFromDateWise(DateTime start)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof => nameof.StatusId == 4 && (nameof.UpdatedOn >= start.Date))
                    .ToList();
                return award;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllFilteredFromDateWise(DateTime start) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("AwardRepository : GetAllFilteredFromDateWise(DateTime start) : (Error:{Message}",exception.Message);
              throw;
            }
        }

        //Gets all list of awards by date wise using To date
        public IEnumerable<Award> GetAllFilteredToDateWise(DateTime end)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof => nameof.StatusId == 4 && (nameof.UpdatedOn <= end.Date))
                    .ToList();
                return award;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllFilteredToDateWise(DateTime end) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              
              _logger.LogError("AwardRepository : GetAllFilteredToDateWise(DateTime end) : (Error:{Message}",exception.Message);
              throw;
            }
        }
        
       
   }
}