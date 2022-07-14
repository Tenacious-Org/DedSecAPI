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
   public class AwardRepository
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
       try{
         var employee = _employeeRepository.GetEmployeeById(id);
                _context.Set<Award>().Update(award);
                award.UpdatedBy=employee?.Id;
                award.UpdatedOn=DateTime.Now;
                _context.SaveChanges();
                if(award.StatusId == 4)
                {

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
        public bool AddComments(Comment comment)
        {
            if(!AwardServiceValidations.ValidateAddComment(comment)) throw new ValidationException("Invalid data");
            try{
                  _context.Set<Comment>().Add(comment);
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
          public IEnumerable<Award> GetAllAwardsList()
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
                return award;
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

                _mail.ExampleAsync();
                
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

        
       
   }
}