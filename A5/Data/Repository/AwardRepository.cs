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
                _logger.LogError("AwardRepository : RaiseAwardRequest(Award award,int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                return false;
            }
        }
        
        //Gets HR id by using awardee id
        public int? GetHRID(int awardeeid)
        {
            var list = _context?.Set<Employee>()?.FirstOrDefault(nameof => nameof.Id == awardeeid)?.HRId;
            
            return (int?)list;
        }
        
        //approves the request raised by using award object
        public bool ApproveRequest(Award award)
        {
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
                _logger.LogError("AwardRepository : ApproveRequest(Award award,int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                return false;
            }
        }

        //Gets award by using award id
        public Award? GetAwardById(int id)
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
                    .FirstOrDefault(nameof => nameof.Id == id);
                return award != null ? award : throw new ValidationException($"There is no matching records found for award Id -{id} , enter the correct award Id");
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAwardById(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
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
                _logger.LogError("AwardRepository : AddComments(Comment comment) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
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
                _logger.LogError("AwardRepository : GetComments(Comment comment) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
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
                _logger.LogError("AwardRepository : GetAllAwardsList() : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
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
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }

        //Gets list of awards filtered by organisation wise using organisation id
        public IEnumerable<Award> GetAllbyOrgwise(int orgid)
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
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.OrganisationId == orgid && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }

        //Gets list of awards filtered by awards wise using award id
        public IEnumerable<Award> GetAllAwardwise(int id)
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
                    .Where(nameof => nameof.AwardTypeId == id && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }

        
        //Gets list of awards filtered by organisation and award using organisation id and award id
        public IEnumerable<Award> GetAllOrganisationandAward(int orgid, int awdid)
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
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == orgid && nameof.AwardTypeId == awdid && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              throw;
            }
        }
        
         //Gets list of awards filtered by organisation and department using organisation id and department id
        public IEnumerable<Award> GetAllOrganisationandDepartment(int orgid, int depid)
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
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == orgid && nameof.Awardee.Designation.Department.Id == depid && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }

         //Gets list of awards filtered by organisation and award using organisation id, department id and award id
        public IEnumerable<Award> GetAllOrgDepandAwardwise(int orgid, int depid, int awdid)
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
                    .Where(nameof =>    nameof.Awardee!.Designation!.Department!.Organisation!.Id == orgid 
                                     && nameof.Awardee.Designation.Department.Id == depid
                                     && nameof.AwardTypeId == awdid 
                                     && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }

         //Gets list of awards filtered by organisation, department and from date using organisation id and award id and from date
        public IEnumerable<Award> GetAllOrgDepandFromdatewise(int orgid, int depid, DateTime start)
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
                    .Where(nameof =>    nameof.Awardee!.Designation!.Department!.Organisation!.Id == orgid 
                                     && nameof.Awardee.Designation.Department.Id == depid
                                     && nameof.UpdatedOn >= start.Date
                                     && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              throw;
            }
        }

        //Gets list of awards filtered by organisation, award and from date using organisation id and award id and from date
        public IEnumerable<Award> GetAllOrgAwardandFromdatewise(int orgid, int awdid, DateTime start)
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
                    .Where(nameof =>    nameof.Awardee!.Designation!.Department!.Organisation!.Id == orgid 
                                     && nameof.AwardTypeId == awdid
                                     && nameof.UpdatedOn >= start.Date
                                     && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              throw;
            }
        }

       
       //Gets list of awards filtered by organisation, department and To date using organisation id and department id and To date
        public IEnumerable<Award> GetAllOrgDepandTodatewise(int orgid, int depid, DateTime end)
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
                    .Where(nameof =>    nameof.Awardee!.Designation!.Department!.Organisation!.Id == orgid 
                                     && nameof.Awardee.Designation.Department.Id == depid
                                     && nameof.UpdatedOn <= end.Date
                                     && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              throw;
            }
        }

        //Gets list of awards filtered by organisation, award and To date using organisation id and award id and To date
        public IEnumerable<Award> GetAllOrgAwardandTodatewise(int orgid, int awdid, DateTime end)
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
                    .Where(nameof =>    nameof.Awardee!.Designation!.Department!.Organisation!.Id == orgid 
                                     && nameof.AwardTypeId == awdid
                                     && nameof.UpdatedOn <= end.Date
                                     && nameof.StatusId == 4)
                    .ToList();
                return award;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              throw;
            }
        }

        //Gets list of awards filtered by organisation and from date using organisation id and from date
        public IEnumerable<Award> GetAllFilteredOrganisationandFromDateWise(int orgid, DateTime start)
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
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == orgid 
                                  && nameof.StatusId == 4
                                  && nameof.UpdatedOn >= start.Date)
                    .ToList();
                return award;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              throw;
            }
        }

        //Gets all awards filtered by organisation and To date using organisation id and To date
        public IEnumerable<Award> GetAllFilteredOrganisationandToDateWise(int orgid, DateTime end)
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
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == orgid 
                                  && nameof.StatusId == 4
                                  && nameof.UpdatedOn <= end.Date)
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }

        //Gets all awards filtered by date wise using organisation id,department id, award id,from date and to date
        public IEnumerable<Award> GetAllFilteredDateWise(int orgid, int deptid, int awdid, DateTime start, DateTime end)
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
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == orgid
                                  && nameof.Awardee.Designation.Department.Id == deptid
                                  && nameof.AwardTypeId == awdid
                                  && nameof.StatusId == 4
                                  && (nameof.UpdatedOn >= start.Date && nameof.UpdatedOn <= end.Date))
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }

        //Gets all awards list filtered by organisation,department and date wise using organisation id, department id, from date and To date
        public IEnumerable<Award> GetAllOrgDepDateWise(int orgid, int deptid, DateTime start, DateTime end)
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
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == orgid
                                  && nameof.Awardee.Designation.Department.Id == deptid
                                  && nameof.StatusId == 4
                                  && (nameof.UpdatedOn >= start.Date && nameof.UpdatedOn <= end.Date))
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
                throw;
            }
        }

        
        //gets all list of awards by organisation and date wise using organisation id,award id, from date and To date
        public IEnumerable<Award> GetAllOrgAwdDateWise(int orgid, int awdid, DateTime start, DateTime end)
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
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == orgid
                                  && nameof.AwardTypeId == awdid
                                  && nameof.StatusId == 4
                                  && (nameof.UpdatedOn >= start.Date && nameof.UpdatedOn <= end.Date))
                    .ToList();
                return award;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: {Message}", exception.Message);
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
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
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
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              throw;
            }
        }
        
       
   }
}