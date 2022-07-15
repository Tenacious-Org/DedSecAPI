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
   public class AwardRepository :IAwardRepository
   {
     private readonly AppDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly  ILogger<IAwardService> _logger;
        private readonly MailService _mail;

        public AwardRepository(AppDbContext context, IEmployeeRepository employeeRepository,ILogger<IAwardService> logger, MailService mail)
        {
            _context=context;
            _employeeRepository = employeeRepository;
            _logger=logger;
            _mail = mail;
           
        }
    public bool RaiseAwardRequest(Award award,int id)
    {
        if(!AwardServiceValidations.RequestValidation(award,id)) throw new ValidationException("Invalid data");
        try{
            var employee = _employeeRepository.GetEmployeeById(id);
                _context.Set<Award>().Add(award);
                award.RequesterId=employee!.Id;
                award.ApproverId = employee.ReportingPersonId;
                award.HRId= employee.HRId;
                award.StatusId=1;
                award.AddedBy=employee.Id;
                award.AddedOn=DateTime.Now;
                _context.SaveChanges();
                if(award.StatusId == 1)
                {
                    var awardee = GetAwardById(award.Id);
                    _mail?.RequesterAsync(awardee);
                }
                return true;

        }
        catch(ValidationException exception)
        {
                _logger.LogError("AwardRepository : RaiseAwardRequest(Award award,int id) : (Error:{Message}",exception.Message);
                throw;
        }
        catch (Exception exception){
            _logger.LogError("Error: {Message}",exception.Message);
            return false;
        }
    }
    public bool ApproveRequest(Award award,int id)
    {
        if(award.StatusId == 4)
        {
        bool IsIdAlreadyExists=_context.Awards!.Any(nameof=>nameof.CouponCode==award.CouponCode);
        if(IsIdAlreadyExists) throw new ValidationException("CouponCode already redeemed");
        }
        try{
                _context.Set<Award>().Update(award);
                award.UpdatedBy=id;
                award.UpdatedOn=DateTime.Now;
                _context.SaveChanges();
                if(award.StatusId == 4)
                {
                    var awardee = GetAwardById(award.Id); 
                    _mail?.ExampleAsync(awardee);
                    
                }
                else if(award.StatusId == 3)
                {
                    var awardee = GetAwardById(award.Id);
                    _mail?.RejectedAsync(awardee);
                }
                return true;  
       }
       catch(ValidationException exception)
        {
                _logger.LogError("AwardRepository : ApproveRequest(Award award,int id) : (Error:{Message}",exception.Message);
                throw;
        }
        catch (Exception exception){
            _logger.LogError("Error: {Message}",exception.Message);
            return false;
        }
    }
   
     public Award? GetAwardById(int id)
        {
            if(!AwardServiceValidations.ValidateGetAwardById(id)) throw new ValidationException("Invalid data");
            try{
                var award=_context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .FirstOrDefault(nameof=> nameof.Id == id);
                return award;
            }
            catch(ValidationException exception)
            {
                  _logger.LogError("AwardRepository : GetAwardById(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              throw;
            }
        }
        public bool AddComments(Comment comment,int employeeId)
        {
            if(!AwardServiceValidations.ValidateAddComment(comment)) throw new ValidationException("Invalid data");
            try{
                  _context.Set<Comment>().Add(comment);
                  comment.EmployeeId=employeeId;
                  comment.CommentedOn=DateTime.Now;
                  _context.SaveChanges();
                  return true;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : AddComments(Comment comment) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              return false;
            }
        }
        
         public IEnumerable<Comment> GetComments(int awardId)
        {
            if(!AwardServiceValidations.ValidateGetComments(awardId)) throw new ValidationException("Invalid data");
           try
           {
               var comments= _context.Set<Comment>()
                    .Include("Employees")
                    .Include("Awards")
                    .Where(nameof=>nameof.AwardId==awardId)
                    .ToList();
               return comments;
           }
            catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetComments(Comment comment) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              throw;
            }
        }
          public IEnumerable<Award> GetAllAwardsList(int ? pageId,int ? employeeId)
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
                 if(pageId==1 && employeeId!=0) 
                    return award =award.Where(nameof =>nameof.StatusId == 4 && nameof.AwardeeId==employeeId).ToList();
                else if(pageId==2 && employeeId!=0) 
                    return award =award.Where(nameof => nameof.RequesterId == employeeId).OrderBy(nameof => nameof.StatusId).ToList();
                else if(pageId==3 && employeeId!=0) 
                    return award =award.Where(nameof => nameof.ApproverId == employeeId).OrderBy(nameof => nameof.StatusId).ToList();
                else if(pageId==4 && employeeId!=0) 
                    return award =award.Where(nameof => nameof.HRId == employeeId && (nameof.StatusId == 2 || nameof.StatusId == 4)).OrderBy(nameof => nameof.StatusId).ToList();
                else
                    return award =award.Where(nameof =>nameof.StatusId == 4).ToList();

            }
              catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllAwardsList() : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              throw;
            }
        }
        public IEnumerable<Award> GetAllWinners()
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
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllWinners() : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              throw;
            }
        }
        
        public IEnumerable<Award> GetAllbyOrgwise(int id)
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
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.OrganisationId == id && nameof.StatusId == 4)
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

        public IEnumerable<Award> GetAllbyDeptwise(int id)
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
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.Id == id && nameof.StatusId == 4)
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

        public IEnumerable<Award> GetAllOrgAndAwardwise(int orgid, int awdid)
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
                    .Where(nameof => nameof.Awardee.Designation.Department.Organisation.Id == orgid && nameof.AwardTypeId == awdid && nameof.StatusId == 4)
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
        public IEnumerable<Award> GetAllOrgAndDepwise(int orgid, int depid)
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
                    .Where(nameof => nameof.Awardee.Designation.Department.Organisation.Id == orgid && nameof.Awardee.Designation.Department.Id == depid && nameof.StatusId == 4)
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
                    .Where(nameof => nameof.Awardee.Designation.Department.Organisation.Id == orgid 
                                  && nameof.Awardee.Designation.Department.Id == deptid 
                                  && nameof.AwardTypeId == awdid 
                                  && nameof.StatusId == 4
                                  && (nameof.UpdatedOn >= start.Date && nameof.UpdatedOn <= end.Date))
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