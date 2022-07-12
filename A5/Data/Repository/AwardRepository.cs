using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using A5.Models;
using Microsoft.EntityFrameworkCore;
using A5.Service.Interfaces;
namespace A5.Data.Repository
{
   public class AwardRepository
   {
     private readonly AppDbContext _context;
        private readonly MasterRepository _master;
        private readonly  ILogger<IAwardService> _logger;

        public AwardRepository(AppDbContext context, MasterRepository master,ILogger<IAwardService> logger)
        {
            _context=context;
            _master = master;
            _logger=logger;
           
        }
    public bool RaiseAwardRequest(Award award,int id)
    {
       
        try{
            var employee = _master.GetEmployeeById(id);
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
             _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Award Repository : RaiseAwardRequest(Award award,int id) : (Error:{Message}",exception.Message);
                throw;
        }
        catch (Exception exception){
            _logger.LogError("Error: {Message}",exception.Message);
            return false;
        }
    }
    public bool ApproveRequest(Award award,int id)
    {
         bool result = false;
       try{
         var employee = _master.GetEmployeeById(id);
                _context.Set<Award>().Update(award);
                award.UpdatedBy=employee?.Id;
                award.UpdatedOn=DateTime.Now;
                _context.SaveChanges();
                result=true;
                return result;  
       }
       catch(ValidationException exception)
        {
             _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Award Repository : ApproveRequest(Award award,int id) : (Error:{Message}",exception.Message);
                throw;
        }
        catch (Exception exception){
            _logger.LogError("Error: {Message}",exception.Message);
            return false;
        }
    }
   
     public Award? GetAwardById(int id)
        {
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
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Award Repository : GetAwardById(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              throw;
            }
        }
        public bool AddComments(Comment comment)
        {
           
            bool result=false;
            try{
                  _context.Set<Comment>().Add(comment);
                    _context.SaveChanges();
                    result=true;
                    return result;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Award Repository : AddComments(Comment comment) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              throw;
            }
        }
        
         public IEnumerable<Comment> GetComments(int awardId)
        {
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
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Award Repository : GetComments(Comment comment) : (Error:{Message}",exception.Message);
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
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Award Repository : GetAllAwardsList() : (Error:{Message}",exception.Message);
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
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Award Repository : GetAllWinners() : (Error:{Message}",exception.Message);
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
                    .Where(nameof => nameof.Awardee!.Designation!.Department!.OrganisationId == id)
                    .ToList();
                return award;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Award Repository : GetAllbyOrgwise(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              throw;
            }
        }

        
       
   }
}